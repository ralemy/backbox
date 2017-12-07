[TOC]

# Barcode scanning with Xamarin

This branch adds barcode scanning to the Xamarin BDD project. Barcode scanning is done throuhg the [Zing.Net.Mobile for Forms][] Nuget pakage. 

For starters, the package should be added to iOS and Android projects and permissions and initializations should be applied as described in the [Zing.Net.Mobile for Forms][]

# Getting to the barcode page

The first requirement is to have some way of getting to the barcode page. Easiest is to have a button that moves to the page. 

The feature test file would be Barcode.feature and the first scenario checks to see there is a barcode button on the main page. 

~~~gherkin
Feature: Barcode
	I need to have a page that uses the phone camera to read barcodes.
	
@barcode_structure
Scenario: Main page should have a button to navigate to barcode page
    Given I am running the app
    When I examine the main page
    Then It has a button marked as 'Barcode'

~~~

Interesting in this scenario is that there in no test code to be written. the difference between steps of this feature and the Main Page feature is in the parameters of the test. so once we run it and see it fail we can get to coding directly.

the second scenario checks to see if tapping it takes us to the barcode page. The mechanism for navigation to the new page is the same as before, but the barcode page is special in that we have to implement it completely in code, not partialy in xaml and code-behind.

~~~gherkin
@barcode_function
Scenario: Should navigate to barcode page
    Given I am in main page
    When I tap the 'Barcode' button
    Then I go to the barcode page
~~~

The only step to implement is the last one. We need a page test object for Barcode page, we can write its interface but for correct functionality we need to implement it and use Repl. For now, we fill go through the three steps:

 * Create an interface for the page, with a WaitForLoad() function. we will add more when we findout which components are there.
 * Implement the interface in a class, for the body of WaitForLoad() leave it to throw an exception.
 * Inject the class to the FeatureContext 

finally, create the step for barcode page examination, and have it report as pending after getting the page test object from FeatureContext.

~~~csharp
 [Binding]
    public class ShouldNavigateToBarcodePage : StepsBase
    {
        [Then(@"I go to the barcode page")]
        public void ThenIGoToTheBarcodePage()
        {
            var page = FeatureContext.Current.Get<IBarcodeScreen>();
            ScenarioContext.Current.Pending();
        }

    }
~~~

# Defining the page

Barcode page is similar to other form pages, it has a PageKey for Navigation and a ViewModel object that is tasked with containing business logic and replace code-behind widgets.

## Registering the page and ViewModel
The ViewModel object is a standard C# class which is a subclass of ViewModelBase, and is Registered with the dependency manager in the App.xaml.cs file.

~~~csharp
        private void RegisterDependencies()
        {
            Initializer.SetupDI();
            Initializer.Register<MainPageVM>();
            Initializer.Register<AssociationPageVM>();
            Initializer.Register<BarcodePageVM>();
        }

~~~

Also in the App.xaml.cs, the Page file itself is Registered with the NavigationManager.

~~~csharp
        private Page RegisterPages(NavigationPage page)
        {
            var nav= Initializer.GetDependency<INavigationManager>();
            nav.SetMain(page);
            nav.Register(AssociationPage.PageKey, typeof(AssociationPage));
            nav.Register(BarCodeScanPage.PageKey, typeof(BarCodeScanPage));
            return page;
        }
~~~

## Another way to create Page clases
This Page object is different. it doesn't have a partial xaml file, it is a C# object that is a subclass of Content Page.

