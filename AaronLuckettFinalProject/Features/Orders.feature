@Orders
Feature: Orders

As a user I would like to keep track of orders by having orders numbers correclating to
specific purchases

Background:
	Given I have logged in
	And I am on the main shop page
	When I add an item to my cart

@Purchase
Scenario: CheckOrderNumber
	When I Complete the purchase by filling in my correct details
	Then The order number recevied after purachse should match the order number in order history