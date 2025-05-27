using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleTerminal.ProductLogic
{
    internal class StoreFront
    {
        public List<Product> Cart { get; private set; }
        public List<Product> Menu { get; private set; }
        //public List<Dictionary<int,Product>> Menu {  get; private set; }
        //REFACTOR TO INCLUDE THIS LOGIC
        public string StoreName { get; set; }
        public List<MealDeal> Meals { get; set; }
        public Dictionary<int, Product> NumberToProductDictionary { get; private set; }
        private int menuCounter = 1;

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
        //methods

        //CART METHODS
        public void AddToCart(Product item)
        {
            Cart.Add(item);
            Console.WriteLine("Added to Cart!");
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
                item.DisplayProduct();
            }
        }
        // will be used in other functions to display a specific item that might be being affeceted by any customer actions
        public void DisplayFromCart(Product product)
        {
            product.DisplayProduct();
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
            //will call the menu display and will give options of selection and purchase;
            DisplayMenu();
            //Will display each menu item in the menu List and assign a number it can be selected by. Will then offer the user to add to cart (by quantity) or return to the menu
            //Will then allow the user to check out and offer payment options and give blah blah blah
            //throw new NotImplementedException();
        }
        //Throws each menu item into a list based on its category and will separate and display them based on that information
        public void SelectMenuItems(int index)
        {
            try 
            {
                this.Menu[index].DisplayProduct();
            } catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Index is out of range");
            }
        }

        
        //figure out how to make menu hold MealDeal items too






        /// <summary>
        /// A method which will display the menu -  it's categories and it's items
        /// </summary>
        public void DisplayMenu()
        {
            //Will be used to assign a number to each menu item
            //int menuItemValue = -1;
            int nameSpaceOffset = -1;
            string itemName = "";
            //index values
            int indexSpaceOffset = -1;
            string itemIndex = "";
            //TOBEIMPLEMENTED 
            string itemPrice = "";
            int priceSpaceOffset = -1;
            //OFFSET USED TO DESIGNATE THE AMOUNT OF SPACE TAKEN UP BETWEEN ALL RELEVANT VALUES
            int totalContentOffset;
            string[] categoryNames = new string[]
            {
                $"{(Category)0}s",
                $"{(Category)1}es",
                $"{(Category)2}s",
                $"{(Category)3}s",
                $"{(Category)4}s",
                $"{(Category)5}s",
            };
            const int menuMaxLength = 128;
            const int menuDeadCenterLength = menuMaxLength / 2;
            const int menuQuarterCenterLength = menuDeadCenterLength / 2;
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
            Console.WriteLine(new string('-', menuMaxLength));
            //test value with magic value 
            //TODO Change length values to include const values and math that would take into account of variable lengths for each thing. e.g: change "4C Menu" to store name.
            //Add logic to take into account the store name being odd vs even. As well as all of the other name dependent cw calls.
            Console.Write(new string(' ', menuDeadCenterLength - StoreName.Length / 2) + $"{StoreName}" + new string(' ', menuDeadCenterLength - StoreName.Length / 2) + "\n\r");

            Console.Write(new string('-', menuQuarterCenterLength - categoryNames[0].Length / 2) + $"{categoryNames[0]}" + new string('-', menuQuarterCenterLength - categoryNames[0].Length / 2));
            Console.WriteLine(new string('-', menuQuarterCenterLength - categoryNames[1].Length / 2) + $"{categoryNames[1]}" + new string('-', menuQuarterCenterLength - categoryNames[1].Length / 2));
            Console.WriteLine(new string(' ', menuMaxLength));
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

                        string completeString = $"{itemIndex}{itemName}| {itemPrice}";
                        totalContentOffset = completeString.Length / 2 ;
                        Console.Write(new string(' ', (menuQuarterCenterLength - totalContentOffset)) + $"{completeString}" + new string(' ', (menuQuarterCenterLength - totalContentOffset)));
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine(new string(' ', menuDeadCenterLength));
                    }
                    try
                    {
                        itemName = $"{sides[i].Name} ";

                        itemIndex = $"{Menu.IndexOf(sides[i]) + 1}. ";

                        itemPrice = $"{sides[i].Price:c} ";

                        string completeString = $"{itemIndex}{itemName}| {itemPrice}";
                        totalContentOffset = completeString.Length / 2;
                        Console.WriteLine(new string(' ', (menuQuarterCenterLength - totalContentOffset)) + $"{completeString}" + new string(' ', (menuQuarterCenterLength - totalContentOffset)));
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

                    Console.WriteLine(new string('-', menuDeadCenterLength - categoryNames[2].Length / 2) + $"{categoryNames[2]}" + new string('-', menuDeadCenterLength - categoryNames[2].Length / 2));
                    Console.WriteLine(new string(' ', menuDeadCenterLength));
                    for (int i = 0; i < valueItems.Count; i++)
                    {
                        nameSpaceOffset = -1;
                        try
                        {
                            itemName = $"{valueItems[i].Name} ";

                            itemIndex = $"{Menu.IndexOf(valueItems[i]) + 1}. ";

                            itemPrice = $"{valueItems[i].Price:c} ";

                            string completeString = $"{itemIndex}{itemName}| {itemPrice}";
                            totalContentOffset = completeString.Length / 2;
                            Console.WriteLine(new string(' ', (menuDeadCenterLength - totalContentOffset)) + $"{completeString}" + new string(' ', (menuDeadCenterLength - totalContentOffset)));
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine(new string(' ', menuDeadCenterLength));
                        }
                    }
                    Console.WriteLine(new string(' ', menuDeadCenterLength));
                }



                Console.Write(new string('-', menuQuarterCenterLength - categoryNames[3].Length / 2) + $"{categoryNames[3]}" + new string('-', menuQuarterCenterLength - categoryNames[2].Length / 2));
                Console.WriteLine(new string('-', menuQuarterCenterLength - categoryNames[4].Length / 2) + $"{categoryNames[4]}" + new string('-', menuQuarterCenterLength - categoryNames[3].Length / 2));
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

                            string completeString = $"{itemIndex}{itemName}| {itemPrice}";
                            totalContentOffset = completeString.Length / 2;
                            Console.Write(new string(' ', (menuQuarterCenterLength - totalContentOffset)) + $"{completeString}" + new string(' ', (menuQuarterCenterLength - totalContentOffset)));
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

                            string completeString = $"{itemIndex}{itemName}| {itemPrice}";
                            totalContentOffset = completeString.Length / 2;
                            Console.WriteLine(new string(' ', (menuQuarterCenterLength - totalContentOffset)) + $"{completeString}" + new string(' ', (menuQuarterCenterLength - totalContentOffset)));
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



    }
}
