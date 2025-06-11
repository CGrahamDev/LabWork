using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Lot_Lab
{
    internal class Car
    {
        public string  Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public static List<Car> Cars {  get; set; }

        public Car()
        {
            Make = "";
            Model = "";
            Year = -1;
            Price = -1.0m;
        }
        public Car(string make, string model, int year, decimal price)
        {
            this.Make = make;
            this.Model = model;
            this.Year = year;
            this.Price = price;
        }
        //PascalCase
        public override string ToString() 
        {
            return $"{Make} {Model} {Year} {Price:c}";
        }

        public void ListCars()
        {
            for (int i = 0; i < Cars.Count; i++)
            {
                Console.WriteLine($"{i+1}." +  Cars[i].ToString());
            }
        }

        public static void Remove(int index)
        {
            Cars.RemoveAt(index);
        }
    }
}
