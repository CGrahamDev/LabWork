using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PointOfSaleTerminal.ProductLogic
{
    internal class StoreFront
    {
        public List<Product> Cart { get; private set; }
        public List<Product> Menu { get; private set; }
        public string StoreName { get; set; }
        public List<MealDeal> Meals { get; set; }
        private int menuCounter = 1;
        public const int menuMaxLength = 128;
        public const int menuDeadCenterLength = menuMaxLength / 2;
        public const int menuQuarterCenterLength = menuDeadCenterLength / 2;

        //constructor 
        public StoreFront(string storeName)
        {
            Meals = new List<MealDeal>();
            Cart = new List<Product>();
            Menu = new List<Product>();
            // Menu = new List<Dictionary<int, Product>>();
            StoreName = storeName;
        }

        //REWRITE ALL METHODS SO THAT THEY MAKE SENSE NOW THAT THIS CLASS IS NO LONGER STATIC
        //consider rewriting them as bools to send signal of item being successfully added
        //methods

        //CART METHODS
        public void AddToCart(Product item)
        {
            Cart.Add(item);
        }
        public void AddToCart(int quantity, Product item)
        {
            for (int i = 0; i < quantity; i++)
            {
                Cart.Add(item);
            }

        }
        public void AddToCart(MealDeal combo)
        {
            foreach (Product item in combo.Meal)
            {
                Cart.Add(item);
            }
            Console.WriteLine($"Added {combo.MealName}");
        }
        public void ClearCart()
        {
            Cart.Clear();
        }
        public void DisplayCart()
        {
            //add dfuntionality for seperately displaying meal deals and displaying single item products
            foreach (Product item in Cart)
            {
                DisplayProductDetails(item);
            }
        }
        // will be used in other functions to display a specific item that might be being affeceted by any customer actions
        //maybe change this to take an int parameter or maybe overload that
        public void DisplayFromCart(Product product)
        {
            DisplayProductDetails(product);
        }
        public void CheckOut()
        {
            foreach (Product item in Cart)
            {
                throw new NotImplementedException();
            }
        }



        //MENU METHODS
        //Overarching method which will allow the user to View the menu, select items to see more info and add to cart by quantity

        public void OrderFood()
        {
            int selectedFood = -1;

            bool isValidNumber = false;
            string introText = "Enter a number to select the desired menu item";
            string exitText = "Press any key to continue:";
            do
            {
                //will call the menu display and will give options of selection and purchase;
                DisplayMenu();
                //Will display each menu item in the menu List and assign a number it can be selected by. Will then offer the user to add to cart (by quantity) or return to the menu
                DisplayMax('-'); 
                DisplayDeadCenter(' ', introText);
                DisplayMax('-');
                Console.Write(new string(' ', StoreFront.menuDeadCenterLength));
                if (isValidNumber = int.TryParse(Console.ReadLine(), out selectedFood))
                {
                    selectedFood -= 1;
                    try
                    {
                        Product selectedProduct = Menu[selectedFood];
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine($"Argument out of range. Please enter a number 1-{Menu.Count}");
                        Console.WriteLine("Press any key to continue!");
                        Console.ReadKey();
                        Console.Clear();
                        Console.WriteLine("\x1b[3J");
                        continue;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine($"Argument out of range. Please enter a number 1-{Menu.Count}");
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
                        SelectMenuItems(selectedFood);
                        string cartActionString = $"[1] Add To Cart ";
                        string returnActionString = $"[2] Return To Menu ";
                        string[] secondaryMenuActions =
                        {
                        cartActionString,
                        returnActionString
                        };

                        DisplayMax('-');
                        DisplayQuarterCenter(' ', cartActionString);
                        DisplayQuarterCenter(' ', returnActionString, true);
                        int secondActionIndex = -1;

                        DisplayMax('-');
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
                                        SelectMenuItems(selectedFood);
                                        Console.WriteLine(new string('-', StoreFront.menuMaxLength));
                                        DisplayQuarterCenter(' ', addToCartOptions[0]);
                                        DisplayQuarterCenter(' ', addToCartOptions[1], true);
                                        DisplayDeadCenter(' ', new string('-', StoreFront.menuDeadCenterLength));
                                        DisplayDeadCenter(' ', addToCartOptions[2]);
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
                                                    AddToCart(Menu[selectedFood]);
                                                    string confirmationText = $"{Menu[selectedFood].Name} has been added to your cart! Your cart now has {Cart.Count} items!";
                                                    DisplayDeadCenter(' ', confirmationText);
                                                    DisplayDeadCenter(' ', exitText);
                                                    Console.Write(new string(' ', menuDeadCenterLength));
                                                    Console.ReadKey();
                                                    return;
                                                case 1:
                                                    //asks how many a user wants to add then addds that many to the cart | will display item and then will display quantity like xNumber to the right of the item in the receipt
                                                    int fourthActionIndex = -1;
                                                    string question = "How Many Would you like to add to your cart? (MAX:10)";
                                                    DisplayDeadCenter(' ', question);
                                                    Console.Write(new string(' ', StoreFront.menuDeadCenterLength));
                                                    if (int.TryParse(Console.ReadLine(), out fourthActionIndex))
                                                    {
                                                        if (fourthActionIndex >= 1 && fourthActionIndex <= 10)
                                                        {
                                                            AddToCart(fourthActionIndex, Menu[selectedFood]);
                                                            confirmationText = $"{Menu[selectedFood].Name} has been added to your cart {fourthActionIndex} times! Your cart now has {Cart.Count} items!";
                                                            DisplayDeadCenter(' ', confirmationText);
                                                            DisplayDeadCenter(' ', exitText);
                                                            Console.Write(new string(' ', menuDeadCenterLength));
                                                            Console.ReadKey();
                                                            return;
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
                    } while (true);
                }
                else
                {
                    Console.WriteLine("Invalid Input... Please enter a number to choose the desired option.");
                    Console.WriteLine("Press Any Key To Continue");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
            } while (true);
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
            DisplayDeadCenter(' ', itemName);
            Console.WriteLine(new string(' ', menuQuarterCenterLength) + new string('-', menuDeadCenterLength) + new string(' ', menuQuarterCenterLength));
            DisplayDeadCenter(' ', itemDescription);
            DisplayDeadCenter(' ', itemPrice);
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
            DisplayMax('-');
            //test value with magic value 
            //TODO Change length values to include const values and math that would take into account of variable lengths for each thing. e.g: change "4C Menu" to store name.
            //Add logic to take into account the store name being odd vs even. As well as all of the other name dependent cw calls.
            DisplayDeadCenter(' ', StoreName);
            DisplayQuarterCenter('-', categoryNames[0]);
            DisplayQuarterCenter('-', categoryNames[1], true);
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
            if ((higherCount > 0) && (entrees.Count > 0 || sides.Count > 0))
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
                        DisplayQuarterCenter(' ', completeString);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine(new string(' ', menuDeadCenterLength));
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine(new string(' ', menuDeadCenterLength));
                    }
                    try
                    {
                        itemName = $"{sides[i].Name} ";
                        itemIndex = $"{Menu.IndexOf(sides[i]) + 1}. ";
                        itemPrice = $"{sides[i].Price:c} ";

                        string completeString = $"{itemIndex}{itemName}- {itemPrice}";
                        DisplayQuarterCenter(' ', completeString, true);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine(new string(' ', menuDeadCenterLength));
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine(new string(' ', menuDeadCenterLength));
                    }
                }
                higherCount = 0;
                Console.WriteLine(new string(' ', menuMaxLength));



                if (valueItems.Count >= 1)
                {

                    DisplayDeadCenter('-', categoryNames[2]);
                    Console.WriteLine(new string(' ', menuDeadCenterLength));
                    for (int i = 0; i < valueItems.Count; i++)
                    {
                        try
                        {
                            itemName = $"{valueItems[i].Name} ";

                            itemIndex = $"{Menu.IndexOf(valueItems[i]) + 1}. ";

                            itemPrice = $"{valueItems[i].Price:c} ";

                            string completeString = $"{itemIndex}{itemName}- {itemPrice}";
                            DisplayDeadCenter(' ', completeString);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine(new string(' ', menuDeadCenterLength));
                        }
                    }
                    Console.WriteLine(new string(' ', menuDeadCenterLength));
                }



                DisplayQuarterCenter('-', categoryNames[3]);
                DisplayQuarterCenter('-', categoryNames[4], true);
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
                    Console.WriteLine(new string(' ', menuDeadCenterLength));
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
                            DisplayQuarterCenter(' ', completeString);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine(new string(' ', menuDeadCenterLength));
                        }
                        try
                        {
                            itemName = $"{desserts[i].Name} ";

                            itemIndex = $"{Menu.IndexOf(desserts[i]) + 1}. ";

                            itemPrice = $"{desserts[i].Price:c} ";

                            string completeString = $"{itemIndex}{itemName}- {itemPrice}";
                            DisplayQuarterCenter(' ', completeString, true);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine(new string(' ', menuDeadCenterLength));
                        }
                    }
                    Console.WriteLine(new string(' ', menuDeadCenterLength));
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

                this.Menu.Add(product);
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
                foreach (Product item in this.Menu)
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
            Console.WriteLine(new string(characterTally, StoreFront.menuMaxLength));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="characterTally"></param>
        /// <param name="contentString"></param>
        static public void DisplayDeadCenter(char characterTally, string contentString)
        {
            int offset = contentString.Length / 2;
            Console.WriteLine(new string(characterTally, StoreFront.menuDeadCenterLength - offset) + $"{contentString}" + new string(characterTally, StoreFront.menuDeadCenterLength - offset));
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
            Console.Write(new string(characterTally, StoreFront.menuQuarterCenterLength - offset) + $"{contentStringOne}" + new string(characterTally, StoreFront.menuQuarterCenterLength - offset));
            offset = contentStringTwo.Length / 2;
            Console.WriteLine(new string(characterTally, StoreFront.menuQuarterCenterLength - offset) + $"{contentStringTwo}" + new string(characterTally, StoreFront.menuQuarterCenterLength - offset));
        }
        /// <summary>
        /// offset is equal to contentString.Length / 2; will be removed from menuQuarterCenterLength - offset and then
        /// Console.Write's a string using a new string(characterTally, StoreFront.menuQuarterCenterLength) - offset cocentated w/ contentString
        /// </summary>
        /// <param name="characterTally">the character that will be repeated x-offset amount of times</param>
        /// <param name="contentString">the character that will be displayed center quarter of the menu</param>
        static public void DisplayQuarterCenter(char characterTally, string contentString)
        {
            int offset = contentString.Length / 2;
            Console.Write(new string(characterTally, StoreFront.menuQuarterCenterLength - offset) + $"{contentString}" + new string(characterTally, StoreFront.menuQuarterCenterLength - offset));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="characterTally"></param>
        /// <param name="contentString"></param>
        /// <param name="isNewLine"></param>
        static public void DisplayQuarterCenter(char characterTally, string contentString, bool isNewLine)
        {
            int offset = contentString.Length / 2;
            Console.Write(new string(characterTally, StoreFront.menuQuarterCenterLength - offset) + $"{contentString}" + new string(characterTally, StoreFront.menuQuarterCenterLength - offset) + (isNewLine ? "\n" : ""));

        }




    }
}
