using System;
using System.Collections.Generic;
using System.Threading;

namespace Exam_Monefy
{
    class Money
    {
        //public int AZN { get => azn; set => azn = value; }
        //public double USD { get => usd; set => usd = value; }
        //public int EUR { get => eur; set => eur = value; }
        static public int azn = 1;
        static public double usd = 1.7;
        static public int eur = 2;
        public override string ToString()
        {
            return $"{azn} {usd} {eur}";
        }    
        public int MoneyConverter(int amountOfMoney, string fromValue, string toValue)
        {
            if (fromValue == "USD")
            {
                if (toValue == "USD")
                {
                    return amountOfMoney;
                }
                else if (toValue == "EUR")
                {
                    return (int)((amountOfMoney * usd) / eur);
                }
                else if (toValue == "AZN")
                {
                    return (int)(amountOfMoney * usd);
                }
            }
            else if (fromValue == "EUR")
            {
                if (toValue == "EUR")
                {
                    return amountOfMoney;
                }
                else if (toValue == "USD")
                {
                    return (int)((amountOfMoney * eur) / usd);
                }
                else if (toValue == "AZN")
                {
                    return amountOfMoney * eur;
                }
            }
            else if (fromValue == "AZN")
            {
                if (toValue == "AZN")
                {
                    return amountOfMoney;
                }
                else if (toValue == "EUR")
                {
                    return (int)(amountOfMoney * eur);
                }
                else if (toValue == "USD")
                {
                    return (int)(amountOfMoney * usd);
                }
            }
            return 0;
        }
    }
}
