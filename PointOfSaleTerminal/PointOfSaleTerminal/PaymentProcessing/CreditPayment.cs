using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PointOfSaleTerminal.IPaymentProcessing;

namespace PointOfSaleTerminal.PaymentProcessing
{
    internal class CreditPayment : IPaymentProcessor
    {
        public string CardNumber { get; private set; } //12 char length min string 17 max. Must return true using int.TryParse
        public DateTime ExpirationDate { get; set; }
        public string Pin { get; private set; } //3 char length. Must return true using int.TryParse
        public decimal Balance { get; set; }
        public decimal Cost { get; set; }
        public static DateTime CurrentDate = DateTime.Today;


        public CreditPayment(string cardNumber, DateTime expirationDate, string pin, decimal balance)
        {
            CardNumber =  cardNumber;
            ExpirationDate = expirationDate;
            Pin = pin;
            if (ValidateCardInfo() == false)
            {
                throw new ArgumentException("Card Info is Invalid");

            }
        }
        public CreditPayment(string cardNumber, DateTime expirationDate, string pin, decimal balance, decimal cost, decimal paymentTarget)
        {
            CardNumber = cardNumber;
            ExpirationDate = expirationDate;
            Pin = pin;
            if (ValidateCardInfo() == false)
            {
                throw new ArgumentException("Card Info is Invalid");
            }
            Pay(paymentTarget);
        }
        public void Pay(decimal paymentTarget)
        {
            if (Balance >= Cost)
            {
                paymentTarget += Cost;
                Balance -= Cost;
                Console.WriteLine($"Your balance is {Balance:c}");
            }
            else
            {
                throw new Exception("Insufficient Funds. Balance cannot be less than the tendered amount");
            }
        }
        public decimal GetBalance()
        {
            throw new NotImplementedException();
        }

        private bool ValidateCardInfo()
        {
            bool checkOne = false;
            bool checkTwo = false;
            bool checkThree = false;

            checkOne = ValidateCardNumber();
            checkTwo = ValidateExpirationDate();
            checkThree = ValidatePinNumber();
            if (checkOne && checkTwo && checkThree)
            {
                return true;
            }
            else if (checkOne != true)
            {
                Console.WriteLine("Card number not valid");
            }
            else if (checkTwo != true)
            {
                Console.WriteLine("Expiration date not valid");
            }
            else if (checkThree != true) 
            {
                Console.WriteLine("Pin number not valid");
            }
            return false;
        }

        private bool ValidateCardNumber()
        {
            if ((CardNumber.Length >= 12 && CardNumber.Length <= 17) && int.TryParse(CardNumber, out _))
            {
                return true;
            }
            return false;
        }
        private bool ValidateExpirationDate()
        {
            if (CurrentDate <= ExpirationDate)
            {
                return true;
            }
            return false;
        }
        private bool ValidatePinNumber()
        {
            if ((Pin.Length == 3) && int.TryParse(CardNumber, out _))
            {
                return true;
            }
            return false;
        }

        public void Pay()
        {
            throw new NotImplementedException();
        }



    }
}
