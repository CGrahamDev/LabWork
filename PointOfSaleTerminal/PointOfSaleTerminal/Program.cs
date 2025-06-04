/*
Write a cash register or self-service terminal for some kind of retail location.  Obvious choices include a small store, a coffee shop, or a fast food restaurant.
-Your solution must include some kind of a product class with a name, category, description, and price for each item. 
-12 items minimum; stored in a list.
-Present a menu to the user and let them choose an item (by number or letter).
    -Allow the user to choose a quantity for the item ordered.
    -Give the user a line total (item price * quantity).
-Either through the menu or a separate question, allow them to re-display the menu and to complete the purchase.
-Give the subtotal, sales tax, and grand total.  (Remember rounding issues the Math library will be handy!)
-Ask for payment type—cash, credit, or check
-For cash, ask for amount tendered and provide change.
-For check, get the check number.
-For credit, get the credit card number, expiration, and CVV.
-At the end, display a receipt with all items ordered, subtotal, grand total, and appropriate payment info.
-Return to the original menu for a new order.  (Hint: you’ll want an array or List to keep track of what’s been ordered!)

Optional enhancements:
-(Moderate) Store your list of products in a text file and then include an option to add to the product list, which then outputs to the product file.
-(Buff) Do a push up every time you get an exception or error while running your code
*/
//Consider creating an Account class which will hold stock (food) information as well as expenses and costs for a specific session 
//Consider adding a session start time/date and session end time/date


using Microsoft.Win32.SafeHandles;
using PointOfSaleTerminal.ProductLogic;
using PointOfSaleTerminal.StoreLogic;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

//Fast Food - Chelsea's Comically Collossal Chicken

string fileName = "food-menu";

List<Product> defaultMenu = new List<Product>()
{
    //ADD BASIC ITEMS THAT WILL ALWAYS BE IN THE MENU

    //Entrees
    new Product("Classic Chicken Sandwhich", 0, "Our classic collosal chicken sandwhich w/ tomatoes, onion, lettuce and mayo ", 3.99m ),
    new Product("Spicy Chicken Sandwhich", 0, "Our classic collosal chicken sandwhich w/ tomatoes, onions, lettuce, mayo and a spicy twist", 3.99m),
    new Product("Classic Boneless Tenders",0,"The behemoth beloved boneless wings you know and love /8x ct.",7.99m),
    new Product("Spicy Boneless Tenders",0,"The behemoth beloved boneless wings you know and love w/ a spicy twist /8x ct.",7.99m),
    new Product("Classic Bone-In Breasts",0,"Our famous massive chicken breast seasoned w/ our 12 herbs and spices sprinkled w/ a lil' lemon /2lb.",11.99m),
    new Product("Spicy Bone-In Breasts",0,"Our famous massive chicken breast seasoned w/ our 12 herbs and spices sprinkled w/ a lil' lemon and cajun seasoning /2lb.",11.99m),

    //SIDES 
    new Product("Classic Chicken Fingers", 1, "Our classically crafted chicken fingers, fun for the whole family /12x ct.", 2.99m),
    new Product("Spicy Chicken Fingers", 1, "Our classically crafted chicken fingers, fun for the whole family with an added spice /12x ct.", 2.99m),
    new Product("Southern Lousiana Fries", 1, "Our fries seasoned with a recipe borrowed from a certain sister company", 1.99m),
    new Product("Cajun Fries", 1, "Spicy, delicious fries seasoned to perfection", 1.99m),
    new Product("Collosal Chicken Nuggets", 1, "4 MASSIVE chicken nuggets sold at an amazing value", 1.99m),

    //VALUE ITEMS
    new Product("Value Wings",2,"Beautifully affordable and delicious wings",1.29m),
    new Product("Spicy Value Wings",2,"Beautifully affordable and delicious wings w/ a lemony spice added",1.29m),
    new Product("Normal Sized Chicken Sandwhich",2,"Our NORMAL sized chicken sandwhich w/ mayo",1.99m),
    new Product("Spicy Normal Chicken Sandwhich",2,"Our NORMAL sized chicken sandwhich w/ mayo and a spicy kick",1.99m),
    new Product("Chicken Burria Tacos",2,"A little something we learned from our friends down south ;)",1.49m),
    //BEVS
    new Product("Sprite",3,"A lemon-twist soda",0.99m),
    new Product("Coca Cola",3,"Classic Coke",0.99m),
    new Product("Raspberry Lemonade",3,"Lemonade w/ Raspberry flavoring",0.99m),
    new Product("Lemonade",3,"Lemonade, great for summer days",0.99m),
    new Product("Water",3,"refreshing!",0.99m),
    //DESSERTS
    new Product("Chicken Flavoureed Ice Cream",4,"Honestly really gross, but it's on theme and for some reason it sells okay",2.19m),
    new Product("Vanilla Chip Chocolate Ice Cream",4,"refreshing with a rich and delicious taste",3.49m),
    new Product("Strawberry Cheesecake",4,$"Strawberry cheesecake prepared by hand in-house and served fresh every day {9.99m:c}/full cake(8 slices) ",1.49m),
    new Product("Vanilla Milkshake",4,"A milkshake made w/ real vanilla and Whole Milk",2.49m),
    new Product("Banana Milkshake",4,"A milkshake made w/ real vanilla, bananas and Whole Milk",2.49m),
};
StoreFront chelseasChickenStore = new StoreFront("Chelsea's Comically Collosal Chicken");

