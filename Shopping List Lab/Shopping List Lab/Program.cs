/*
 What will the application do?
-Display at least 8 item names and prices.
-Ask the user to enter an item name
If that item exists, display that item and price and add that item to the user’s order.
If that item doesn’t exist, display an error and re-prompt the user.  (Display the menu again if you’d like.)
After adding one to their order, ask if they want to add another. If so, repeat.  (User can enter an item more than once; we’re not keeping track of quantity at this point.)
When they’re done adding items, display a list of all items ordered with prices in columns.
Display the sum price of the items ordered.

Build Specifications/Grading Points
Application uses a Dictionary<string, decimal> to keep track of the menu of items.  You can code it with literals. (2 points for instantiation & initialization)
Use a List<string> for the shopping list and store the name of the items the customer ordered.
Application takes item name input and:
Responds if that item doesn’t exist (1 point)
Adds its name to the relevant List if it does (1 point)
Application asks user if they want to quit or continue, and loops appropriately (1 point)
Application displays list of items with their prices (2 points)
Application displays the correct total receipt for the list (1 point)
To determine the sum: Loop through the shopping list’s List collection, obtain the name, and look up the name’s price in the Menu Dictionary.

Extended Challenges:
Implement a menu system so the user can enter just a letter or number for the item.
Display the most and least expensive item ordered.
Display the items ordered in order of price.

 */

//COME BACK LATER ONCE I HAVE A BETTER UNDERSTANDING OR ARRAYS AND LISTS.
Dictionary<string,double> itemsForSale = new Dictionary<string, double>()
{
    {"Chips",2.50},
    {"Soda",1.37 },
    {"Peanuts",0.99},
    {"Gummies",2.85 },
    {"Lighter",3.00 },
    {"Gum",0.50},
    {"Popcorn",2.29 },
    {"Ramen",0.59}
    
};
Console.WriteLine("Welcome! The store is selling: ");
foreach(KeyValuePair<string,double> itemPair in itemsForSale )
{
    Console.WriteLine($"{itemPair.Key} at {itemPair.Value:c},");
}
    Console.WriteLine("Enter the name of any item you would like to buy.");
string userItemInput = Console.ReadLine();

//Shoppin list where added items will go.
Dictionary<string, double> shoppingList = new Dictionary<string, double> {};



string itemNameKey = "";
bool doesItemExist = itemsForSale.ContainsKey(userItemInput);
if (doesItemExist == true)
{
    itemNameKey = userItemInput;
    Console.WriteLine($"You have selected {itemNameKey} ");
} else
{
    Console.Clear();
    Console.WriteLine("The item you entered doesn't exist please enter one of these items.");
    foreach(KeyValuePair<string, double> itemPair in itemsForSale)
    {
        Console.WriteLine($"{itemPair.Key} at {itemPair.Value:c}");
    }
}


//classes cannot be private