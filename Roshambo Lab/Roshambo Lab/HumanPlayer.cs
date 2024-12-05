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
            
            do
            {
                
                foreach (int enumValue in Enum.GetValues(typeof(Roshambo)))
                {
                    Console.WriteLine($"{enumValue} - {Enum.GetName(typeof(Roshambo), enumValue)}");
                }
                Console.WriteLine("Enter which call to throw by value (number).");
                try
                {
                    userValue = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Oops!Enter a number 0-2 correlating to the throw you wanna call.");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                // TODO: ADD EXTRA LAYER OF VALIDATION SO THAT THE USER CAN DIRECTLY NOT HAVE TO CRASH THEIR APP FOR USING THE WRONG INPUT
                if (!Enum.IsDefined(typeof(Roshambo), userValue))
                {
                    Console.WriteLine("Invalid input; not Rock, Paper, or Scissors.");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                break;
            } while (pass != true);
            return (Roshambo)userValue;
        }
    }
}
