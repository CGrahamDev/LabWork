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




        //constructor 
        public StoreFront(string storeName)
        {

            Cart = new List<Product>();
            Menu = new List<Product>();
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
            Console.Write(new string(' ', 60) + " 4C Menu" + new string(' ', 60)+"\n\r");

            Console.Write(new string('-', 28) + "Entrees" + new string('-',28));
            Console.WriteLine(new string('-', 29) + "Sides" + new string('-',31));
            if(entrees.Count > sides.Count)
            {
                higherCount = entrees.Count;
            }
            else
            {
                higherCount = sides.Count;      
            }
            for (int i = 0; i < higherCount; i++)
            {
                //NOTE: Add logic to make sure that in the case of a categorical type not having the same number of indexing as the higher count, that the loop will end as to avoid throwing an ArgumentOutRangeException.
                //Particularly adding a try-catch that will catch that IndexOutOfRangeExceptions and break the loop in the case of being caught.
                int nameSpaceOffset = entrees[i].Name.Length/2;
                Console.Write(new string(' ', (menuMaxLength/4 - nameSpaceOffset)) + $"{ entrees[i].Name}" + new string(' ', (menuMaxLength/4 - nameSpaceOffset)));
                nameSpaceOffset = sides[i].Name.Length/2;
                Console.WriteLine(new string(' ', menuMaxLength/4 - nameSpaceOffset) + $"{sides[i].Name}" + new string(' ', (menuMaxLength/4 - nameSpaceOffset)));
                
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
