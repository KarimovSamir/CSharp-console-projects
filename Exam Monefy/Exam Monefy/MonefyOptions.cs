using System;
using System.Collections.Generic;

namespace Exam_Monefy
{
    [Serializable]
    class MonefyOptions
    {
        public DateTime Date { get; set; } = new DateTime();
        public int IdAccount { get; set; }
        public int IdCategory { get; set; }
        public double MoneySpent { get; set; }
        public override string ToString()
        {
            return $"Category ID: {IdCategory}\nMoney spent: { MoneySpent}\n" +
                $"\nDate: { Date.ToString()}";
        }
    }
}
