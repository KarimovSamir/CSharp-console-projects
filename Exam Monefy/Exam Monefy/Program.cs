using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;

namespace Exam_Monefy
{    
    class Program
    {
        static Account[] accounts = new Account[1];
        static Money money = new Money();
        static void Main(string[] args)
        {
            int index = 0;
            if (File.Exists("accoutns.json"))
            {
                ReadFromFile(ref accounts);
                index = accounts.Length;
            }  
            while (true)
            {
                try
                {
                    Console.Clear();
                    bool exit = false;
                    Console.Write("Приветствую вас в Sam Monefy.\n1) Зарегистрироваться\n2) Войти\n3) Выйти\nВыбор: ");
                    int choose = int.Parse(Console.ReadLine());
                    if (choose == 1)
                    {
                        if (index == accounts.Length)
                        {
                            Array.Resize(ref accounts, accounts.Length + 1);
                        }
                        accounts[index] = new Account();
                        RegistrationNewAccount(index);
                        index++;
                        WriteToFile();
                    }
                    else if (choose == 2)
                    {
                        if (index == 0)
                        {
                            Console.WriteLine("Пользователей не существует. Создайте новый аккаунт");
                            Thread.Sleep(1000);
                            continue;
                        }
                        while (exit == false)
                        {
                            Console.Clear();
                            Console.Write("Введите логин: ");
                            string userLogin = Console.ReadLine();
                            index = CheckUser(userLogin);
                            if (index != -1)
                            {
                            line1:
                                Console.Write("Введите пароль: ");
                                string password = Console.ReadLine();
                                if (accounts[index].Password == password)
                                {
                                    while (true)
                                    {
                                        WriteToFile();
                                        Console.Clear();
                                        //основная прога
                                        Console.WriteLine($"Вы в Sam Monefy!\nВаш баланс: {accounts[index].Amount} {accounts[index].Currency}");
                                        Console.WriteLine("1. Потратить деньги" +
                                                        "\n2. Добавить деньги" +
                                                        "\n3. Аккаунт" +
                                                        "\n4. Категории" +
                                                        "\n5. Статистика трат" +
                                                        "\n6. Выйти из аккаунта" +
                                                        "\n7) Выход");
                                        int menuChoose = int.Parse(Console.ReadLine());
                                        if (menuChoose == 1)
                                        {
                                            Console.Clear();
                                            SpendMoney(index);
                                        }
                                        else if (menuChoose == 2)
                                        {
                                            Console.Clear();
                                            AddMoney(accounts[index].categoriesIncome, index);
                                        }
                                        else if (menuChoose == 3)
                                        {
                                            Console.Clear();
                                            Console.WriteLine($"Баланс: {accounts[index].Amount} {accounts[index].Currency}");
                                            CatagoryIncomeScreen(index);
                                            Console.WriteLine("Валюта: " + accounts[index].Currency);
                                            Console.WriteLine("Логин: " + accounts[index].Login);
                                            Console.WriteLine("Пароль: " + accounts[index].Password);
                                            Console.WriteLine("Имя: " + accounts[index].Name);
                                            Console.WriteLine("Фимилия: " + accounts[index].Surname);
                                            Console.ReadKey();
                                        }
                                        else if (menuChoose == 4)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Категории.");
                                            CatagoryExpanseScreen(index);
                                            Console.WriteLine("\n==================================================\n");
                                            CatagoryIncomeScreen(index);
                                            Console.WriteLine("\n1) Добавить категорию" +
                                                "\n2) Добавить карту оплаты" +
                                                "\n3) Удалить категорию" +
                                                "\n4) Удалить карту" +
                                                "\n5) Вернуться в предыдущее меню" +
                                                "\nВыбор:");
                                            int chooseCategory = int.Parse(Console.ReadLine());
                                            if (chooseCategory == 1)
                                            {
                                                AddCategory(accounts[index].categoriesExpense, Type.Expense);
                                            }
                                            else if (chooseCategory == 2)
                                            {
                                                AddCategory(accounts[index].categoriesIncome, Type.Income, 1);
                                            }
                                            else if (chooseCategory == 3)
                                            {
                                                DeleteCategory(accounts[index].categoriesExpense, index);
                                            }
                                            else if (chooseCategory == 4)
                                            {
                                                DeleteCategory(accounts[index].categoriesIncome, index);
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                        }
                                        else if (menuChoose == 5)
                                        {
                                            Date(index);
                                        }
                                        else if (menuChoose == 6)
                                        {
                                            exit = true;
                                            break;
                                        }
                                        else if (menuChoose == 7)
                                        {
                                            Environment.Exit(0);
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Пароль не верный. Введите его заново");
                                    Thread.Sleep(1000);
                                    goto line1;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Такого логина не существует. Введите его заново");
                                Thread.Sleep(1000);
                                continue;
                            }
                        }
                    }
                    else if (choose == 3)
                    {
                        Environment.Exit(0);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }            
        }
        static public void Date(int index)
        {
            Console.Write("1) Задать интервал дат\n2) Годовая, месячная и дневная статистика\nВыбор: ");
            int typeSelect = int.Parse(Console.ReadLine());
            if (typeSelect == 1)
            {
                Console.Write("1) Задать любой интервал\n2) Недельный интервал\nВыбор: ");
                typeSelect = int.Parse(Console.ReadLine());
                if (typeSelect == 1)
                {
                    Console.Write("Введите \"от\" какого года: ");
                    int year = int.Parse(Console.ReadLine());
                    Console.Write("Введите \"от\" какого месяца: ");
                    int month = int.Parse(Console.ReadLine());
                    Console.Write("Введите \"от\" какого дня: ");
                    int day = int.Parse(Console.ReadLine());
                    DateTime date1 = new DateTime(year, month, day);

                    Console.Write("Введите \"к\" какому году: ");
                    year = int.Parse(Console.ReadLine());
                    Console.Write("Введите \"к\" какому месяцу: ");
                    month = int.Parse(Console.ReadLine());
                    Console.Write("Введите \"к\" какому дню: ");
                    day = int.Parse(Console.ReadLine());
                    DateTime date2 = new DateTime(year, month, day);
                    Statistics(date1, date2, index);
                }
                else if (typeSelect == 2)
                {
                    Console.Write("Введите \"от\" какого года: ");
                    int year = int.Parse(Console.ReadLine());
                    Console.Write("Введите \"от\" какого месяца: ");
                    int month = int.Parse(Console.ReadLine());
                    Console.Write("Введите \"от\" какого дня: ");
                    int day = int.Parse(Console.ReadLine());
                    DateTime date1 = new DateTime(year, month, day);
                    DateTime date2 = new DateTime();
                    date2 = date1;
                    date2 = date2.AddDays(7);
                    Statistics(date1, date2, index);
                }
            }
            else if (typeSelect == 2)
            {
                Console.WriteLine("1) Дневная статистика\n2) Месячная статистика\n3) Годовая статистика\nВыбор: ");
                typeSelect = int.Parse(Console.ReadLine());
                if (typeSelect == 1)
                {
                    Console.Write("Введите год: ");
                    int year = int.Parse(Console.ReadLine());
                    Console.Write("Введите месяц: ");
                    int month = int.Parse(Console.ReadLine());
                    Console.Write("Введите день: ");
                    int day = int.Parse(Console.ReadLine());
                    DateTime date1 = new DateTime(year, month, day);
                    DateTime date2 = new DateTime(year, month, day, 23, 59, 59);
                    Statistics(date1, date2, index);
                }
                else if (typeSelect == 2)
                {
                    Console.Write("Введите год: ");
                    int year = int.Parse(Console.ReadLine());
                    Console.Write("Введите месяц: ");
                    int month = int.Parse(Console.ReadLine());
                    DateTime date1 = new DateTime(year, month, 1);
                    DateTime date2 = new DateTime(year, month, 31);
                    Statistics(date1, date2, index);
                }
                else if (typeSelect == 3)
                {
                    Console.Write("Введите год: ");
                    int year = int.Parse(Console.ReadLine());
                    DateTime date1 = new DateTime(year, 1, 1);
                    DateTime date2 = new DateTime(year, 12, 31);
                    Statistics(date1, date2, index);
                }
            }
            Console.WriteLine("Нажмите любую кнопку для продолжения");
            Console.ReadKey();
        }
        static public void Statistics(DateTime date1, DateTime date2, int index)
        {
            List<MonefyOptions> result = new List<MonefyOptions>();
            result = accounts[index].Options.Where(x => x.Date >= date1 && x.Date <= date2 && x.MoneySpent != 0).ToList();
            double sum = result.Sum(x => x.MoneySpent);
            if (sum != 0)
            {
                Dictionary<int, double> categorySpend = new Dictionary<int, double>();
                foreach (var item in result)
                {
                    categorySpend.Add(item.IdCategory, 0);
                }
                for (int i = 0; i < result.Count; i++)
                {
                    categorySpend[result[i].IdCategory] += result[i].MoneySpent;
                }
                foreach (var item in result)
                {
                    Console.WriteLine($"{accounts[index].categoriesExpense[item.IdCategory].Name}: " +
                        $"{categorySpend[item.IdCategory]} {accounts[index].Currency}" +
                        $"\nВ процентах: {Math.Round(categorySpend[item.IdCategory] / sum * 100, 2)}%\n");
                }
            }
            else
            {
                Console.WriteLine("В этот период нет никаких трат");
            }
        }
        static public void AddDate(int index, int expanseChoose, int moneySpent)
        {
            Console.Write("1) Назначить дату\n2) Дефолтная дата\nВыбор: ");
            int optionDate = int.Parse(Console.ReadLine());
            DateTime date;
            if (optionDate == 1)
            {
                Console.Write("Введите год: ");
                int day = int.Parse(Console.ReadLine());
                Console.Write("Введите месяц: ");
                int month = int.Parse(Console.ReadLine());
                Console.Write("Введите день: ");
                int year = int.Parse(Console.ReadLine());
                date = new DateTime(year, month, day);
            }
            else
            {
                date = DateTime.Now;
            }
            accounts[index].Options.Add(new MonefyOptions { IdAccount = index, IdCategory = expanseChoose, MoneySpent = moneySpent, Date = date });
        }
        static public void DeleteCategory(List<Category> categories, int index)
        {
            while (true)
            {
                Console.Clear();
                int choose = 0;
                if (accounts[index].categoriesExpense == categories)
                {
                    CatagoryExpanseScreen(index);
                    Console.Write("Выберите категорию: ");
                    choose = int.Parse(Console.ReadLine());
                }
                else if (accounts[index].categoriesIncome == categories)
                {
                    CatagoryIncomeScreen(index);
                    Console.Write("Выберите категорию: ");
                    choose = int.Parse(Console.ReadLine());
                }
                
                foreach (var item in categories)
                {
                    if (item.ID == choose)
                    {
                        categories.Remove(item);
                        Console.WriteLine($"\"{item.Name}\" успешно удалено");
                        Thread.Sleep(1000);
                        return;
                    }
                }
                Console.WriteLine("Категория с таким номером не найдена");
                Thread.Sleep(1000);
            }
        }
        static public void AddCategory(List<Category> categories, Type type, int choice = 0)
        {
            while (true)
            {
                Console.Clear();
                Category category = new Category();
                if (choice == 0)
                {
                    Console.Write("Введите название категории: ");
                    category.Name = Console.ReadLine();
                }
                else if (choice == 1)
                {
                    Console.Write("Введите название карты\n(Пример: 1234 1234 1234 1234)\nВвод: ");
                    Regex regex = new Regex("^[0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4}$");
                    category.Name = Console.ReadLine();
                    Match match = regex.Match(category.Name);
                    if (!match.Success)
                    {
                        Console.WriteLine("Вы ввели данные неверно!");
                        Thread.Sleep(1000);
                        continue;
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Ошибка");
                    return;
                }
                category.type = type;
                if (categories.Count == 0)
                {
                    categories.Add(category);
                    categories[0].ID = 1;
                    Console.WriteLine($"\"{category.Name}\" успешно добавлено");
                    Thread.Sleep(1000);
                    break;
                }
                else
                {
                    category.ID = categories.Last().ID + 1;
                    categories.Add(category);
                    Console.WriteLine($"\"{category.Name}\" успешно добавлено");
                    Thread.Sleep(1000);
                    break;
                }
            }
        }
        static public void AddMoney(List<Category> categories, int index)
        {
            while (true)
            {
                Console.Clear();
                int moneyValue;
                Console.Write("Введите колличество денег: ");
                int moneySpent = int.Parse(Console.ReadLine());
                if (moneySpent <= 0)
                {
                    Console.WriteLine($"Денег не может быть {moneySpent}. Ошибка");
                    Thread.Sleep(1000);
                    continue;
                }
                Console.Write("Впишите название валюты (AZN, USD, EUR)\nВвод: ");
                string chooseValue = Console.ReadLine();
                if (chooseValue != "AZN" && chooseValue != "USD" && chooseValue != "EUR")
                {
                    Console.WriteLine("Ошибка. Неверно введена валюты");
                    Thread.Sleep(1000);
                    continue;
                }
                else
                {
                    moneyValue = money.MoneyConverter(moneySpent, accounts[index].Currency, chooseValue);
                }
                CatagoryIncomeScreen(index);
                Console.Write("Выберите куда добавить деньги: ");
                int chooseIncome = int.Parse(Console.ReadLine());
                if (chooseIncome <= categories.Count)
                {
                    categories[--chooseIncome].MoneySpent += moneyValue;
                    accounts[index].Amount += moneyValue;
                    break;
                }
                else
                {
                    Console.WriteLine("Ошибка. Неверно введены данные");
                    Thread.Sleep(1000);
                    continue;
                }
            }
        }
        static public void SpendMoney(int index)
        {
            while (true)
            {
                Console.Clear();
                int moneyValue;
                Console.Write("Введите колличество денег: ");
                int moneySpent = int.Parse(Console.ReadLine());
                if (moneySpent <= 0)
                {
                    Console.WriteLine($"Денег не может быть {moneySpent}. Ошибка");
                    Thread.Sleep(1000);
                    continue;
                }                
                Console.Write("Впишите название валюты (AZN, USD, EUR)\nВвод: ");
                string chooseValue = Console.ReadLine();
                if (chooseValue != "AZN" && chooseValue != "USD" && chooseValue != "EUR")
                {
                    Console.WriteLine("Ошибка. Введите валюту правильно");
                    Thread.Sleep(1000);
                    continue;
                }
                else
                {
                    moneyValue = money.MoneyConverter(moneySpent, accounts[index].Currency, chooseValue);
                }

                CatagoryExpanseScreen(index);
                Console.Write("Выберите категорию: ");
                int expanseChoose = int.Parse(Console.ReadLine());              
                if (expanseChoose == accounts[index].categoriesExpense[--expanseChoose].ID)
                {
                    accounts[index].categoriesExpense[expanseChoose].MoneySpent += moneyValue;
                    accounts[index].Amount -= moneyValue;
                    AddDate(index, expanseChoose, moneyValue);
                    break;
                }
                else
                {
                    Console.WriteLine("Ошибка. Вы неправильно выбрали категорию");
                    Thread.Sleep(1000);
                    continue;
                }
            }
        }        
        static public void CatagoryIncomeScreen(int index)
        {
            foreach (var item in accounts[index].categoriesIncome)
            {
                Console.WriteLine($"{item.ToString()} {accounts[index].Currency}");
            }
        }
        static public void CatagoryExpanseScreen(int index)
        {
            foreach (var item in accounts[index].categoriesExpense)
            {
                Console.WriteLine($"{item.ToString()} {accounts[index].Currency}");
            }
        }
        static void WriteToFile()
        {
            File.WriteAllText("curency.json", JsonSerializer.Serialize(money));
            File.WriteAllText("accoutns.json", JsonSerializer.Serialize(accounts));
            File.WriteAllText("monefyCategoryExpense.json", JsonSerializer.Serialize(accounts[0].categoriesExpense));
            File.WriteAllText("monefyCategoryIncome.json", JsonSerializer.Serialize(accounts[0].categoriesIncome));
            File.WriteAllText("monefyOptions.json", JsonSerializer.Serialize(accounts[0].Options));
        }
        static void ReadFromFile(ref Account[] accounts)
        {
            var json = File.ReadAllText("curency.json");
            money = JsonSerializer.Deserialize<Money>(json);

            var json2 = File.ReadAllText("accoutns.json");
            accounts = JsonSerializer.Deserialize<Account[]>(json2);

            var json3 = File.ReadAllText("monefyCategoryExpense.json");
            accounts[0].categoriesExpense = JsonSerializer.Deserialize<List<Category>>(json3);

            var json4 = File.ReadAllText("monefyCategoryIncome.json");
            accounts[0].categoriesIncome = JsonSerializer.Deserialize<List<Category>>(json4);

            var json5 = File.ReadAllText("monefyOptions.json");
            accounts[0].Options = JsonSerializer.Deserialize<List<MonefyOptions>>(json5);
        }
        static int CheckUser(string userLogin)
        {
            for (int index = 0; index < accounts.Length; index++)
            {
                if (accounts[index].Login == userLogin)
                {
                    return index;
                }
            }
            return -1;
        }
        static void RegistrationNewAccount(int index)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Введите имя");
                accounts[index].Name = Console.ReadLine();
                Console.WriteLine("Введите фамилию");
                accounts[index].Surname = Console.ReadLine();
            line2:
                Console.WriteLine("Введите логин");
                string login = Console.ReadLine();
                foreach (var item in accounts)
                {
                    if (login == item.Login)
                    {
                        Console.WriteLine("Такой логин существует, введите его заного");
                        Thread.Sleep(1000);
                        Console.Clear();
                        goto line2;
                    }
                }
                accounts[index].Login = login;
                Console.WriteLine("Введите пароль");
                accounts[index].Password = Console.ReadLine();
                line3:
                Console.WriteLine("Введите тип валюты (USD, AZN, EUR)");
                accounts[index].Currency = Console.ReadLine();
                if (accounts[index].Currency == "USD" || accounts[index].Currency == "AZN" || accounts[index].Currency == "EUR")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверные данные");
                    Thread.Sleep(1000);
                    Console.Clear();
                    goto line3;
                }
            }
        }
    }
}
