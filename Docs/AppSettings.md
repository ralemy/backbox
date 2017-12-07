[TOC]

# Adding Settings to Mobile App

Most apps require settings feature. Settings are a collection of key-value pairs for the parameters that the app needs for its functionality. In order to have a platform independent settings feature, one needs to be able to access (read and write) the settings in a common way, as well as displaying them in a device-independent fashion.

# Crossplatform access to Settings

Install [Xam.Plugins.Settings][] Nuget package in the Droid and iOS projects. Once installed, Create a Settings.cs class in the root of the shared project and add a static class called Settings in the namespace of _$rootnamespace$.Helpers_:

~~~csharp
namespace associator.Helpers{
	public static class Settings{
		private static ISettings AppSettings{
			get => CrossSettings.Current;
		}
	}
}
~~~

This will serve as the Singleton for accessing settings throughout the application. Individual Settings parameters are added to this singleton class as static members with getters and setters:

~~~csharp
        public static bool UseHttp{
            get => AppSettings.GetValueOrDefault(nameof(UseHttp), true);
            set => AppSettings.AddOrUpdateValue(nameof(UseHttp), value);
        }
~~~

To allow the user to change the settings, we need a UI page. Since we are following the MVVMLight model, we need some housekeeping:

* Add a Forms ContentPage Xaml to the Pages directory and call it SettingsPage.xaml
* Add a C# subclass of ViewModelBase to the ViewModels directory and call it SettingsPageVM.cs
* Add a PageKey with value "SettingsPage" to SettingsPage.xaml.cs, and add an AutomationId with the same value to ContentPage element in SettingsPage.xaml
* Register the page with NavigationManager and the ViewModel with dependency manager in App.xaml.cs

~~~csharp
        private Page RegisterPages(NavigationPage page)
        {
            var nav= Initializer.GetDependency<INavigationManager>();
            nav.SetMain(page);
            nav.Register(AssociationPage.PageKey, typeof(AssociationPage));
            nav.Register(SettingsPage.PageKey, typeof(SettingsPage));
            return page;
        }

        private void RegisterDependencies()
        {
            Initializer.SetupDI();
            Initializer.Register<MainPageVM>();
            Initializer.Register<SettingsPageVM>();
        }

~~~

finally, set the BindingContext of the SettingsPage.xaml.cs to the ViewModel:

~~~csharp
    public partial class SettingsPage : ContentPage
    {
        public static readonly string PageKey = "SettingsPage";
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = Initializer.GetDependency<SettingsPageVM>()
        }
    }
~~~

# Navigating to the Settings Page

The easiest way is to add a new button to the Main Page and Set the command for it to navigate to the Settigs Page. As usual, the ViewModel will have a command that is initialized in the constructor and uses the navigator to navigate to the settings page when invoked.

## The first test

To test for this part, We need a little more housekeeping on the test side. 

* First we need a Specflow feature file that elaborates what is being tested:

~~~gherkin
Feature: Settings Page
    To have a configurable application 
    I need a settings page where parameters can be stored and updated.
	
@Settings_Page
Scenario: Should Navigate from Main to Settings Page
	Given I am in Main Page
	And There is a 'Settings' button on the page
	When I press the 'Settings' button
	Then the application will Navigate to Settings Page
~~~

* Once we save this file, we need the second part of the partial feature class that ensures the app is started and available:

~~~csharp
   public partial class SettingsPageFeature : FeatureBase
    {
        public SettingsPageFeature(Platform p, string iS, bool reset):base(p,iS,reset)
        {
        }
    }
~~~

* Then we execute the test once to get the expected template in the error message and create a C# subclass of StepsBase to implement it.

~~~csharp
    public class ShouldNavigateFromMainToSettingsPage : StepsBase
    {
        [Given(@"I am in Main Page")]
        public void GivenIAmInMainPage()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"There is a '(.*)' button on the page")]
        public void GivenThereIsAButtonOnThePage(string settings)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I press the '(.*)' button")]
        public void WhenIPressTheButton(string settings0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the application will Navigate to Settings Page")]
        public void ThenTheApplicationWillNavigateToSettingsPage()
        {
            ScenarioContext.Current.Pending();
        }
   }
~~~

## Creating the page test object

Now we need to create a page test object for the SettingsPage. this in turn is divided into three steps. 

* Create an ISettingsScreen that is a subset of IAppScreen<ISettingsScreen>. No extra memebers are needed for now

~~~csharp
 public interface ISettingsScreen: IAppScreen<ISettingsScreen>
    {}
~~~

* Create a SettingsScreen object that inherits from AppScreen and ISettingsScreen, sets the app in constructor and assigns the PageKey. also implement the WaitForLoad() function

~~~csharp
   public class SettingsScreen : AppScreen, ISettingsScreen
    {
        public SettingsScreen(IApp app) : base(app) 
        {
            PageKey = "SettingsPage";            
        }

        public ISettingsScreen WaitForLoad()
        {
            app.WaitForElement(PageContainer);
            return this;
        }
    }
