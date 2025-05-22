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


using PointOfSaleTerminal.ProductLogic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

StoreFront chelseasChickenStore = new StoreFront();

string[] menuOptions = new string[4]
{
    "Order Food",
    "Display Cart",
    "Empty Cart",
    "Check Out",
};
const int adminCode = 958627;


try
{
    StreamReader reader = new StreamReader("menu.txt");
    reader.Close();
}
catch (FileNotFoundException)
{
    StreamWriter writer = new StreamWriter("menu.txt");
    writer = new StreamWriter("store_info.txt");
    writer.Close();

}

//test values
//Product classicChickenSandwhich = new Product("Classic Chicken Sandwhich", 1, "Our classic collosal chicken sandwhich with tomatoes, onions, and lettuce", 3.99m );
//MealDeal doubleChickenMeal = new MealDeal("Chicken Fry Meal", classicChickenSandwhich, classicChickenSandwhich );

//chelseasChickenStore.RemoveFromMenu(classicChickenSandwhich);

//One of a few nav screens intended (Admin, Edit(edit menu items), Menu/Customer,)
ConsoleMenuNavigation();

/* test method
Console.WriteLine($"{doubleChickenMeal.ToString()}");
*/

Console.ReadKey();


//Fast Food - Chelsea's Comically Collossal Chicken



































//METHODS




void UpdateMenuFile()
{
    throw new NotImplementedException();
    /*TEST VALUES
     * StreamWriter writer = new StreamWriter("menu.txt", true);
    writer.WriteLine(classicChickenSandwhich.ToString);
    writer.Close();
    */
}



//will contain everything needed to navigate the menu
void ConsoleMenuNavigation()
{
    Console.WriteLine("Welcome to Chelsea's Comically Collossal Chicken");
    while (true)
    {
        bool validMenuOption = false;
        Console.WriteLine($"Enter the number for the menu option:");
        for (int i = 0; i < menuOptions.Length; i++)
        {
            Console.WriteLine($"{i+1}: {menuOptions[i]}");
        }
        if (int.TryParse(Console.ReadLine(), out int menuOption))
        {
            try { Console.WriteLine($"You have selected {menuOptions[menuOption]}"); }
            catch (IndexOutOfRangeException)
            {
                if (menuOption == adminCode)
                {
                    menuOption = adminCode;
                }
                else
                {
                    Console.WriteLine("Incorrect input! Please select a corresponding option on the menu");
                    continue;
                }
            }
            switch (menuOption)
            {
                case 1: // Adding items and combos to cart
                    chelseasChickenStore.OrderFood();
                    break;
                case 2: // displaying cart 
                    chelseasChickenStore.DisplayCart();
                    break;
                case 3: //Cart clearing
                    Console.WriteLine("Are you sure? This will empty your cart!");
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
                case adminCode:
                    throw new NotImplementedException();
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
