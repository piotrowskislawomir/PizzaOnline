using System.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using PizzaOnline.Api.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PizzaOnline.Web.Tests.Steps
{
    [Binding]
    public class IngredientFeatureSteps
    {
        // private ChromeDriver _driver;
        private FirefoxDriver _driver;
         private readonly string _webUrl;

        public IngredientFeatureSteps()
        {
            _webUrl = ConfigurationManager.AppSettings["TestedWebUrl"];
        }
        
        [Given(@"I have started web browser")]
        public void GivenIHaveStartedBrowser()
        {
            _driver = new FirefoxDriver(/*new FirefoxBinary(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe"), new FirefoxProfile()*/);
            //_driver = new ChromeDriver();
        }

        [Given(@"I have navigated to (.*) page")]
        public void GivenINavigatedToApplicationPage(string page)
        {
            _driver.Navigate().GoToUrl($"{_webUrl}/{page}");
        }
        [When(@"I enter ingredient data")]
        public void WhenIEnterIngredientData(Table table)
        {
            var ingredients = table.CreateSet<IngredientModel>();
            var nameInput = _driver.FindElement(By.Name("name"));
            var priceInput = _driver.FindElement(By.Name("price"));
            foreach(var ingredient in ingredients)
            {
                nameInput.SendKeys(ingredient.Name);
                priceInput.SendKeys(ingredient.Price.ToString());
            }
        }
        
        [When(@"I press add button")]
        public void WhenIPressAddButton()
        {
            var addButton = _driver.FindElement(By.Id("add_ingredient"));
            addButton.Click();
        }
        [Then(@"The list contain item")]
        public void ThenTheListContainItem()
        {
            var list = _driver.FindElement(By.Id("ingredients_table"));
            var items = list.FindElements(By.TagName("tr"));

            Assert.That(items.Count, Is.GreaterThan(1));
        }


    }
}
