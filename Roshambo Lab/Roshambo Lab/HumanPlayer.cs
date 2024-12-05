using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Roshambo_Lab
{
    public class HumanPlayer : Player
    {
        public string Name { get; set; }
        private int userValue;

        public HumanPlayer()
        {

            Console.WriteLine("Enter your name: ");
            this.Name = Console.ReadLine();
            
        }

        public override Roshambo GenerateRoshambo()
        {
            bool pass = false;
            foreach (int enumValue in Enum.GetValues(typeof(Roshambo)))
            {
                Console.WriteLine($"{enumValue} - {Enum.GetName(typeof(Roshambo), enumValue)}");
            }
            Console.WriteLine("Enter which call to throw by value (number).");
            do
            {
                try
                {
                    userValue = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Oops!Enter a number 0-2 correlating to the throw you wanna call.");
                    continue;
                }
                //used to make sure that 
                if (!Enum.IsDefined(typeof(Roshambo), userValue))
                {
                    throw new ArgumentOutOfRangeException("Invalid input; not Rock, Paper, or Scissors.");
                    
                }
                break;
            } while (pass != true);
            return (Roshambo)userValue;
        }
    }
}
