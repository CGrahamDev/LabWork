using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleTerminal.ProductLogic
{
    internal class StoreFront
    {
        public List<Product> Cart { get; private set; }
        public List<Product> Menu {  get; private set; }
        public string StoreName { get; set; }
        public List<MealDeal> Meals { get; set; }



        //constructor 
        public StoreFront(string storeName)
        {
            Meals = new List<MealDeal>();
            Cart = new List<Product>();
            Menu = new List<Product>();
            int menuItemNumber = 0;
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
        
        //will display the entire menu and section it all and will then
        public void OrderFood()
        {
            //will call the menu display and will give options of selection and purchase;
            throw new NotImplementedException();
        }
        //Throws each menu item into a list based on its category and will separate and display them based on that information
        //figure out how to make menu hold MealDeal items too
        public void DisplayMenu()
        {
            //Will be used to assign a number to each menu item
            int menuItemValue = 1;
            int nameSpaceOffset = -1;
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
            Console.Write(new string(' ', menuDeadCenterLength-StoreName.Length/2) + $"{StoreName}" + new string(' ', menuDeadCenterLength - StoreName.Length/2) +"\n\r");

            Console.Write(new string('-', menuQuarterCenterLength - categoryNames[0].Length/2) + $"{categoryNames[0]}" + new string('-', menuQuarterCenterLength - categoryNames[0].Length / 2));
            Console.WriteLine(new string('-', menuQuarterCenterLength - categoryNames[1].Length / 2) + $"{categoryNames[1]}" + new string('-', menuQuarterCenterLength - categoryNames[1].Length / 2));
            Console.WriteLine(new string(' ', menuMaxLength));
            if(entrees.Count > sides.Count)
            {
                higherCount = entrees.Count;
            }
            else
            {
                higherCount = sides.Count;      
            }
            //Making sure that both entrees and sides have more than 0 items before
            if (higherCount > 0 && entrees.Count > 0 && sides.Count > 0)
            {
                for (int i = 0; i < higherCount; i++)
                {
                    
                    //NOTE: Add logic to make sure that in the case of a categorical type not having the same number of indexing as the higher count, that the loop will end as to avoid throwing an ArgumentOutRangeException.
                    //Particularly adding a try-catch that will catch that IndexOutOfRangeExceptions and break the loop in the case of being caught.
                    try
                    {
                        nameSpaceOffset = entrees[i].Name.Length / 2;
                        Console.Write(new string(' ', (menuMaxLength / 4 - nameSpaceOffset)) + $"{entrees[i].Name}" + new string(' ', (menuMaxLength / 4 - nameSpaceOffset)));
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine(new string(' ', menuDeadCenterLength));
                    }
                    try
                    {
                        nameSpaceOffset = sides[i].Name.Length / 2;
                        Console.WriteLine(new string(' ', menuMaxLength / 4 - nameSpaceOffset) + $"{sides[i].Name}" + new string(' ', (menuMaxLength / 4 - nameSpaceOffset)));
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine(new string(' ', menuDeadCenterLength));
                    }
                }
                Console.WriteLine(new string(' ', menuMaxLength));
                Console.Write(new string('-', menuQuarterCenterLength - categoryNames[2].Length / 2) + $"{categoryNames[2]}" + new string('-', menuQuarterCenterLength - categoryNames[2].Length / 2));
                Console.WriteLine(new string('-', menuQuarterCenterLength - categoryNames[3].Length / 2) + $"{categoryNames[3]}" + new string('-', menuQuarterCenterLength - categoryNames[3].Length / 2));
                if (beverages.Count > desserts.Count)
                {
                    higherCount = beverages.Count;
                }
                else
                {
                    higherCount = desserts.Count;
                }
                //Making sure that both entrees and sides have more than 0 items before
                if (higherCount > 0 && beverages.Count > 0 && desserts.Count > 0)
                {
                    for (int i = 0; i < higherCount; i++)
                    {
                        nameSpaceOffset = -1;
                        //NOTE: Add logic to make sure that in the case of a categorical type not having the same number of indexing as the higher count, that the loop will end as to avoid throwing an ArgumentOutRangeException.
                        //Particularly adding a try-catch that will catch that IndexOutOfRangeExceptions and break the loop in the case of being caught.
                        try
                        {
                            nameSpaceOffset = entrees[i].Name.Length / 2;
                            Console.Write(new string(' ', (menuMaxLength / 4 - nameSpaceOffset)) + $"{beverages[i].Name}" + new string(' ', (menuMaxLength / 4 - nameSpaceOffset)));
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine(new string(' ', menuDeadCenterLength));
                        }
                        try
                        {
                            nameSpaceOffset = sides[i].Name.Length / 2;
                            Console.WriteLine(new string(' ', menuMaxLength / 4 - nameSpaceOffset) + $"{desserts[i].Name}" + new string(' ', (menuMaxLength / 4 - nameSpaceOffset)));
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine(new string(' ', menuDeadCenterLength));
                        }
                    }
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



    }
}
