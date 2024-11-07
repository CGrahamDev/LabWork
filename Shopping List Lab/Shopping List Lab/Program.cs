/*
 *Objectives: Generic Collections, Data Processing

Task: Make a shopping list application which uses collections to store your items. (You will be using two collections, one for the menu and one for the shopping list.)

What will the application do?
-Display at least 8 item names and prices.
-Ask the user to enter an item name
-If that item exists, display that item and price and add that item to the user’s order.
-If that item doesn’t exist, display an error and re-prompt the user.  (Display the menu again if you’d like.)
After adding one to their order, ask if they want to add another. If so, repeat.  (User can enter an item more than once; we’re not keeping track of quantity at this point.)
When they’re done adding items, display a list of all items ordered with prices in columns.
Display the sum price of the items ordered.

Build Specifications/Grading Points
-Application uses a Dictionary<string, decimal> to keep track of the menu of items.  You can code it with literals. (2 points for instantiation & initialization)
-Use a List<string> for the shopping list and store the name of the items the customer ordered.
-Application takes item name input and:
-Responds if that item doesn’t exist (1 point)
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
bool addAnother = false;

Dictionary<string, double> items = new Dictionary<string, double>()
{
    {"gum",1.50 },
    {"soda",2.50 },
    {"water",2.0 },
    {"skittles",1.5 },
    {"root beer", 2.50 },
    {"gatorade", 2.50 },
    {"snickers",1.50 },
    {"reese",1.50 }
};

List<string> shoppingList = new List<string>();
double totalPrice = 0;
do
{
    Console.WriteLine("Scott's Vending Machine");
    Console.WriteLine("Here are your options");
    foreach (var item in items)
    {
        Console.WriteLine($"{item.Key} - {item.Value:c}");

    }

    Console.WriteLine("What would you like to buy?");

    string selectedItem = Console.ReadLine();

    bool doesItemExist = items.ContainsKey(selectedItem);

    while (doesItemExist == false)
    {
        Console.WriteLine("Selected item does not exist. Please try again.");
        selectedItem = Console.ReadLine();
        doesItemExist = items.ContainsKey(selectedItem);
    }

    // a valid item has been selected
    // add to order 
    shoppingList.Add(selectedItem);

    Console.WriteLine($"You have ordered {selectedItem} for {items[selectedItem]}");
    Console.WriteLine("Would you like to continue? (Add/Stop)");
    string userInput = Console.ReadLine();
    addAnother = userInput.ToLower().Trim() == "add";
} while (addAnother == true);

Console.Clear();
Console.WriteLine("Here's what you ordered");
foreach(string item in shoppingList)
{
    //:c format token
    Console.WriteLine($"{item} {items[item]:c}");
    totalPrice += items[item];
}
Console.WriteLine($"Total Price: {totalPrice:c}");
Console.ReadKey();