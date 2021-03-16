using System;
using System.Threading;

namespace AtmHomework
{
    [Serializable]
    class Money
    {
        public int Azn { get => azn; set => azn = value; }
        public double Usd { get => usd; set => usd = value; }
        public int Eur { get => eur; set => eur = value; }
        static public int azn = 1;
        static public double usd = 1.7;
        static public int eur = 2;
        public override string ToString()
        {
            return $"{azn} {usd} {eur}";
        }
        public void CardToCard(Account[] accounts, int fromUser, int toUser, int amount)
        {
            if (accounts[fromUser].Amount < amount)
            {
                Console.WriteLine("На вашем счету недостаточно средств");
                Thread.Sleep(1000);
            }
            else
            {
                int value = MoneyConverter(amount, accounts[fromUser].Currency, accounts[toUser].Currency);
                accounts[toUser].Amount += value;
                accounts[fromUser].Amount -= amount;
            }
        }
        public void CashIn(Account[] accounts, int user, string valueType, int moneyIn)
        {
            accounts[user].Amount += MoneyConverter(moneyIn, accounts[user].Currency, valueType);
        }
        public void CashOut(Account[] accounts, int user, string valueType, int moneyIn)
        {
            int value = MoneyConverter(moneyIn, accounts[user].Currency, valueType);
            if (accounts[user].Amount < value)
            {
                Console.WriteLine("На вашем счету недостаточно средств");
                Thread.Sleep(1000);
            }
            else
            {
                accounts[user].Amount -= value;
            }
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