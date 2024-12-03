using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roshambo_Lab
{
    public abstract class Player
    {
        string Name { get; set; }
        int RoshamboValue { get; set; }

        public abstract Roshambo GenerateRoshambo();
    }
}
