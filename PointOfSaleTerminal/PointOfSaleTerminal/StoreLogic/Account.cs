using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using PointOfSaleTerminal.StoreLogic;

namespace PointOfSaleTerminal.StoreLogic
{
    internal abstract class Account
    {
        public abstract string Name { get; set; }
        public decimal Balance { get; private set; }
        public abstract int RoutingNumber { get; set; }
        public abstract int AccountNumber { get; set; }

        
        public abstract void SendFunds(Account targetAccount, decimal transferAmount);
        public abstract void ReceiveFunds(decimal transferAmount);
       
    }
}
