using System;
using System.Collections;
using System.Collections.Generic;

namespace Exam_Monefy
{
    class Account
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Amount { get; set; }    
        public string Currency { get; set; }

        public List<MonefyOptions> Options = new List<MonefyOptions>();        

        public List<Category> categoriesExpense = new List<Category>()
        {
            new Category { Name="Еда",ID=1,type=Type.Expense},
            new Category { Name="Машина",ID=2,type=Type.Expense},
            new Category { Name="Кафе",ID=3,type=Type.Expense},
            new Category { Name="Транспорт",ID=4,type=Type.Expense},
            new Category { Name="Развлечения",ID=5,type=Type.Expense},
            new Category { Name="Такси",ID=6,type=Type.Expense},
            new Category { Name="Одежда",ID=7,type=Type.Expense},
            new Category { Name="Питомцы",ID=8,type=Type.Expense},
            new Category { Name="Подарки",ID=9,type=Type.Expense},
            new Category { Name="Связь",ID=10,type=Type.Expense},
            new Category { Name="Жильё",ID=11,type=Type.Expense},
            new Category { Name="Здоровье",ID=12,type=Type.Expense},
            new Category { Name="Спорт",ID=13,type=Type.Expense},
            new Category { Name="Гигиена",ID=14,type=Type.Expense}
        };
        public List<Category> categoriesIncome = new List<Category>()
        {
            new Category { Name="Наличные",ID=1,type=Type.Income }
        };
    }
}
