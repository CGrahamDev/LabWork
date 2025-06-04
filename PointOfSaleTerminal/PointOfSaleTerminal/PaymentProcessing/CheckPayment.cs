using PointOfSaleTerminal.IPaymentProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleTerminal.PaymentProcessing
{
    internal class CheckPayment : IPaymentProcessor
    {
        public string? CheckNumber { get; set; }
        public decimal Cost { get; set; }
        public bool IsUsed { get; private set; } = false;
        
        /// <summary>
        /// 9 digit number for check
        /// </summary>
        /// <param name="checkNumber"></param>
        public CheckPayment(string checkNumber, decimal cost, decimal paymentTarget)
        {
            if (ValidateCheckNumber(checkNumber.Trim()) == true)
            {
                Cost = cost;
                Pay(paymentTarget);
            }
            else 
            {
                throw new ArgumentException("Check number must be 9 characters long and only numbers");    
            }

        }

        public void Pay(decimal paymentTarget)
        {
            if (IsUsed == false)
            {
                paymentTarget += Cost;
                IsUsed = true;
            }
            else
            {
                throw new Exception("Check has already been used and cannot be used again");
            }
        }
        public bool ValidateCheckNumber(string checkNumber)
        {
            if ((checkNumber.Length == 9) && int.TryParse(checkNumber, out _))
            {
                CheckNumber = checkNumber;
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
