using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Monefy
{
    enum Type { Expense, Income }
    [Serializable]
    class Category
    {
        public string Name { get; set; }
        public Type type { get; set; }
        public int ID { get; set; }
        public int MoneySpent { get; set; }
        public override string ToString()
        {
            return $"{ID}. {Name} = {MoneySpent}";
        }
    }
}
