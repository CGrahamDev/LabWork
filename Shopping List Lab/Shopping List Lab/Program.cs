/*
 What will the application do?
-Display at least 8 item names and prices.
-Ask the user to enter an item name
-If that item exists, display that item and price and add that item to the user’s order.
-If that item doesn’t exist, display an error and re-prompt the user.  (Display the menu again if you’d like.)
-After adding one to their order, ask if they want to add another. If so, repeat.  (User can enter an item more than once; we’re not keeping track of quantity at this point.)
-When they’re done adding items, display a list of all items ordered with prices in columns.
-Display the sum price of the items ordered.

Build Specifications/Grading Points
-Application uses a Dictionary<string, decimal> to keep track of the menu of items.  You can code it with literals. (2 points for instantiation & initialization)
-Use a List<string> for the shopping list and store the name of the items the customer ordered.
-Application takes item name input and:
-Responds if that item doesn’t exist (1 point)
-Adds its name to the relevant List if it does (1 point)
-Application asks user if they want to quit or continue, and loops appropriately (1 point)
-Application displays list of items with their prices (2 points)
-Application displays the correct total receipt for the list (1 point)
To determine the sum: Loop through the shopping list’s List collection, obtain the name, and look up the name’s price in the Menu Dictionary.

Extended Challenges:
Implement a menu system so the user can enter just a letter or number for the item.
Display the most and least expensive item ordered.
Display the items ordered in order of price.

 */

//COME BACK LATER ONCE I HAVE A BETTER UNDERSTANDING OR ARRAYS AND LISTS.
Dictionary<string,double> itemsForSale = new Dictionary<string, double>()
{
    {"chips",2.50},
    {"soda",1.37 },
    {"peanuts",0.99},
    {"gummies",2.85 },
    {"lighter",3.00 },
    {"gum",0.50},
    {"popcorn",2.29 },
    {"ramen",0.59}
    
};
Console.WriteLine("Welcome! The store is selling: ");
foreach(KeyValuePair<string,double> itemPair in itemsForSale )
{
    Console.WriteLine($"{itemPair.Key} at {itemPair.Value:c},");
}
bool willAddMoreItems = true;

//Shoppin list where added items will go.
List<string> shoppingList = new List<string>();



string userItemInput = "";
string itemNameKey = "";
do
{
    bool isValidItem = false;
    do
    {
        
        Console.Clear();
        Console.WriteLine("Enter the name of any item you would like to buy.");
        userItemInput = Console.ReadLine().ToLower();
        itemNameKey = "";
        bool doesItemExist = itemsForSale.ContainsKey(userItemInput);
        
        if (doesItemExist == true)
        {
            isValidItem = true;
            itemNameKey = userItemInput;
            Console.WriteLine($"You have selected {itemNameKey} and it costs {itemsForSale[itemNameKey]:c} ");
            shoppingList.Add(itemNameKey);
        }
        else
        {
            Console.Clear();
            Console.WriteLine("The item you entered doesn't exist please enter the names of one of these items.");
            foreach (KeyValuePair<string, double> itemPair in itemsForSale)
            {
                Console.WriteLine($"{itemPair.Key} at {itemPair.Value:c}");

            }
            Console.WriteLine("press any key to continue.");
            Console.ReadKey();
        }
    } while (isValidItem == false);
    
    Console.WriteLine("Would you like to add another item?");
    string userAnswer = AnswerYOrN();
    if (userAnswer == "y")
    {
        continue;
    } else if (userAnswer == "n")
    {
        break;
    }


} while (true);
double totalPrice = 0;
int itemCount = shoppingList.Count;
//debug cw for now
foreach (string shoppedItem in shoppingList)
{
    Console.WriteLine($"{shoppedItem, 8}{' ',5} {"----------------------------------",5} {' ',5}   {itemsForSale[shoppedItem]:c}");
    totalPrice += itemsForSale[shoppedItem];

}

Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine($"Your total price is {totalPrice:c}. ");
Console.ReadKey();
Environment.Exit(0);








//classes cannot be private


string AnswerYOrN()
{
    while (true)
    {
        Console.WriteLine("Answer \"y\" or \"n\"");
        string answer = Console.ReadLine().ToLower();
        switch (answer)
        {
            case "y":
            case "n":
               return answer;
                break;
            default:
                Console.WriteLine("Invalid input: please enter \"y\" or \"n\"");
                continue;
        }
    }
}
