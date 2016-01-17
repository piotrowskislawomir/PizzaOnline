using OpenQA.Selenium;
using PizzaOnline.Api.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PizzaOnline.Web.Tests.Steps
{
    [Binding]
    public sealed class PizzaFeatureSteps
    {
        [When(@"I enter pizza data")]
        public void WhenIEnterPizzaData(Table table)
        {
            var pizzas = table.CreateSet<PizzaModel>();
            var nameInput = BaseSteps.Driver.FindElement(By.Id("new_pizza_name"));
            foreach (var pizza in pizzas)
            {
                nameInput.SendKeys(pizza.Name);
            }
        }

        [When(@"I press add pizza button")]
        public void WhenIPressAddPizzaButton()
        {
            var addButton = BaseSteps.Driver.FindElement(By.Id("add_new_pizza_to_menu"));
            addButton.Click();
        }

        [When(@"I have selected ingredients")]
        public void WhenIHaveSelectedIngredients()
        {
            var table = BaseSteps.Driver.FindElement(By.Id("ingredients_table_to_compose"));
            var inputs = table.FindElements(By.TagName("input"));

            int count = 0;
            foreach (var input in  inputs)
            {
                if (input.GetAttribute("type") == "checkbox")
                {
                    input.Click();
                    count++;
                }
                if (count == 2)
                    break;
            }
        }

    }
}
