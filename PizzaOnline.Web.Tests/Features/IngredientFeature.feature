Feature: IngredientFeature

Scenario: Add ingredient
	Given I have navigated to Ingredient page
	And I wait 5 seconds
	When I enter ingredient data
	| Name        | Price |
	| ingredient1 | 12.56 |
	And I press add ingredient button 
	Then I wait 5 seconds
	And The ingredient list contain item
