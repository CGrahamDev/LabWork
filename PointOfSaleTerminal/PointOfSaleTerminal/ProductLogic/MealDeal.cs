using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleTerminal.ProductLogic
{
    internal class MealDeal
    {
        public Product[] Meal { get; set; }
        public static Category MenuCategory = Category.MealDeal;
        public decimal Price { get; private set; }


        //Will learn what items are in 
        public MealDeal(params Product[] mealItems)
        {
            FormMeal(mealItems);
            CalculateDealPrice();
        }

        //takes in the list of items congregated for a meal and adds each item to the Meal
        private void FormMeal(params Product[] mealItems)
        {
            Meal = new Product[mealItems.Length];
            for (int i = 0; i < mealItems.Length; i++) 
            {
                Meal[i] = mealItems[i];
            }
        }


        private void CalculateDealPrice()
        {
            decimal totalPrice = 0;
            foreach (Product item in Meal)
            {
                switch (item.MenuCategory)
                {
                    case Category.Entree:
                    case Category.ValueItem:
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
            Price = totalPrice;
        }
        public override string ToString()
        {
            return "";
        }
    
    }
}