~~~csharp
    public class BarCodeScanPage : ContentPage
    {
        BarcodePageVM ViewModel;
        public static readonly string PageKey = "BarcodeScanPage";

        public BarCodeScanPage() :base()
        {
            ViewModel = Initializer.GetDependency<BarcodePageVM>();
            BindingContext = ViewModel;
   
~~~

As described in [Zing.net.Mobile for Forms][], the content of the page could be replaced with a Grid that has multiple layers. the first one should be a _ZXingScannerView_, which has a few properties that could be leveraged to control the operation of the barcode scanner.

property Name | usage |
--------------|-------|
IsScanning| puts the view to scanning mode if true, stops scanning if false|
HasTorch| true if the device has flash light, false if not|
InAnalyzing| Puts the scanning to pause if false and resumes once true|
OnScanResult|The event handler for when barcode is read and ready|

So, in the appearing of the page, the IsScanning and IsAnalyzing are set to true, and on the disappearing of the page the IsScanning is set to false. on the arrival of a new barcode, IsAnalysing shoud be set to false to pause new barcodes until this one is parsed.

here is the block that prepares the first layer, stashing the object in a class property called _zxing_. The ZXingScannerView object's properites and events of interest are bound to the Context and the BindingContext is set to the view model.

~~~csharp
        private View InitZXingView()
        {
            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            zxing.SetBinding(ZXingScannerView.IsScanningProperty, "IsScanning");
            zxing.SetBinding(ZXingScannerView.IsAnalyzingProperty, "IsAnalyzing");
            zxing.SetBinding(ZXingScannerView.IsTorchOnProperty, "IsTorchOn");
            zxing.SetBinding(ZXingScannerView.ScanResultCommandProperty, "ScanResult");
            zxing.SetBinding(ZXingScannerView.ResultProperty, "Result");
            zxing.BindingContext = ViewModel;
            return zxing;
        }
~~~
## The Overlay View

For the overlay layer, we can use _ZXingDefaultOverlay_ object, However, any View object can be used, and allows for more flexibility. the [Zing.Net.Mobile for Forms][] already shows an example of using the _ZXingDefaultOverlay_ class, here we extend that class to contain a Cancel button and command binding for it.

~~~csharp
public class BarcodeOverlay : ZXingDefaultOverlay
    {
        Button _Cancel;

        public BarcodeOverlay() : base()
        {
            BindingContext = this;
            CreateAndAddComponents();
        }

        private void CreateAndAddComponents()
        {
            _Cancel = new Button
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Start,
                Text = "Cancel",
                TextColor = Color.Maroon,
                AutomationId = "Barcode_CancelButton",
            };
           _Cancel.Clicked += (sender, e) =>
            {
                CancelButtonClicked?.Invoke(_Cancel, e);
                if (CancelCommand != null)
                    if (CancelCommand.CanExecute(null))
                        CancelCommand.Execute(null);
            };

            Children.Add(_Cancel, 0, 2);

        }

        public static BindableProperty CancelCommandProperty =
            BindableProperty.Create(nameof(CancelCommand), typeof(ICommand), typeof(BarcodeOverlay),
                defaultValue: default(ICommand),
                propertyChanged: OnCancelCommandChanged);
        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        private static void OnCancelCommandChanged(BindableObject bindable, object oldvalue, object newValue)
        {
            var overlay = bindable as BarcodeOverlay;
            if (overlay?._Cancel == null) return;
            overlay._Cancel.Command = newValue as Command;
        }
    }
~~~
Note that to add the Cancel button, a few steps where taken:

* Created the button Object and added to View.
* Registered a click handler to both invoke the CancelButtonClicked event listeners and the Command binding for Cancel button
* Declared a Bindable Probery for the CancelCommand and implemented a change handler for it.


## Creating the final Page
This new class can now be instantiated to create the overlay layer of the Grid in BarcodeScanPage.cs, and bindings are set to the ViewModel.

~~~csharp
private View InitOverlay(){
            overlay = new BarcodeOverlay
            {
                ShowFlashButton = zxing.HasTorch,
                BindingContext = ViewModel
            };

            overlay.FlashButtonClicked += (sender, e) =>
                zxing.IsTorchOn = !zxing.IsTorchOn;

            overlay.SetBinding(ZXingDefaultOverlay.TopTextProperty, "TopText");
            overlay.SetBinding(ZXingDefaultOverlay.BottomTextProperty, "BottomText");
            overlay.SetBinding(BarcodeOverlay.CancelCommandProperty, "CancelOperation");
            return overlay;
        }
~~~

Finally, the two can be put in the Grid and set as contant of the page:

~~~csharp
private View MakeScannerGrid()
        {
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            grid.Children.Add(InitZXingView());
            grid.Children.Add(InitOverlay());
            grid.BindingContext = ViewModel;
            return grid;
        }
~~~
# The ViewModel Object.

The ViewModel object is shared between the views of the page, so it has commands and properties to support both of them.

For Binding to properties, there are the private variable, the getter and setter, and the Set() function which will enusre double binding:

~~~csharp
private string _TopText = "Initial TopText";
        public String TopText
        {
            get => _TopText;
            set => Set(() => TopText, ref _TopText, value);
        }
~~~

For Binding to commands, a public ICommand with a private setter that gets set in the constructor and maps to a RelayCommand that calls a method on the abject when command is invoked.

~~~csharp
        public ICommand FlashOperation { get; private set; }

        public BarcodePageVM(INavigationService navigator)
        {
        	...
        	
          FlashOperation = new RelayCommand(OperationFlash);
        }

        private void OperationFlash()
        {
        	...
        }

~~~


# References
[Zing.Net.Mobile for Forms][]

[Zing.Net.Mobile for Forms]:https://components.xamarin.com/gettingstarted/zxing.net.mobile.forms
