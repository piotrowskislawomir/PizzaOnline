Feature: IngredientFeature

Scenario: Add ingredient
	Given I have started web browser
	And I have navigated to Ingredient page
	When I enter ingredient data
	| Name        | Price |
	| ingredient1 | 12.56 |
	And I press add button 
	Then The list contain item
