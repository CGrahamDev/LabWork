using PointOfSaleTerminal.IPaymentProcessing;
using System.Linq;

namespace PointOfSaleTerminal.PaymentProcessing
{
    internal class CreditPayment : IPaymentProcessor
    {
        public string CardNumber { get; private set; } //12 char length min string 17 max. Must return true using int.TryParse
        public DateTime ExpirationDate { get; set; }
        public string CVV { get; private set; } //3 char length. Must return true using int.TryParse
        public decimal Balance { get; set; }
        public decimal Cost { get; set; }
        public string AnonymousCardNumber { get; set; }

        public static DateTime CurrentDate = DateTime.Today;


        public CreditPayment(string cardNumber, DateTime expirationDate, string cvv, decimal balance)
        {
            CardNumber = cardNumber;
            ExpirationDate = expirationDate;
            CVV = cvv;
            Balance = balance;
            if (ValidateCardInfo(cardNumber, expirationDate, cvv) == false)
            {
                throw new ArgumentException("Card Info is Invalid");
            }
            SetCardAnonymousNumber();
        }
        public CreditPayment(string cardNumber, DateTime expirationDate, string cvv, decimal balance, decimal cost, decimal paymentTarget)
        {
            CardNumber = cardNumber;
            ExpirationDate = expirationDate;
            CVV = cvv;
            Balance = balance;
            if (ValidateCardInfo(cardNumber, expirationDate, cvv) == false)
            {
                throw new ArgumentException("Card Info is Invalid");
            }
            char[] chars = cardNumber.ToCharArray();
            int cardNumLength = chars.Length;
            List<string> anonCardNum = new List<string>();
            for (int i = cardNumLength - 5; i < chars.Length; i++)
            {
                anonCardNum.Add(chars[i].ToString());
            };
            AnonymousCardNumber = $"****-****-****-{anonCardNum[0]}{anonCardNum[1]}{anonCardNum[2]}{anonCardNum[3]}";
            Pay(paymentTarget);
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


        private bool ValidateCardInfo(string cardNumber, DateTime expirationDate, string cvv)
        {
            bool checkOne = false;
            bool checkTwo = false;
            bool checkThree = false;

            checkOne = ValidateCardNumber(cardNumber);
            checkTwo = ValidateExpirationDate(expirationDate);
            checkThree = ValidatePinNumber(cvv);
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

        private bool ValidateCardNumber(string cardNumber)
        {

            char[] chars = cardNumber.ToCharArray();
            foreach (char c in chars)
            {
                try
                {
                    int.Parse(c.ToString());
                }
                catch (FormatException)
                {
                    return false;
                }
            }

            if ((cardNumber.Length >= 12 && cardNumber.Length <= 17))
            {
                CardNumber = cardNumber;
                return true;
            }
            return false;
        }
        private bool ValidateExpirationDate(DateTime expirationDate)
        {
            if (CurrentDate <= expirationDate)
            {
                ExpirationDate = expirationDate;
                return true;
            }
            return false;
        }
        private bool ValidatePinNumber(string cvv)
        {
            if ((cvv.Length == 3) && int.TryParse(cvv, out _))
            {
                CVV = cvv;
                return true;
            }
            return false;
        }
        private void SetCardAnonymousNumber()
        {
            char[] chars = CardNumber.ToCharArray();
            int cardNumLength = chars.Length;
            List<string> anonCardNum = new List<string>();
            for (int i = cardNumLength - 5; i < chars.Length; i++)
            {
                anonCardNum.Add(chars[i].ToString());
            };
            AnonymousCardNumber = $"****-****-****-{anonCardNum[0]}{anonCardNum[1]}{anonCardNum[2]}{anonCardNum[3]}";
        }
        public void Pay()
        {
            throw new NotImplementedException();
        }



    }
}
