@Purchase
Feature: Purchasing products

As a user I want to be able apply coupons and check order numbers

Background:
	Given I have logged in
	And I am on the main shop page
	When I add an item to my cart
	When I enter the coupon code 'edgewords'

@ApplyCoupon
Scenario: ApplyCoupon
	Then a discount of '15'% should be applied

@CorrectDeduction
Scenario: CorrectDeduction
	Then The final cost should equal the item price minus the discount value

