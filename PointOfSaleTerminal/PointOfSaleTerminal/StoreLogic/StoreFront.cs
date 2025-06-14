using PointOfSaleTerminal.ProductLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace PointOfSaleTerminal.StoreLogic
{
    internal class StoreFront
    {
        //Add functionality to make it so that an item cannot exist more than 10 times in the cart (LINQ is probably the easiest way to do so unless there's some already made method in the List Library that does that for me)
        //Do this by making it so that Cart is a dictionary {Key: Product; Value: Count;}. perhaps initialize the cart as holding every product and having an inital count of 0; When displaying the cart go through each dictionary item and only display the ones with a count >= 1;
        //public List<Product> Cart { get; private set; }
        //<Key: Product; int: Count>
        public Dictionary<Product, int> Cart { get; private set; }
        public decimal CartPrice { get; private set; }
        public List<Product> Menu { get; private set; }
        public string StoreName { get; set; }
        public List<MealDeal> Meals { get; set; }

        public const int MenuMaxLength = 128;
        public const int MenuHalfCenterLength = MenuMaxLength / 2;
        public const int MenuQuarterCenterLength = MenuMaxLength / 4;
        public const int CartItemMax = 10;
        public int menuCounter { get; set; } = 0;
        internal StoreAccount StoreAccount { get; set; }

        //constructor 
        public StoreFront(string storeName)
        {


            Meals = new List<MealDeal>();
            //Cart = new List<Product>();
            Cart = new Dictionary<Product, int>();
            Menu = new List<Product>();
            StoreName = storeName;
            StoreAccount = new StoreAccount(storeName);

        }

        //REWRITE ALL METHODS SO THAT THEY MAKE SENSE NOW THAT THIS CLASS IS NO LONGER STATIC
        //consider rewriting them as bools to send signal of item being successfully added
        //methods

        //CART METHODS
        public void AddToCart(Product item)
        {
            Cart.TryGetValue(item, out int value);
            if (Cart.ContainsKey(item) == true && value < 10)
            {
                Cart[item]++;
            }
            else if (Cart.ContainsKey(item) == true && value >= 10)
            {
                throw new OverflowException("Quantity cannot be higher than 10 in the cart.");
            }
            else
            {
                Cart.Add(item, 1);
            }
        }
        public void AddToCart(int quantity, Product item)
        {
            _ = Cart.TryGetValue(item, out int value);
            if (Cart.ContainsKey(item) == true && (value + quantity <= 10))
            {
                Cart[item] += quantity;
            }
            else if (Cart.ContainsKey(item) == true && value + quantity >= 10)
            {
                throw new ArgumentOutOfRangeException("Quantity cannot be higher than 10 in the cart.");
            }
            else
            {
                Cart.Add(item, quantity);
            }

        }
        /*
        public void AddToCart(MealDeal combo)
        {
            //NOT IMPLEMENTED
            foreach (Product item in combo.Meal)
            {
                Cart.Add(item, 1);
            }
            Console.WriteLine($"Added {combo.MealName}");
            throw new NotImplementedException();
        }
        */
        public void ClearCart()
        {
            Cart.Clear();

        }
        public void DisplayCart()
        {
            CartPrice = 0;
            string item = "";
            //add dfuntionality for seperately displaying meal deals and displaying single item products
            //change this so that repeated items arent displayed individually and so that there's a seperate count
            if (Cart.Count >= 1)
            {
                DisplayMax('=');
                DisplayCenter(' ', "Cart ");
                DisplayMax('=');
                foreach (KeyValuePair<Product, int> productToQuantity in Cart)
                {
                    if (productToQuantity.Value > 1)
                    {
                        string itemNameAndIndex = $"{Menu.IndexOf(productToQuantity.Key) + 1}. {productToQuantity.Key.Name} ";
                        string itemPrice = $"{productToQuantity.Key.Price:c} ";
                        string itemTotalPrice = $"{(productToQuantity.Key.Price * productToQuantity.Value):c} ";
                        CartPrice += productToQuantity.Key.Price * productToQuantity.Value;
                        DisplayCenter(' ', $"{itemNameAndIndex}{itemPrice} - x{productToQuantity.Value}");

                        Console.WriteLine(new string(' ', MenuQuarterCenterLength) + new string('-', MenuHalfCenterLength) + new string(' ', MenuQuarterCenterLength));

                        DisplayCenter(' ', $"{itemTotalPrice}");

                        DisplayMax('-');
                    }
                    else if (productToQuantity.Value == 1)
                    {
                        string itemNameAndIndex = $"{Menu.IndexOf(productToQuantity.Key) + 1}. {productToQuantity.Key.Name} ";
                        string itemPrice = $"{productToQuantity.Key.Price:c} ";
                        CartPrice += productToQuantity.Key.Price * productToQuantity.Value;
                        DisplayCenter(' ', $"{itemNameAndIndex}{itemPrice} - x{productToQuantity.Value}");
                        DisplayMax('-');
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                DisplayHalfCenter(' ', $"Price: {CartPrice:c}");
                DisplayHalfCenter(' ', $"Tax: {(CartPrice * 0.06m):c}", true);
                CartPrice *= 1.06m;
                DisplayCenter(' ', $"Total Price: {CartPrice:c}");

            }
            else
            {
                throw new NullReferenceException("Cart is empty.");
            }
        }
        public int CountCart()
        {
            int count = 0;
            foreach(KeyValuePair<Product, int> item in Cart)
            {
                count += item.Value;
            }
            return count;
        }

        // will be used in other functions to display a specific item that might be being affeceted by any customer actions
        //maybe change this to take an int parameter or maybe overload that
        public void DisplayFromCart(Product product)
        {
            DisplayProductDetails(product);
        }
        public void DisplayReceipt()
        {
            CartPrice = 0;
            string item = "";
            //add dfuntionality for seperately displaying meal deals and displaying single item products
            //change this so that repeated items arent displayed individually and so that there's a seperate count
            if (Cart.Count >= 1)
            {
                DisplayMax('=');
                DisplayCenter(' ', "Receipt");
                DisplayMax('=');
                foreach (KeyValuePair<Product, int> productToQuantity in Cart)
                {
                    if (productToQuantity.Value > 0)
                    {

                        string itemNameAndIndex = $"{Menu.IndexOf(productToQuantity.Key) + 1}. {productToQuantity.Key.Name} ";
                        string itemPrice = $"{productToQuantity.Key.Price:c} ";
                        string itemTotalPrice = $"{(productToQuantity.Key.Price * productToQuantity.Value):c} ";
                        CartPrice += productToQuantity.Key.Price * productToQuantity.Value;
                        for (int i = 0; i < productToQuantity.Value; i++)
                        {
                            DisplayCenter(' ', $"{itemPrice} | {itemNameAndIndex}");
                            Console.WriteLine(new string(' ', MenuQuarterCenterLength) + new string('-', MenuHalfCenterLength) + new string(' ', MenuQuarterCenterLength));
                        }

                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                DisplayMax('-');
                Console.WriteLine(new string(' ', MenuQuarterCenterLength) + new string('-', MenuHalfCenterLength) + new string(' ', MenuQuarterCenterLength));
                DisplayHalfCenter(' ', $"Price: {CartPrice:c}");
                DisplayHalfCenter(' ', $"Tax: {(CartPrice * 0.06m):c}", true);
                CartPrice *= 1.06m;
                DisplayCenter(' ', $"Total Price: {CartPrice:c}");


            }
        }
        public void CheckOut()
        {
            CustomerAccount payment;
            string[] checkOutOptions = {
                "[1] Cash ",
                "[2] Credit ",
                "[3] Check ",
                "[4] Return ",
            };

            while (true)
            {

                //DISPLAYS THE CART THEN ASKS THE USER FOR ACTIONS (CHECKOUT/REMOVE FROM CART/)
                try
                {
                    DisplayCart();
                }
                catch (NullReferenceException)
                {
                    DisplayMax('-');
                    DisplayCenter(' ', "Cart is empty; there is nothing to check out");
                    return;
                }
                DisplayMax('-');
                for (int i = 0; i < checkOutOptions.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        DisplayHalfCenter(' ', $"{checkOutOptions[i]} ");
                    }
                    else
                    {
                        DisplayHalfCenter(' ', $"{checkOutOptions[i]} ", true);
                    }

                }
                DisplayCenter(' ', "");
                int firstInput = -1;

                if (int.TryParse(Console.ReadLine(), out firstInput) == true)
                {
                    try
                    {
                        _ = checkOutOptions[firstInput - 1];
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        DisplayMax('-');
                        DisplayCenter(' ', "Input is out of range of selected inputs");
                        Console.Clear();
                        Console.WriteLine("\x1b[3J");
                        continue;
                    }
                    while (true)
                    {
                        switch (firstInput)
                        {
                            case 1:
                                DisplayMax('-');
                                DisplayCenter(' ', $"Your amount owed is {CartPrice:c}. How much do you tender?");
                                DisplayCenter();
                                if (decimal.TryParse(Console.ReadLine(), out decimal tenderedAmount))
                                {
                                    payment = new CustomerAccount(tenderedAmount, CartPrice, StoreAccount.Balance);
                                    Console.Clear();
                                    Console.WriteLine("\x1b[3J");
                                    DisplayReceipt();
                                    DisplayCenter(' ', $"Cash Payment");    
                                    DisplayHalfCenter('-', $"Tendered Amount: {payment.Cash.TenderedAmount:c}");
                                    DisplayHalfCenter('-', $"Change: {payment.Cash.Balance:c}", true);
                                    DisplayMax(' ');
                                    DisplayCenter(' ', $"Payemnt successful. Your Balance is {payment.Balance:c} Press any key to continue");
                                    DisplayCenter();
                                    Console.ReadKey();
                                }
                                else
                                {
                                    DisplayMax('-');
                                    DisplayCenter(' ', "Invalid input, please enter a number amount for your payment. Press any key to continue:");
                                    DisplayCenter();
                                    Console.ReadLine();
                                    Console.Clear();
                                    Console.WriteLine("\x1b[3J");
                                }
                                break;
                            case 2:
                                DisplayMax('-');
                                DisplayCenter(' ', $"Your total is {CartPrice:c}. Enter your name: ");
                                DisplayCenter();
                                string customerName = Console.ReadLine();
                                do
                                {
                                    //CREDIT
                                    decimal prePaymentBalance = StoreAccount.Balance;
                                    
                                    DisplayCenter(' ', "Enter your card number:");
                                    DisplayCenter();
                                    string cardNumber = Console.ReadLine();
                                    DisplayCenter(' ', $"Enter your expiration date: ");
                                    DisplayCenter();
                                    DateTime expiration = DateTime.Parse(Console.ReadLine());
                                    DisplayCenter(' ', $"Enter your CVV");
                                    DisplayCenter();
                                    string cvv = Console.ReadLine();
                                    try
                                    {
                                        payment = new CustomerAccount(cardNumber, expiration, cvv, new Random().Next(25, 300), CartPrice, StoreAccount.Balance);
                                        payment.Name = customerName;
                                        Console.Clear();
                                        Console.WriteLine("\x1b[3J");
                                        DisplayReceipt();
                                        DisplayCenter(' ', "Payment type: Credit");
                                        DisplayHalfCenter('-', $"{payment.Name}");
                                        DisplayHalfCenter('-', $"Card Number: {payment.Credit.AnonymousCardNumber}", true);
                                        DisplayCenter(' ', "Payemnt successful. Press any key to continue");
                                        DisplayCenter(' ', $"Your balance is {payment.Balance:c}");
                                        DisplayMax('-');
                                        DisplayCenter();
                                        Console.ReadKey();
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        DisplayMax('-');
                                        DisplayCenter(' ', $"Payment failed. {ex.Message} Press any key to continue:");
                                        Console.ReadKey();
                                        Console.Clear();
                                        Console.WriteLine("\x1b[3J");
                                        continue;
                                    }
                                } while (true);
                                break;
                            case 3:
                                //CHECK PAYMENT
                                DisplayMax('-');
                                DisplayCenter(' ', "Enter your name:");
                                string name = Console.ReadLine().Trim();
                                DisplayMax('-');
                                DisplayCenter(' ', "Enter your check number (9 chars):");
                                string checkNumber = Console.ReadLine().Trim();
                                try
                                {
                                    payment = new CustomerAccount(name, checkNumber, CartPrice, StoreAccount.Balance);
                                    Console.Clear();
                                    Console.WriteLine("\x1b[3J");
                                    DisplayReceipt();
                                    DisplayCenter(' ', "Payment type: Check ");
                                    DisplayHalfCenter('-', $"Check Number: {payment.Name}", true);
                                    DisplayHalfCenter('-', $"Check Number: {payment.Check.CheckNumber}", true);
                                    DisplayCenter(' ', "Payemnt successful. Press any key to continue");

                                    DisplayCenter();
                                    Console.ReadKey();
                                }
                                catch (Exception)
                                {
                                    DisplayMax('-');
                                    DisplayCenter(' ', "Payment failed. Press any key to continue:");
                                    Console.ReadKey();
                                    Console.Clear();
                                    Console.WriteLine("\x1b[3J");
                                    continue;
                                }
                                break;
                            case 4:
                                return;
                        }
                        break;
                    }
                }
                else
                {
                    DisplayCenter(' ', "Invalid input. Must be a number. Press any key to continue");
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("\x1b[3J");
                }

                break;
            }


        }



        //MENU METHODS
        //Overarching method which will allow the user to View the menu, select items to see more info and add to cart by quantity
        //BUG WITH ADDING ITEMS TO CART LOOPING WRONG; FIND CAUSE AND FIX
        public void OrderFood()
        {
            int selectedFood = -1;
            bool repeatSecondActionPrompt = false;
            bool repeatThirdActionPrompt = false;
            int cartCount;

            bool isValidNumber = false;
            string introText = "Enter a number to select the desired menu item";
            string exitText = "Press any key to continue:";
            do
            {
                do
                {
                    //will call the menu display and will give options of selection and purchase;
                    DisplayMenu();
                    //Will display each menu item in the menu List and assign a number it can be selected by. Will then offer the user to add to cart (by quantity) or return to the menu
                    DisplayMax('=');
                    DisplayCenter(' ', introText);
                    DisplayMax('=');
                    DisplayCenter();
                    if (isValidNumber = int.TryParse(Console.ReadLine(), out selectedFood))
                    {
                        selectedFood -= 1;
                        try
                        {
                            //potentially rewrite this method to use selectedProduct more prominently in leiu of the indexer, selectedFood
                            Product selectedProduct = Menu[selectedFood];
                            break;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            DisplayCenter(' ', $"Argument out of range. Please enter a number 1-{Menu.Count}");
                            DisplayCenter(' ', "Press any key to continue!");
                            DisplayCenter();
                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine("\x1b[3J");
                            continue;
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
                }
                while (repeatSecondActionPrompt == true);
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                SelectMenuItems(selectedFood);
                string cartActionString = $"[1] Add To Cart ";
                string returnActionString = $"[2] Return To Menu ";
                string[] secondaryMenuActions =
                {
                        cartActionString,
                        returnActionString
                        };

                DisplayMax('-');
                DisplayHalfCenter(' ', cartActionString);
                DisplayHalfCenter(' ', returnActionString, true);
                int secondActionIndex = -1;

                DisplayMax('-');
                Console.Write(new string(' ', MenuHalfCenterLength));
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
                    switch (secondActionIndex)
                    {
                        case 0:
                            do
                            {
                                int thirdActionIndex = -1;
                                string[] addToCartOptions = new string[]
                                {
                                " [1] Add One ",
                                " [2] Add More ",
                                " [3] Return ",
                                };
                                Console.Clear();
                                SelectMenuItems(selectedFood);
                                Console.WriteLine(new string('-', MenuMaxLength));
                                DisplayHalfCenter(' ', addToCartOptions[0]);
                                DisplayHalfCenter(' ', addToCartOptions[1], true);
                                DisplayCenter(' ', new string('-', MenuHalfCenterLength));
                                DisplayCenter(' ', addToCartOptions[2]);
                                Console.WriteLine(new string('-', MenuMaxLength));
                                Console.Write(new string(' ', MenuHalfCenterLength));
                                if (int.TryParse(Console.ReadLine(), out thirdActionIndex))
                                {
                                    thirdActionIndex -= 1;
                                    try
                                    {
                                        _ = addToCartOptions[thirdActionIndex];
                                    }
                                    catch (ArgumentOutOfRangeException)
                                    {
                                        DisplayCenter(' ', $"Invalid Input! Please enter an input between 1 and {addToCartOptions.Length}");
                                        DisplayCenter(' ', "Press any key to continue");
                                        DisplayCenter();
                                        Console.ReadKey();
                                        Console.Clear();
                                        continue;
                                    }
                                    switch (thirdActionIndex)
                                    {
                                        case 0:
                                            try
                                            {
                                                AddToCart(Menu[selectedFood]);
                                            }
                                            catch (Exception ex)
                                            {
                                                DisplayCenter(' ', $"{ex.Message} Press any key to continue;");
                                                DisplayCenter();
                                                Console.ReadKey();
                                                continue;
                                            }
                                            string confirmationText = $"{Menu[selectedFood].Name} has been added to your cart! Your cart now has {CountCart()} items!";
                                            DisplayCenter(' ', confirmationText);
                                            DisplayCenter(' ', exitText);
                                            Console.Write(new string(' ', MenuHalfCenterLength));
                                            Console.ReadKey();
                                            return;
                                        case 1:
                                            //asks how many a user wants to add then addds that many to the cart | will display item and then will display quantity like xNumber to the right of the item in the receipt
                                            int fourthActionIndex = -1;
                                            string question = "How Many Would you like to add to your cart? (MAX:10)";
                                            DisplayCenter(' ', question);
                                            Console.Write(new string(' ', MenuHalfCenterLength));
                                            if (int.TryParse(Console.ReadLine(), out fourthActionIndex))
                                            {
                                                if (fourthActionIndex >= 1 && fourthActionIndex <= 10)
                                                {
                                                    try
                                                    {
                                                        AddToCart(fourthActionIndex, Menu[selectedFood]);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        DisplayCenter(' ', $"{ex.Message} Press any key to continue;");
                                                        DisplayCenter();
                                                        Console.ReadKey();
                                                        continue;
                                                    }
                                                    confirmationText = $"{Menu[selectedFood].Name} has been added to your cart {fourthActionIndex} times! Your cart now has {CountCart()} items!";
                                                    DisplayCenter(' ', confirmationText);
                                                    DisplayCenter(' ', exitText);
                                                    Console.Write(new string(' ', MenuHalfCenterLength));
                                                    Console.ReadKey();
                                                    return;
                                                }
                                                else if (fourthActionIndex > 10)
                                                {

                                                    DisplayCenter(' ', $"Sorry you can only add 10 items at a time! Thank You though :) {exitText}");
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
                                            break;
                                        case 2:
                                            repeatSecondActionPrompt = true;
                                            break;
                                    }
                                    break;
                                }
                            } while (repeatThirdActionPrompt == true);
                            break;
                        case 1:
                            continue;

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

            } while (repeatSecondActionPrompt == true);
        }

        //Throws each menu item into a list based on its category and will separate and display them based on that information
        /// <summary>
        /// Will Select a menu product by index and display that item and its details 
        /// </summary>
        /// <param name="index"> integer value used to index item in the Menu Property</param>
        /// <returns>returns true if indexed item is able to be selected and returns true if otherwise </returns>
        public bool SelectMenuItems(int index)
        {
            Console.Clear();
            try
            {
                DisplayProductDetails(Menu[index]);
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Entered index is not tied to any item on the menu");
                return false;
            }
        }
        //method to display items from Cart
        public void DisplayProductDetails(Product selectedProduct)
        {

            string itemName = $"{Menu.IndexOf(selectedProduct) + 1}. {selectedProduct.Name} ";
            string itemPrice = $"{selectedProduct.Price:c} ";
            string itemDescription = $"{selectedProduct.Description}";

            DisplayMax('-');
            DisplayCenter(' ', itemName);
            Console.WriteLine(new string(' ', MenuQuarterCenterLength) + new string('-', MenuHalfCenterLength) + new string(' ', MenuQuarterCenterLength));
            DisplayCenter(' ', itemDescription);
            DisplayCenter(' ', itemPrice);
        }


        //figure out how to make menu hold MealDeal items too






        /// <summary>
        /// A method which will display the menu -  it's categories and it's items
        /// </summary>
        public void DisplayMenu()
        {
            string itemName = "";
            string itemIndex = "";
            string itemPrice = "";
            string[] categoryNames = new string[]
            {
                $"{(Category)0}s",
                $"{(Category)1}es",
                $"{(Category)2}s",
                $"{(Category)3}s",
                $"{(Category)4}s",
                $"{(Category)5}s",
            };
            //used to determine which of the two category lists at is time is holding more items
            int higherCount = 0;
            List<Product> entrees = new List<Product>();
            List<Product> sides = new List<Product>();
            List<Product> beverages = new List<Product>();
            List<Product> desserts = new List<Product>();
            List<Product> valueItems = new List<Product>();
            List<MealDeal> mealDeals = new List<MealDeal>();
            //assigning sections of menu
            foreach (Product item in Menu)
            {
                switch (item.MenuCategory)
                {
                    case Category.Entree:
                        entrees.Add(item);
                        break;
                    case Category.SideDish:
                        sides.Add(item);
                        break;
                    case Category.Beverage:
                        beverages.Add(item);
                        break;
                    case Category.Dessert:
                        desserts.Add(item);
                        break;
                    case Category.ValueItem:
                        valueItems.Add(item);
                        break;
                        // THIS IS WHERE MEALDEAL SWITCH WILL BE
                        //TBC
                }
            }
            //look how to express repeated chars using Console witchery
            DisplayMax('=');
            //test value with magic value 
            //TODO Change length values to include const values and math that would take into account of variable lengths for each thing. e.g: change "4C Menu" to store name.
            //Add logic to take into account the store name being odd vs even. As well as all of the other name dependent cw calls.
            DisplayCenter(' ', StoreName);
            DisplayHalfCenter('-', categoryNames[0]);
            DisplayHalfCenter('-', categoryNames[1], true);
            DisplayMax(' ');
            if (entrees.Count > sides.Count)
            {
                higherCount = entrees.Count;
            }
            else
            {
                higherCount = sides.Count;
            }
            //Making sure that both entrees and sides have more than 0 items before
            if (higherCount > 0 && (entrees.Count > 0 || sides.Count > 0))
            {
                for (int i = 0; i < higherCount; i++)
                {

                    //NOTE: Add logic to make sure that in the case of a categorical type not having the same number of indexing as the higher count, that the loop will end as to avoid throwing an ArgumentOutRangeException.
                    //Particularly adding a try-catch that will catch that IndexOutOfRangeExceptions and break the loop in the case of being caught.
                    try
                    {
                        itemName = $"{entrees[i].Name} ";
                        itemIndex = $"{Menu.IndexOf(entrees[i]) + 1}. ";
                        itemPrice = $"{entrees[i].Price:c} ";

                        string completeString = $"{itemIndex}{itemName}- {itemPrice}";
                        DisplayHalfCenter(' ', completeString);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine(new string(' ', MenuHalfCenterLength));
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine(new string(' ', MenuHalfCenterLength));
                    }
                    try
                    {
                        itemName = $"{sides[i].Name} ";
                        itemIndex = $"{Menu.IndexOf(sides[i]) + 1}. ";
                        itemPrice = $"{sides[i].Price:c} ";

                        string completeString = $"{itemIndex}{itemName}- {itemPrice}";
                        DisplayHalfCenter(' ', completeString, true);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine(new string(' ', MenuHalfCenterLength));
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine(new string(' ', MenuHalfCenterLength));
                    }
                }
                higherCount = 0;
                Console.WriteLine(new string(' ', MenuMaxLength));



                if (valueItems.Count >= 1)
                {

                    DisplayCenter('-', categoryNames[2]);
                    Console.WriteLine(new string(' ', MenuHalfCenterLength));
                    for (int i = 0; i < valueItems.Count; i++)
                    {
                        try
                        {
                            itemName = $"{valueItems[i].Name} ";

                            itemIndex = $"{Menu.IndexOf(valueItems[i]) + 1}. ";

                            itemPrice = $"{valueItems[i].Price:c} ";

                            string completeString = $"{itemIndex}{itemName}- {itemPrice}";
                            DisplayCenter(' ', completeString);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine(new string(' ', MenuHalfCenterLength));
                        }
                    }
                    Console.WriteLine(new string(' ', MenuHalfCenterLength));
                }



                DisplayHalfCenter('-', categoryNames[3]);
                DisplayHalfCenter('-', categoryNames[4], true);
                if (beverages.Count > desserts.Count)
                {
                    higherCount = beverages.Count;
                }
                else
                {
                    higherCount = desserts.Count;
                }
                //Making sure that both entrees and sides have more than 0 items before
                if (higherCount > 0 && (beverages.Count > 0 || desserts.Count > 0))
                {
                    Console.WriteLine(new string(' ', MenuHalfCenterLength));
                    for (int i = 0; i < higherCount; i++)
                    {
                        //NOTE: Add logic to make sure that in the case of a categorical type not having the same number of indexing as the higher count, that the loop will end as to avoid throwing an ArgumentOutRangeException.
                        //Particularly adding a try-catch that will catch that IndexOutOfRangeExceptions and break the loop in the case of being caught.
                        try
                        {
                            itemName = $"{beverages[i].Name} ";

                            itemIndex = $"{Menu.IndexOf(beverages[i]) + 1}. ";

                            itemPrice = $"{beverages[i].Price:c} ";

                            string completeString = $"{itemIndex}{itemName}- {itemPrice}";
                            DisplayHalfCenter(' ', completeString);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine(new string(' ', MenuHalfCenterLength));
                        }
                        try
                        {
                            itemName = $"{desserts[i].Name} ";

                            itemIndex = $"{Menu.IndexOf(desserts[i]) + 1}. ";

                            itemPrice = $"{desserts[i].Price:c} ";

                            string completeString = $"{itemIndex}{itemName}- {itemPrice}";
                            DisplayHalfCenter(' ', completeString, true);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine(new string(' ', MenuHalfCenterLength));
                        }
                    }
                    Console.WriteLine(new string(' ', MenuHalfCenterLength));
                }
            }
        }

        //DEVELOP A PRODUCT DETAILS METHOD



        //will be moved to a console admin methods 
        public void AddToMenu(Product product)
        {
            bool duplicateName = false;
            foreach (Product item in Menu)
            {
                if (item.Name == product.Name)
                {
                    duplicateName = true;
                    Console.WriteLine("This item is already on the list. If you want to change something about this item, please edit it instead.");
                    return;
                }
            }
            if (duplicateName == false)
            {

                Menu.Add(product);
                //NumberToProductDictionary.Add(menuCounter, product);
                //menu counter might not be necessary if you find a way to find the highest int value in a dictionary

                menuCounter += 1;
                return;
            }
        }
        public void RemoveFromMenu(Product item)
        {
            //try
            //{
            Menu.Remove(item);
            //}
            //catch (Exception e)
            {

            }
        }

        public void UpdateMenuFile(string fileName)
        {
            //food-menu is what Ill be using as a file name, but the idea is that this would allow for easy switching to new menus and easy categorizing (dates, seasons, times of day, etc etc)
            using (StreamWriter writer = new StreamWriter($"{fileName}.txt", false))
            {
                foreach (Product item in Menu)
                {
                    writer.WriteLine(item.ToString().Trim());
                }
                writer.Close();
            }
        }
        /// <summary>
        /// A method that reads a txt file using pipe separated values and returns true if the file is able to be parsed and have its contents added to the menu of the instaniated StoreFront object. 
        /// </summary>
        /// <param name="fileName">NOTE: the param by defualt works for txt files, so do not use the file or extension type at the end of the end of the param</param>
        /// <returns ></returns>
        public bool ReadMenuFile(string fileName)
        {
            string line = "";
            try
            {

                using (StreamReader reader = new StreamReader($"{fileName}.txt"))
                {
                    if (!reader.EndOfStream)
                    {
                        while (reader.EndOfStream != true)
                        {
                            line = reader.ReadLine();
                            try
                            {
                                string[] pipeSections = line.Split('|');
                                if (pipeSections.Length == 4)
                                {
                                    try
                                    {
                                        Product food = new Product(pipeSections[0], int.Parse(pipeSections[1]), pipeSections[2], decimal.Parse(pipeSections[3]));
                                        AddToMenu(food);
                                    }
                                    catch (FormatException)
                                    {

                                        Console.WriteLine("File content formatted incorrectly!");
                                    }
                                }
                                else
                                {
                                    //ADD SOMETHING HERE TO DEFENSIVELY CODE AGAINST FILE FORMAT ISSUES
                                    continue;
                                }
                            }
                            catch (NullReferenceException)
                            {
                                reader.Close();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        throw new FileNotFoundException();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error; File Not Found");
                return false;
            }
            if (Menu.Count >= 1) { return true; }
            return false;
        }

        /// <summary>
        /// Console.WriteLine()s a new string and repeats the character to be tallied by the StoreFront.menuMaxLength
        /// </summary>
        /// <param name="characterTally">the character that will be repeated in a new string</param>
        static public void DisplayMax(char characterTally)
        {
            Console.WriteLine(new string(characterTally, MenuMaxLength));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="characterTally"></param>
        /// <param name="contentString"></param>
        static public void DisplayCenter(char characterTally, string contentString)
        {
            int offset = contentString.Length / 2;
            Console.WriteLine(new string(characterTally, MenuHalfCenterLength - offset) + $"{contentString}" + new string(characterTally, MenuHalfCenterLength - offset));
        }
        static public void DisplayCenter()
        {
            Console.Write(new string(' ', MenuHalfCenterLength));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="characterTally"></param>
        /// <param name="contentStringOne"></param>
        /// <param name="contentStringTwo"></param>
        static public void DisplayQuarterCenter(char characterTally, string contentStringOne, string contentStringTwo)
        {
            int offset = contentStringOne.Length / 2;
            Console.Write(new string(characterTally, MenuQuarterCenterLength - offset) + $"{contentStringOne}" + new string(characterTally, MenuQuarterCenterLength - offset));
            offset = contentStringTwo.Length / 2;
            Console.WriteLine(new string(characterTally, MenuQuarterCenterLength - offset) + $"{contentStringTwo}" + new string(characterTally, MenuQuarterCenterLength - offset));
        }
        /// <summary>
        /// offset is equal to contentString.Length / 2; will be removed from menuQuarterCenterLength - offset and then
        /// Console.Write's a string using a new string(characterTally, StoreFront.menuQuarterCenterLength) - offset cocentated w/ contentString
        /// </summary>
        /// <param name="characterTally">the character that will be repeated x-offset amount of times</param>
        /// <param name="contentString">the character that will be displayed center quarter of the menu</param>
        static public void DisplayHalfCenter(char characterTally, string contentString)
        {
            int offset = contentString.Length / 2;
            Console.Write(new string(characterTally, MenuQuarterCenterLength - offset) + $"{contentString}" + new string(characterTally, MenuQuarterCenterLength - offset));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="characterTally"></param>
        /// <param name="contentString"></param>
        /// <param name="isNewLine"></param>
        static public void DisplayHalfCenter(char characterTally, string contentString, bool isNewLine)
        {
            int offset = contentString.Length / 2;
            Console.Write(new string(characterTally, MenuQuarterCenterLength - offset) + $"{contentString}" + new string(characterTally, MenuQuarterCenterLength - offset) + (isNewLine ? "\n" : ""));

        }




    }
}
