using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roshambo_Lab
{
    public class RandomPlayer : Player
    {

        public override Roshambo GenerateRoshambo()
        {
            Random random = new Random();
            int randomNum = random.Next(0,3);
            return (Roshambo)randomNum;
        }
    }
}