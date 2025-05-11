using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleTerminal.ProductLogic
{
    internal class Product
    {
        //product class with a name, category, description, and price for each item. 
        public string Name { get; set; }
        public Category MenuCategory { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }


        public Product()
        {

        }
        //CSV TO STRING METHOD FOR FILE SAVING
        public override string ToString()
        {
            return $"{Name},{MenuCategory},{Description},{Price}";
        }

        //must be used by an admin to add a price
        public void UpdatePrice(decimal newPrice)
        {
            this.Price = newPrice;
        }  


    }
}
