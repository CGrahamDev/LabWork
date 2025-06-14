using PointOfSaleTerminal.IPaymentProcessing;


namespace PointOfSaleTerminal.PaymentProcessing
{
    internal class CashPayment : IPaymentProcessor
    {
        public decimal Balance { get; set; }  // Amount Tendered before transaction, Change to be given after transaction
        public decimal Cost { get; set; }
        public decimal TenderedAmount { get; set; }

        public CashPayment(decimal tenderedAmount, decimal itemCost, decimal paymentTarget)
        {

            if (itemCost >= 0 && tenderedAmount >= 0)
            {
                TenderedAmount = tenderedAmount;
                Balance = tenderedAmount;
                Cost = itemCost;
                Pay(paymentTarget);
            }
            else
            {
                throw new Exception("item cost nor tendered amount can be less than 0 USD");
            }



        }

        public void Pay(decimal paymentTarget)
        {

            if (Balance >= Cost)
            {
                paymentTarget += Cost;
                Balance -= Cost;
            }
            else
            {
                throw new Exception("Insufficient Funds. Balance cannot be less than the tendered amount");
            }
        }


        public void Pay()
        {
            throw new NotImplementedException();
        }
    }
}