~~~

* Finally, Inject the page test object to FeatureContext in RegisterScreens.cs

~~~csharp
        public static void RegisterScreens(Platform platform, IApp app)
        {
            FeatureContext.Current.Set<IAssociationScreen>(new AssociationScreen(app));
            FeatureContext.Current.Set<ISettingsScreen>(new SettingsScreen(app));
            RegisterScreens(platform);
        }
~~~

## Writing the Test Code

By design, the Main page does not have a page test object. Since the Settings page test object is only used once, it stands to reason to store it in a local variable.

~~~csharp
    public class ShouldNavigateFromMainToSettingsPage : StepsBase
    {
        [Given(@"I am in Main Page")]
        public void GivenIAmInMainPage()
        {
            app.Query(c => c.Marked("TheMainPage")).Length.ShouldEqual(1);
        }

        [Given(@"There is a '(.*)' button on the page")]
        public void GivenThereIsAButtonOnThePage(string settings)
        {
            app.Query(c=>c.Text(settings)).Length.ShouldEqual(1);
        }

        [When(@"I press the '(.*)' button")]
        public void WhenIPressTheButton(string settings0)
        {
            app.Tap(c=>c.Text(settings0));
        }

        [Then(@"the application will Navigate to Settings Page")]
        public void ThenTheApplicationWillNavigateToSettingsPage()
        {
            var page = FeatureContext.Current.Get<ISettingsScreen>();
            page.WaitForLoad();
            app.Query(page.PageContainer).Length.ShouldEqual(1);
        }

    }
~~~
## Writing Code to pass the test

To pass the test, the ViewModel of the Main page needs a command object to capture the tapping of the Settings button and navigating to the Settings page.

~~~csharp
        public ICommand GoForSettings { get; private set; }

        public MainPageVM(INavigationService navigator){
            this.navigator = navigator;
            this.GoForSettings = new RelayCommand(_GoForSettings);
				...
        }

        private void _GoForSettings()
        {
            navigator.NavigateTo(SettingsPage.PageKey);
        }
~~~

# Binding the Settings Page to the Settings Object 

The easy test for this section is to check that there is an element on the Settings page that would change a particular setting:

~~~gherkin
	Then the application will Navigate to Settings Page
    And  the settings page has a button to select Https protocol
~~~

~~~csharp
        [Then(@"the settings page has a button to select Https protocol")]
        public void ThenTheSettingsPageHasAButtonToSelectHttpsProtocol()
        {
            app.Query(page.UseHttpSwitch).Length.ShouldEqual(1);
        }
~~~

## Testing a SwitchCell Setting

To be able to test the last step, the ISettingsScreen and SettingsScreen classes have to be refactored to expose the UseHttpSwitch element. Most unfortunately, the Switch Element's AutomationID is not exposed. The only marker available is the text in the label associated with the switch, but that element is not useful to test the tapping of the switch. 

To make matters worse, the switch in iOS is the sibling of text element, but in android is the sibling of its parent. fortunately, the page test object makes it possible to hide these differences.

~~~csharp
 public Func<AppQuery, AppQuery> UseHttpsSwitch
        {
            get
            {
                if (app is Xamarin.UITest.Android.AndroidApp)
                    return c => c.Text("Use Https").Parent(1).Child(1);
                else 
                    return c => c.Marked("Use Https").Index(1);
            }
        }
~~~

# Xamarin Backdoors
Now that we can tap the switch, we need to ensure that such tapping will change the Setting bound to the switch. i.e.:

~~~gherkin
@Settings_Page
Scenario: Should have a switch to Use Https
    Given I am in Settings page
    And  the settings page has a switch to select Https protocol
    And  the 'UseHttps' Setting is 'true'
    When I tap the switch 
    Then the 'UseHttps' Setting will change to 'false'
~~~

So this tests needs a way to read the app settings, but those are internal to the app and not accessible from outside. for this, one should employ the concept of [Backdoors][], which allow the test platform to execute a function inside the app and return a value.

To define backdoors, Few steps need to be taken.

* Right click on the Preferences directory of the Droid project and go to Edit Preferences, search for "export" and add Mono.Android.Export to the project.

![Export dll](images/Export-Reference.png)

* For the Android Project, in the MainActivity.cs add a method with Export (Java.Interop.Export) annotation:

~~~csharp
        [Export]
        public string ExamineSettings(string key)
        {
            switch (key)
            {
                case "UseHttps":
                    return Helpers.Settings.UseHttps ? "true" : "false";
                case "SetHttps":
                    Helpers.Settings.UseHttps = true;
                    return "true";
                case "ClearHttps":
                    Helpers.Settings.UseHttps = false;
                    return "false";
                default:
                    return "Unknown key " + key;
            }
        }
~~~

* For iOS project, in AppDelegate.cs, add an Export decorated method.

