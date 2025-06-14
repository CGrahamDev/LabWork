using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSaleTerminal.ProductLogic;
using PointOfSaleTerminal.StoreLogic;

namespace PointOfSaleTerminal.StoreLogic
{
    internal class StoreAccount : Account
    {
        public override string Name { get; set; }
        public override int AccountNumber { get; set; }
        public override int RoutingNumber { get; set; }
        new public decimal Balance { get; private set; }
        //maybe redo as a Dictionary<DateTime, Dictionary<List<Product>, >>
        public Dictionary<DateTime, List<Product>> Statements { get; set; }


        public StoreAccount(string name)
        {
            decimal corporatePackage = 15_000m;
            Random random = new Random();
            Name = name;
            AccountNumber = random.Next(0_000_000_000, 2_147_483_647);
            RoutingNumber = random.Next(0_000_000_000, 2_147_483_647);
            Balance = corporatePackage;
            Statements = new Dictionary<DateTime, List<Product>>();
        }
        public StoreAccount(string name, decimal initalBalance)
        {
            Random random = new Random();
            Name = name;
            AccountNumber = random.Next(0_000_000_000, 2_147_483_647);
            RoutingNumber = random.Next(0_000_000_000, 2_147_483_647);
            Balance = initalBalance;
            Statements = new Dictionary<DateTime, List<Product>>();
        }

        public override void SendFunds(Account targetVariable, decimal transferAmount)
        {
            if (transferAmount >= 0.01m && (Balance - transferAmount >= 0m))
            {
                Balance -= transferAmount;
                targetVariable.ReceiveFunds(transferAmount);

            }
            else if (transferAmount <= 0)
            {
                throw new Exception("Transfer amount cannot be equal or less than $0");
            }
            else if (Balance - transferAmount <= 0)
            {
                throw new Exception("Transfer amount being sent cannot be more than the available balance");
            }
        }
        public override void ReceiveFunds(decimal transferAmount)
        {
            if (transferAmount >= 00.1m)
            {
                Balance += transferAmount;
            }
            else
            {
                throw new Exception($"Amount can't be less than {0.01:c}");
            }
        }
        public override string ToString()
        {
            return "";
            throw new NotImplementedException();
        }








        public void ReceiveFunds()
        {
            throw new NotImplementedException();
        }

        public void SendFunds()
        {
            throw new NotImplementedException();
        }
    }
}
