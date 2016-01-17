using System;
using System.Configuration;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;

namespace PizzaOnline.Web.Tests.Steps
{
    [Binding]
    public class BaseSteps
    {
        public static FirefoxDriver Driver;
        private string WebUrl { get; } = ConfigurationManager.AppSettings["TestedWebUrl"];

        [BeforeFeature()]
        public static void BeforeFeature()
        {
            Driver = new FirefoxDriver(/*new FirefoxBinary(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe"), new FirefoxProfile()*/);
        }

        [AfterFeature()]
        public static void AfterFeature()
        {
            Driver.Close();
        }

        [Given(@"I have navigated to (.*) page")]
        public void GivenINavigatedToApplicationPage(string page)
        {
            Driver.Navigate().GoToUrl($"{WebUrl}/{page}");
        }

        [Given(@"I wait (.*) seconds")]
        [Then(@"I wait (.*) seconds")]
        public void GivenIWaitSeconds(int p0)
        {
            Thread.Sleep(TimeSpan.FromSeconds(p0));
        }

        [Then(@"Should go to (.*)")]
        public void ThenShouldGoToPizza(string p0)
        {
            Assert.That(Driver.Url, Is.EqualTo($"{WebUrl}/{p0}"));
        }

    }
}
