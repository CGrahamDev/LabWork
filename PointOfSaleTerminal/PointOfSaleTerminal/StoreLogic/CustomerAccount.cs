using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSaleTerminal.PaymentProcessing;
using PointOfSaleTerminal.StoreLogic;

namespace PointOfSaleTerminal.StoreLogic
{
    internal class CustomerAccount : Account
    {
        public override string Name { get ; set; }
        new public decimal Balance { get ; private set; }
        public override int RoutingNumber { get; set; }
        public override int AccountNumber { get; set; }
        public CashPayment Cash {  get; set; }
        public CreditPayment Credit { get; set; }



        /// <summary>
        /// Constructor for making a cash payment
        /// </summary>
        /// <param name="amountTendered"></param>
        /// <param name="itemCost"></param>
        /// <param name="paymentTarget"></param>
        public CustomerAccount(decimal amountTendered, decimal itemCost, decimal paymentTarget)
        {
            Cash = new CashPayment(amountTendered, itemCost, paymentTarget);

        }
        /// <summary>
        /// Constructor for one time cash payment
        /// </summary>
        /// <param name="cardNumber">12-17 character number that can be int.TryParse'd</param>
        /// <param name="expirationDate"></param>
        /// <param name="pin">wacky 3 numbers on the back of your card</param>
        /// <param name="balance">initial balance</param>
        /// <param name="cost">cost of item being bought</param>
        /// <param name="paymentTarget">target for money being spent to go to</param>
        public CustomerAccount(string cardNumber, DateTime expirationDate, string pin, decimal balance, decimal cost, decimal paymentTarget)
        {
            Credit = new CreditPayment(cardNumber, expirationDate, pin, balance, cost, paymentTarget);
        }
        public CustomerAccount(string name, string cardNumber, DateTime expirationDate, string pin, decimal balance)
        {
            Random random = new Random();
            Name = name;
            AccountNumber = random.Next(0_000_000_000, 2_147_483_647);
            RoutingNumber = random.Next(0_000_000_000, 2_147_483_647);
            Credit = new CreditPayment(cardNumber, expirationDate, pin, balance);
            Balance = Credit.Balance;
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

        public override void SendFunds(Account targetAccount, decimal transferAmount)
        {
            if (transferAmount >= 0.01m && (Balance - transferAmount >= 0m))
            {
                Balance -= transferAmount;
                targetAccount.ReceiveFunds(transferAmount);

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
    }
}
