using System;

namespace AtmHomework
{
    [Serializable]
    class Account
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Pin { get; set; }
        public string CardNumber { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public override string ToString()
        {
            return $"{Name} {Surname} {Pin} {CardNumber} {Amount} {Currency}\n";
        }
    }
}