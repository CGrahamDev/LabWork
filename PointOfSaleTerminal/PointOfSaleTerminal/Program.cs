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
using System.Diagnostics.Contracts;
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
    new Product("Strawberry Cheesecake",4,$"Strawberry cheesecake prepared by hand in-house and served fresh every day {9.99:c}/full cake(8 slices) ",1.49m),
    new Product("Vanilla Milkshake",4,"A milkshake made w/ real vanilla and Whole Milk",2.49m),
    new Product("Banana Milkshake",4,"A milkshake made w/ real vanilla, bananas and Whole Milk",2.49m),
};
StoreFront chelseasChickenStore = new StoreFront("Chelsea's Comically Collosal Chicken");

string[] consoleMenuOptions = new string[4]
{
    "Order Food",
    "Display Cart",
    "Empty Cart",
    "Check Out",
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


//One of a few nav screens intended (Admin, Edit(edit menu items), Menu/Customer,)
ConsoleMenuNavigation();

/* test method
Console.WriteLine($"{doubleChickenMeal.ToString()}");
*/

Console.ReadKey();








//METHODS




//will contain everything needed to navigate the menu
void ConsoleMenuNavigation()
{
    Console.WriteLine("Welcome to Chelsea's Comically Collossal Chicken");

    while (true)
    {
        bool validMenuOption = false;
        Console.WriteLine($"Enter the number for the console menu option:");
        for (int i = 0; i < consoleMenuOptions.Length; i++)
        {
            Console.WriteLine($"{i + 1}: {consoleMenuOptions[i]}");
        }
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
                    OrderFood();
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

//Overarching method which will allow the user to View the menu, select items to see more info and add to cart by quantity

void OrderFood()
{
    int offset = 0;
    int selectedFood = -1;
    bool continueVariable = true;
    bool isValidNumber = false;
    string introText = "Enter a number to select the desired menu item";
    do
    {

        //will call the menu display and will give options of selection and purchase;
        chelseasChickenStore.DisplayMenu();
        //Will display each menu item in the menu List and assign a number it can be selected by. Will then offer the user to add to cart (by quantity) or return to the menu
        Console.WriteLine(new string('-', StoreFront.menuMaxLength) );
        offset = introText.Length / 2;
        Console.WriteLine(new string(' ',StoreFront.menuDeadCenterLength - offset) + $"{introText}" + new string(' ', StoreFront.menuDeadCenterLength - offset));
        Console.WriteLine(new string('-', StoreFront.menuMaxLength));
        Console.Write(new string(' ', StoreFront.menuDeadCenterLength));
        if (isValidNumber = int.TryParse(Console.ReadLine(), out selectedFood))
        {
            selectedFood -= 1;
            try
            {
                Product selectedProduct = chelseasChickenStore.Menu[selectedFood];
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"Argument out of range. Please enter a number 1-{chelseasChickenStore.Menu.Count}");
                Console.WriteLine("Press any key to continue!");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                continue;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine($"Argument out of range. Please enter a number 1-{chelseasChickenStore.Menu.Count}");
                Console.WriteLine("Press any key to continue!");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                continue;
            }
            Console.Clear();
            Console.WriteLine("\x1b[3J");

            do
            {
                chelseasChickenStore.SelectMenuItems(selectedFood);
                string cartActionString = $"[1] Add To Cart ";
                string returnActionString = $"[2] Return To Menu ";
                string[] secondaryMenuActions =
                {
                cartActionString,
                returnActionString
                };

                Console.WriteLine(new string('-', StoreFront.menuMaxLength));
                offset = cartActionString.Length / 2;
                Console.Write(new string(' ', StoreFront.menuQuarterCenterLength - offset) + $"{cartActionString}" + new string(' ', StoreFront.menuQuarterCenterLength - offset));
                offset = returnActionString.Length / 2;
                Console.WriteLine(new string(' ', StoreFront.menuQuarterCenterLength - offset) + $"{returnActionString}" + new string(' ', StoreFront.menuQuarterCenterLength - offset));
                int secondActionIndex = -1;
                continueVariable = true;
                Console.WriteLine(new string('-', StoreFront.menuMaxLength));
                Console.Write(new string(' ', StoreFront.menuDeadCenterLength));
                if (int.TryParse(Console.ReadLine(), out secondActionIndex))
                {
                    secondActionIndex -= 1;
                    try
                    {
                        Console.Clear();
                        _ = secondaryMenuActions[secondActionIndex];

                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("Invalid argument please enter 1 or 2.");
                        Console.Write("Press any key to continue: ");
                        Console.ReadKey();
                        continue;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("Invalid index please enter 1 or 2.");
                        Console.Write("Press any key to continue: ");
                        Console.ReadKey();
                        continue;
                    }
                    continueVariable = false;
                    switch (secondActionIndex)
                    {
                        case 0:
                            do
                            {
                                int thirdActionIndex = -1;
                                string[] addToCartOptions = new string[]
                                {
                                "[1] Add One ",
                                "[2] Add More ",
                                "[3] Return ",
                                };
                                Console.Clear();
                                chelseasChickenStore.SelectMenuItems(selectedFood);
                                Console.WriteLine(new string('-', StoreFront.menuMaxLength));
                                offset = addToCartOptions[0].Length / 2;
                                Console.Write(new string(' ', StoreFront.menuQuarterCenterLength - offset) + $"{addToCartOptions[0]}" + new string(' ', StoreFront.menuQuarterCenterLength - offset));
                                offset = addToCartOptions[1].Length / 2;
                                Console.WriteLine(new string(' ', StoreFront.menuQuarterCenterLength - offset) + $"{addToCartOptions[1]}" + new string(' ', StoreFront.menuQuarterCenterLength - offset));
                                offset = addToCartOptions[2].Length / 2;
                                Console.WriteLine(new string(' ', StoreFront.menuQuarterCenterLength) + new string('-', StoreFront.menuDeadCenterLength) + new string(' ', StoreFront.menuQuarterCenterLength));
                                Console.WriteLine(new string(' ', StoreFront.menuDeadCenterLength - offset) + $"{addToCartOptions[2]}" + new string(' ', StoreFront.menuDeadCenterLength - offset));
                                Console.WriteLine(new string('-', StoreFront.menuMaxLength));
                                Console.Write(new string(' ', StoreFront.menuDeadCenterLength));
                                if (int.TryParse(Console.ReadLine(), out thirdActionIndex))
                                {
                                    thirdActionIndex -= 1;
                                    try
                                    {
                                        _ = addToCartOptions[thirdActionIndex];
                                    }
                                    catch (ArgumentOutOfRangeException)
                                    {
                                        Console.WriteLine($"Invalid Input! Please enter an input between 1 and {addToCartOptions.Length}");
                                        Console.WriteLine("Press any key to continue");
                                        Console.ReadKey();
                                        Console.Clear();
                                        continue;
                                    }
                                    catch (IndexOutOfRangeException)
                                    {
                                        Console.WriteLine($"Invalid Input! Please enter an input between 1 and {addToCartOptions.Length}");
                                        Console.WriteLine("Press any key to continue");
                                        Console.ReadKey();
                                        Console.Clear();
                                        continue;
                                    }
                                    switch (thirdActionIndex)
                                    {
                                        case 0:
                                            chelseasChickenStore.AddToCart(chelseasChickenStore.Menu[selectedFood]);
                                            string confirmationText = $"{chelseasChickenStore.Menu[selectedFood].Name} has been added to your cart! Your cart now has {chelseasChickenStore.Cart.Count} items!";
                                            offset = confirmationText.Length / 2;
                                            Console.WriteLine(new string(' ', StoreFront.menuDeadCenterLength - offset) + $"{confirmationText}" + new string(' ', StoreFront.menuDeadCenterLength - offset));
                                            continueVariable = false;
                                            break;
                                        case 1:
                                            //asks how many a user wants to add then addds that many to the cart | will display item and then will display quantity like xNumber to the right of the item in the receipt
                                            int fourthActionIndex = -1;
                                            string question = "How Many Would you like to add to your cart? (MAX:10)";
                                            offset = question.Length / 2;
                                            Console.WriteLine(new string(' ', StoreFront.menuDeadCenterLength - offset) + $"{question}" + new string(' ', StoreFront.menuDeadCenterLength - offset));
                                            Console.Write(new string(' ', StoreFront.menuDeadCenterLength));
                                            if (int.TryParse(Console.ReadLine(), out fourthActionIndex))
                                            {
                                                if (fourthActionIndex >= 1 && fourthActionIndex <= 10)
                                                {
                                                    chelseasChickenStore.AddToCart(fourthActionIndex, chelseasChickenStore.Menu[selectedFood]);
                                                    confirmationText = $"{chelseasChickenStore.Menu[selectedFood].Name} has been added to your cart {fourthActionIndex} times! Your cart now has {chelseasChickenStore.Cart.Count} items!";
                                                    offset = confirmationText.Length / 2;
                                                    Console.WriteLine(new string(' ', StoreFront.menuDeadCenterLength - offset) + $"{confirmationText}" + new string(' ', StoreFront.menuDeadCenterLength - offset));
                                                }
                                                else if (fourthActionIndex > 10)
                                                {
                                                    Console.WriteLine("Sorry you can only add 10 items at a time! Thank You!");
                                                    Console.WriteLine("Press any key to continue!");
                                                    Console.ReadKey();
                                                    Console.Clear();
                                                    continue;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Operation failed! Can't add 0 or less objects to the cart!");
                                                    Console.WriteLine("Press any key to continue!");
                                                    Console.ReadKey();
                                                    Console.Clear();
                                                    continue;
                                                }
                                            }
                                            continueVariable = false;
                                            break;
                                        case 2:
                                            OrderFood();
                                            break;
                                    }
                                    break;
                                }
                            } while (true);
                            break;
                        case 1:
                            //recursive usage of method perhaps?
                            OrderFood();

                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Input... Please enter a number to choose the desired option.");
                    Console.WriteLine("Press Any Key To Continue");
                    Console.ReadKey(); 
                    Console.Clear();
                    continue;
                }
            } while (continueVariable);
        }
        else
        {
            Console.WriteLine("Invalid Input... Please enter a number to choose the desired option.");
            Console.WriteLine("Press Any Key To Continue");
            Console.ReadKey();
            Console.Clear();
            continue;
        }
    } while (continueVariable);
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