string[] consoleMenuOptions = new string[5]
{
    "Order Food",
    "Display Cart",
    "Empty Cart",
    "Check Out",
    "Exit The Application",
};
const int adminCode = 958627;


try
{
    StreamReader reader = new StreamReader($"{fileName}.txt");
    reader.Close();
    if (chelseasChickenStore.ReadMenuFile(fileName))
    {
        Console.WriteLine("Menu loaded!");
    }
    else
    {
        throw new FileNotFoundException();
    }

}
catch (FileNotFoundException)
{
    StreamWriter writer = new StreamWriter($"{fileName}.txt", false);
    //in the case of a file not found exception use a default list of items that'd appear on the menu
    chelseasChickenStore.Menu.AddRange(defaultMenu);
    chelseasChickenStore.UpdateMenuFile(fileName);
    writer.Close();

}

bool isContinuing = true;
//One of a few nav screens intended (Admin, Edit(edit menu items), Menu/Customer,)
do
{
    
    Console.Clear();
    ConsoleMenuNavigation();
    Console.Clear();

}while(isContinuing);
/* test method
Console.WriteLine($"{doubleChickenMeal.ToString()}");
*/

Console.ReadKey();








//METHODS
//make methods that will automatically format the strings based on the center length and wiill take in string params: e.g. quartCenterLength method will take in two string params to be offset and printed by and one string param to decide what will be p[laced in the new string() method and will format the two strings



//will contain everything needed to navigate the menu
void ConsoleMenuNavigation()
{
    string prompt = "";
    StoreFront.DisplayMax('-');
    string storeWelcome = "Welcome to Chelsea's Comically Collossal Chicken";
    StoreFront.DisplayDeadCenter(' ', storeWelcome);
    StoreFront.DisplayMax('-');

    while (true)
    {
        string consoleMenuPrompt = "Enter the number for the console menu option: ";
        StoreFront.DisplayDeadCenter(' ', consoleMenuPrompt);
        for (int i = 0; i < consoleMenuOptions.Length; i++)
        {
            string menuOption = $"{i + 1}: {consoleMenuOptions[i]} ";
            StoreFront.DisplayDeadCenter(' ', menuOption);
        }
        StoreFront.DisplayMax('-');
        Console.Write(new string(' ', StoreFront.menuHalfCenterLength));
        if (int.TryParse(Console.ReadLine(), out int selectedMenuOption))
        {
            Console.Clear();
            try { Console.WriteLine($"You have selected {consoleMenuOptions[selectedMenuOption - 1]}"); }
            catch (IndexOutOfRangeException)
            {
                if (selectedMenuOption == adminCode)
                {
                    selectedMenuOption = adminCode;
                }
                else
                {
                    Console.WriteLine("Incorrect input! Please select a corresponding option on the menu");
                    continue;
                }
            }
            Console.WriteLine("Press any key to continue:");
            Console.ReadKey();
            Console.Clear();
            switch (selectedMenuOption)
            {
                case 1: // Adding items and combos to cart
                    chelseasChickenStore.OrderFood();
                    break;
                case 2: // displaying cart 
                    chelseasChickenStore.DisplayCart();
                    break;
                case 3: //Cart clearing
                    prompt = "Are you sure? This will empty your cart!";
                    StoreFront.DisplayMax('-');
                    StoreFront.DisplayDeadCenter(' ', prompt);
                    string userAnswer = AnswerYOrN();
                    if (userAnswer == "y")
                    {
                        chelseasChickenStore.ClearCart();
                        break;
                    }
                    else
                    {
                        continue;
                    }
                case 4: //Checking out
                    chelseasChickenStore.CheckOut();
                    break;
                case 5:
                    isContinuing = false;
                    break;
                case adminCode:
                    AdminMenu();
                    break;
            }

        }
        break;
    }
}

void AdminMenu()
{
    throw new NotImplementedException();
}




//will contain all of the necessary food options for ordering and provide the ability to order.


string AnswerYOrN()
{
    while (true)
    {
        int i = 0;
        StoreFront.DisplayMax('-');
        string prompt = "Answer \"y\" or \"n\"";
        StoreFront.DisplayDeadCenter(' ', prompt);
        StoreFront.DisplayMax('-');
        Console.Write(new string(' ', StoreFront.menuHalfCenterLength));
        string answer = Console.ReadLine().ToLower();

        switch (answer)
        {
            case "y":
            case "n":
                return answer;
                break;
            default:
                Console.WriteLine("Invalid input: please enter \"y\" or \"n\"");
                i++;
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                continue;
        }
    }
}
