using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleTerminal.ProductLogic
{
    internal class MealDeal : Product
    {
        Product[] Meal {  get; set; }
        decimal Price { get; set; }
        public MealDeal()
        {
            
            CalculateDeal();
        }

        internal decimal CalculateDeal()
        {
            decimal totalPrice = 0;
            foreach (Product item in Meal) 
            {
                switch (item.Category)
                {
                    case Category.Entree:
                        item.Price *= 0.85m;
                        totalPrice += item.Price;
                        break;
                    case Category.SideDish:
                        item.Price *= 0.40m;
                        totalPrice += item.Price;
                        break;
                    case Category.Beverage:
                        item.Price *= 0.25m;
                        totalPrice += item.Price;
                        break;
                    case Category.Dessert:
                        item.Price *= 0.50m;
                        totalPrice += item.Price;
                        break;
                }
            }
            return totalPrice;
        }
    
    }
}
