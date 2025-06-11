using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Lot_Lab
{
    internal class UsedCar : Car
    {
        double Mileage { get; set; }

        public UsedCar(string make, string model, int year, decimal price, double mileage ) : base(make, model, year, price)
        {
            this.Mileage = mileage;
        }

        public override string ToString()
        {
            return $"{Make} {Model} {Year} {Price:c} {Mileage} miles";
        }
    }
}
