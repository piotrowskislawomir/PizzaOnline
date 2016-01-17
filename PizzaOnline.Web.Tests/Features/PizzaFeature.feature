Feature: PizzaFeature


Scenario: Add Pizza
	Given I have navigated to Pizza/Compose page
	And I wait 5 seconds
	When I enter pizza data
	| Name      | 
	| pizzaName | 
	And I have selected ingredients
	And I press add pizza button 
	Then I wait 5 seconds
	And Should go to Pizza
	

