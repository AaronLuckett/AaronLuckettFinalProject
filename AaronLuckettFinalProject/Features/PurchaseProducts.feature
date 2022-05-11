@Purchase
Feature: Purchasing products

As a user I want to be able apply coupons and check the two order numbers

Background:
	Given I have logged in
	And I am on the main shop page


@ApplyCoupon
Scenario: ApplyCoupon
	When I add an item to my cart
	When I enter the coupon code 'edgewords'
	Then a discount of '15'% should be applied

@CorrectDeduction
Scenario: CorrectDeduction
	When I add an item to my cart
	When I enter the coupon code 'edgewords'
	Then The final cost should equal the item price minus the discount value

	@Purchase
Scenario: CheckOrderNumber
	When I add an item to my cart
	When I Complete the purchase by entering these details
	| firstname | lastname | address              | city  | postcode | phoneNumber   |
	| Aaron     | Luckett  | 65 Westbridge Street | Derby | DE22 3GG | 07950 888 888 |
	Then The order number recevied after purachse should match the order number in order history

