using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CreditCard
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your credit card number here:");
            string cardNumber = Console.ReadLine();
            string getVendor = GetCreditCardVendore(cardNumber);
            Console.WriteLine(getVendor);
            string onlyNumbers = GetCardOnlyNumbers(cardNumber);
            var isValid = IsCreditCardNumberValid(onlyNumbers);
            if (isValid)
            {
                Console.WriteLine("This Credit Card Number is valid according to Luhn algorithm!");
            }
            else
            {
                Console.WriteLine("Invalid Credit Card Number");
            }
            Console.ReadLine();
        }

        // Task #1
        public static string GetCreditCardVendore(string cardNumber)
        {
            Regex regAmericanExpress = new Regex(@"^3[47][\d]{2}[ -]*[\d]{4}[ -]*[\d]{4}[ -]*[\d]{3}$"); // 34, 37 (15)  
            Regex regMaestro = new Regex(@"^(5[06789][\d]{2}|6[\d]{3})[ -]*[\d]{4}[ -]*[\d]{4}([ -]*[\d]{4}[ -]*[\d]{1,3}|[ -]*[\d]{4})?$"); // 50, 56-69 (12-19)
            Regex regMasterCard = new Regex(@"^5[1-5][\d]{2}[ -]*[\d]{4}[ -]*[\d]{4}[ -]*[\d]{4}$");  // 51-55     (16)
            Regex regVisa = new Regex(@"^4[\d]{3}[ -]*[\d]{4}[ -]*[\d]{4}([ -]*[\d]{1}|[ -]*[\d]{4}|[ -]*[\d]{4}[ -]*[\d]{3})?$");      // 4 (13, 16, 19)
            Regex regJCB = new Regex(@"^35[2-8][\d]{1}[ -]*[\d]{4}[ -]*[\d]{4}[ -]*[\d]{4}$"); // 3528-3589 (16)

            if (regAmericanExpress.IsMatch(cardNumber))
                return "American Express";
            else if (regMaestro.IsMatch(cardNumber))
                return "Maestro";
            else if (regMasterCard.IsMatch(cardNumber))
                return "Master Card";
            else if (regVisa.IsMatch(cardNumber))
                return "Visa";
            else if (regJCB.IsMatch(cardNumber))
                return "JCB";
            else return "Unknown";
        }

        // Function to get only numbers. No spaces or dashes.
        public static string GetCardOnlyNumbers(string cardNumber)
        {
            Regex regNumFormat = new Regex(@"[-]|[ ]");
            if (string.IsNullOrEmpty(cardNumber))
            {
                return "It's not a number!";
            }
            if (regNumFormat.IsMatch(cardNumber))
            {
                string cardNumberNoDefis = cardNumber.Replace("-", "");
                string cardNumberCorect = cardNumberNoDefis.Replace(" ", "");
                return cardNumberCorect;
            }
            else
            {
                string cardNumberCorect = cardNumber;
                return cardNumberCorect;
            }
        }

        // Task #2
        public static bool IsCreditCardNumberValid(string ccnumber)
        {   
            int sumOfDigits = ccnumber.Where((e) => e >= '0' && e <= '9')
                            .Reverse()
                            .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
                            .Sum((e) => e / 10 + e % 10);
        
            return sumOfDigits % 10 == 0;
        }
    }
}