~~~csharp
[Export("ExamineSettings:")]
        public NSString ExamineSettings(NSString key)
        {
            switch(key){
                case "UseHttps":
                    return new NSString(Helpers.Settings.UseHttps ? "true" : "false");
                case "SetHttps":
                    Helpers.Settings.UseHttps = true;
                    return new NSString("true");
                case "ClearHttps":
                    Helpers.Settings.UseHttps = false;
                    return new NSString("false");
                default:
                    return new NSString("Unknown key " + key);
            }
~~~

* Note a few nuances with the iOS annotation:
	* The export key ends with a ":" This is most important.
	* The function always takes one and only one parameter.
	* It returns a **new NSString()**
	* It will show as an empty array in Repl()
	* It should be turned **toString()** before assertions.
	* Its type is Newtonsoft.Json.Linq.JValue

Because of these differences, invoking the backdoor is different between iOS and Android and an Invoke function is needed in StepsBase to encapsulate that:

~~~csharp
        public string Invoke(string id, string param)
        {
            if (app is Xamarin.UITest.Android.AndroidApp)
                return app.Invoke(id, param).ToString();
            return app.Invoke(id + ":", param).ToString();
        }
~~~

## Test steps for Use Https switch

~~~csharp
    public class ShouldHaveASwitchToUseHttps : StepsBase
    {
        ISettingsScreen page;
        [Given(@"I am in Settings page")]
        public void GivenIAmInSettingsPage()
        {
            page = FeatureContext.Current.Get<ISettingsScreen>();
            if(app.Query("SettingsButton").Length > 0)
            {
                app.Tap("SettingsButton");
                page.WaitForLoad();
            }
            app.Query(page.PageContainer).Length.ShouldEqual(1);
        }

        [Given(@"the settings page has a switch to select Https protocol")]
        public void GivenTheSettingsPageHasASwitchToSelectHttpsProtocol()
        {
            app.Query(page.UseHttpsSwitch).Length.ShouldEqual(1);
        }

        [Given(@"the '(.*)' Setting is '(.*)'")]
        public void GivenTheSettingIs(string useHttps, string state)
        {
            Invoke("ExamineSettings", "SetHttps"); //will set the state to true
        }

        [When(@"I tap the switch")]
        public void WhenITapTheSwitch()
        {
            app.Tap(page.UseHttpsSwitch);
        }

        [Then(@"the '(.*)' Setting will change to '(.*)'")]
        public void ThenTheSettingWillChangeTo(string useHttps0, string @false)
        {
            Invoke("ExamineSettings", "UseHttps").ShouldEqual("false");
        }

    }
~~~

The test fails, because the switch and the setting are not connected to each other. So we can create a binding between the switch and its ViewModel

~~~xml
   <SwitchCell Text="Use Https" On="{Binding UseHttps}" AutomationId="UseHttpsSwitch"/>
~~~

In ViewModel, the changes to the UseHttps property should be delegated to Settings object, which will in turn delegate it to AppSettings.

~~~csharp
        public bool UseHttps{
            get => Settings.UseHttps;
            set
            {
                if (Settings.UseHttps == value) return;
                Settings.UseHttps = value;
                RaisePropertyChanged("UseHttps");
            }
        }
~~~

While there is doublebinding between the ViewModel and the Switch element, there is no double binding between the Settings and the Switch element. i.e. if one directly sets Settings.UseHttps to false, the switch won't change, but changing ViewModel.UseHttps to false will change the Switch.

Settings <------- ViewModel <==========> Switch

Therefore, we have to refactor our test to pass:
# Passing Tests
~~~gherkin
@Settings_Page
Scenario: 02 Should have a switch to Use Https
    Given I am in Settings page
    And  the settings page has a switch to select Https protocol
    And  I record the state of the UseHttps Setting
    When I tap the switch 
    Then the UseHttps Setting will toggle
~~~

And the Step code for these would be

~~~csharp
...
        private string InitialUseHttpsStatus;
...
        [Given(@"I record the state of the UseHttps Setting")]
        public void GivenIRecordTheStateOfTheUseHttpsSetting()
        {
            InitialUseHttpsStatus = Invoke("ExamineSettings", "UseHttps");
        }

        [When(@"I tap the switch")]
        public void WhenITapTheSwitch()
        {
            app.Tap(page.UseHttpsSwitch);

        }

        [Then(@"the UseHttps Setting will toggle")]
        public void ThenTheUseHttpsSettingWillToggle()
        {
            app.WaitFor(() => Invoke("ExamineSettings", "UseHttps") ==
                        (InitialUseHttpsStatus == "false" ? "true" : "false")
                        , "Didn't change the Setting");
        }
~~~

Notice the app.WaitFor() call that allows time for the bindings to take effect for change in Switch to result in change in Settings.


[Xam.Plugins.Settings]: https://jamesmontemagno.github.io/SettingsPlugin/GettingStarted.html
[Backdoors]: https://developer.xamarin.com/guides/testcloud/uitest/working-with/backdoors/
