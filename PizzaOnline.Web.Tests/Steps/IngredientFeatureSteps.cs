using NUnit.Framework;
using OpenQA.Selenium;
using PizzaOnline.Api.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PizzaOnline.Web.Tests.Steps
{
    [Binding]
    public class IngredientFeatureSteps 
    {
        [When(@"I enter ingredient data")]
        public void WhenIEnterIngredientData(Table table)
        {
            var ingredients = table.CreateSet<IngredientModel>();
            var nameInput = BaseSteps.Driver.FindElement(By.Name("name"));
            var priceInput = BaseSteps.Driver.FindElement(By.Name("price"));
            foreach(var ingredient in ingredients)
            {
                nameInput.SendKeys(ingredient.Name);
                priceInput.SendKeys(ingredient.Price.ToString());
            }
        }
        
        [When(@"I press add ingredient button")]
        public void WhenIPressAddButton()
        {
            var addButton = BaseSteps.Driver.FindElement(By.Id("add_ingredient"));
            addButton.Click();
        }

        [Then(@"The ingredient list contain item")]
        public void ThenTheListContainItem()
        {
            var list = BaseSteps.Driver.FindElement(By.Id("ingredients_table"));
            var items = list.FindElements(By.TagName("tr"));

            Assert.That(items.Count, Is.GreaterThan(1));
        }


    }
}
