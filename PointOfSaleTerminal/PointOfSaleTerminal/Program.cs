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
using PointOfSaleTerminal.ProductLogic;

List<Product> cart = new List<Product>(); 
try
{
    StreamReader reader = new StreamReader("menu.txt");
}
catch (FileNotFoundException)
{
    StreamWriter writer = new StreamWriter("menu.txt");
}


//One of a few nav screens intended (Admin, Edit(edit menu items), Menu/Customer,)
MenuNavigation();

Console.ReadKey();


//Fast Food - Chelsea's Comically Collossal Chicken


void MenuNavigation()
{
    while (true)
    {
        
        break;
    }
}

void CheckOut()
{
    foreach (Product item in cart)
    {
        throw new NotImplementedException();
    }
}

