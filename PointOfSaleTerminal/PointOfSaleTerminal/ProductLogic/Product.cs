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
        public Category Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }


        public Product()
        {

        }

        public void UpdatePrice(decimal newPrice)
        {
            this.Price = newPrice;
        }  


    }
}
