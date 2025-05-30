using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PointOfSaleTerminal.ProductLogic
{
    internal class MealDeal
    {
        public Product[] Meal { get; set; }
        public static Category MenuCategory = Category.MealDeal;
        public decimal Price { get; private set; }
        public string MealName { get; private set; }

        //Will be an empty constructor in case someone wants to just have the empty values
        /*public MealDeal()
        {
            
        }*/
        public MealDeal(string mealName, params Product[] mealItems)
        {

            FormMeal(mealName, mealItems);
            CalculateDealPrice();
        }

        //takes in the list of items congregated for a meal and adds each item to the Meal
        //custom meal
        private void FormMeal(string mealName, params Product[] mealItems)
        {
            MealName = mealName;
            Meal = new Product[mealItems.Length];
            for (int i = 0; i < mealItems.Length; i++)
            {
                Meal[i] = mealItems[i];
            }
        }
        //default meal (entree/value items + Fries or Fingers + Drink + dessert) overload method

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
            // Dictionary<string, List<string>> mealNameToProperties = new Dictionary<string, List<string>>();
            List<string> items = new List<string>();
            List<string> orderedItems = new List<string>(); //add a mechanism to order the items in the strings by Entree, Side, Value, Beverage, Dessert 
            string mealInfo = "";
            foreach (Product item in Meal)
            {
                items.Add($"{MealName},{item.ToString()}\r\n");
            }
            foreach (string item in items)
            {
                mealInfo += item;
            }
            return mealInfo;
        }

    }
}
