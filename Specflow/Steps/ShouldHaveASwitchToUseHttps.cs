﻿using System;
using Should;
using Specflow.Screens;
using TechTalk.SpecFlow;

namespace Specflow.Steps
{
    [Binding]
    public class ShouldHaveASwitchToUseHttps : StepsBase
    {
        ISettingsScreen page;
        private string InitialUseHttpsStatus;

        [Given(@"I am in Settings page")]
        public void GivenIAmInSettingsPage()
        {
            page = FeatureContext.Current.Get<ISettingsScreen>();
            if (app.Query("SettingsButton").Length > 0)
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

    }
}
