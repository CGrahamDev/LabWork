using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleTerminal.ProductLogic
{
    internal class Product
    {
        //product class with a name, category, description, and price for each item. 
        public static List<Product> Cart { get; private set; } = new List<Product>();
        public string Name { get; set; }
        public Category MenuCategory { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }


        public Product(string name, int category, string description, decimal price)
        {
            Name = name;
            MenuCategory = (Category)category;
            Description = description;
            Price = price;
        }


        //CSV TO STRING METHOD FOR FILE SAVING
        public override string ToString()
        {
            return $"{Name},{MenuCategory},{Description},{Price}";
        }
        //method to display items from Cart
        public void DisplayProduct()
        {
            Console.WriteLine($"{Name} {Price:c}\n" +
                $"{Description}\n");
        }
        



        //must be used by an admin to add a price
        public void UpdatePrice(decimal newPrice)
        {
            this.Price = newPrice;
        }

        public void AddToCart(Product item)
        {
            Cart.Add(item);
        }

        public static void AddToCart(MealDeal combo)
        {
            foreach (Product item in combo.Meal)
            {
                Cart.Add(item);
            }
        }
        public static void ClearCart()
        {
            Cart.Clear();
        }
        public static void DisplayFromCart()
        {
            foreach(Product item in Cart)
            {
                item.DisplayProduct();
            }
        }
    }
}
