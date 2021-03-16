using System;
using System.Threading;
using System.IO;
using System.Text.Json;

namespace AtmHomework
{
    class Program
    {
        static int CheckUser(Account[] accounts, string userCardNumber)
        {
            for (int user = 0; user < accounts.Length; user++)
            {
                if (userCardNumber == accounts[user].CardNumber)
                {
                    return user;
                }
            }
            return -1;
        }
        static string ChooseValueType()
        {
            while (true)
            {
                Console.Write("\t\t\t\tВыберите тип валюты: \n\t\t\t\t1) AZN \n\t\t\t\t2) USD \n\t\t\t\t3) EUR\n\t\t\t\t");
                int currentChoose = int.Parse(Console.ReadLine());
                Console.Beep(400, 200);
                if (currentChoose == 1)
                {
                    return "AZN";
                }
                else if (currentChoose == 2)
                {
                    return "USD";
                }
                else if (currentChoose == 3)
                {
                    return "EUR";
                }
                else
                {
                    Console.Write("\t\t\t\tВы ввели неверные данные\n");
                    Thread.Sleep(1000);
                    Console.Clear();
                    continue;
                }
            }
        }
        static int HowManyDigits(int number)
        {
            int index = 0;
            while (number > 0)
            {
                index++;
                number /= 10;
            }
            return index;
        }
        static void RegistrationNewCard(Account[] accounts, int index)
        {
            Console.Write("\t\t\t\tВведите имя: ");
            accounts[index].Name = Console.ReadLine();
            Console.Beep(400, 200);
            Console.Write("\t\t\t\tВведите фамилию: ");
            accounts[index].Surname = Console.ReadLine();
            Console.Beep(400, 200);
            while (true)
            {
            line1:                
                Console.Write("\t\t\t\tВведите номер карты: ");
                accounts[index].CardNumber = Console.ReadLine();
                Console.Beep(400, 200);
                for (int checkCardNumber = 0; checkCardNumber < index; checkCardNumber++)
                {
                    if (accounts[index].CardNumber == accounts[checkCardNumber].CardNumber)
                    {
                        Console.Write("\t\t\t\tТакой номер карты уже существует. Введите другой\n");
                        goto line1;
                    }
                }
                if (accounts[index].CardNumber.Length == 16)
                {
                    break;
                }
                else
                {
                    Console.Write("\t\t\t\tНеверные данные. Номер карты должен состоять из 16 цифр. Введите заного\n");
                }
            }
            while (true)
            {
                Console.Write("\t\t\t\tВведите PIN код: ");
                accounts[index].Pin = int.Parse(Console.ReadLine());
                Console.Beep(400, 200);
                if (HowManyDigits(accounts[index].Pin) == 4)
                {
                    break;
                }
                else
                {
                    Console.Write("\t\t\t\tНеверные данные. PIN код должен состоять из 4 цифр. Введите заного\n");
                }
            }
            // Выбор тип валюты
            accounts[index].Currency = ChooseValueType();
        }
        static void WriteToFile(Account[] accounts, Money money)
        {
            //// Binary вариант (в проверке в Main надо исправить формат файла JSON)
            //BinaryFormatter formatter = new BinaryFormatter();
            //using (var item = new FileStream("curency.txt", FileMode.OpenOrCreate))
            //{
            //    formatter.Serialize(item, money);
            //}

            //using (var item = new FileStream("accoutns.txt", FileMode.OpenOrCreate))
            //{
            //    formatter.Serialize(item, accounts);
            //}

            // JSON вариант
            File.WriteAllText("curency.json", JsonSerializer.Serialize(money));
            File.WriteAllText("accoutns.json", JsonSerializer.Serialize(accounts));
        }
        static void ReadFromFile(ref Account[] accounts, Money money)
        {
            //// Binary вариант(в проверке в Main надо исправить формат файла JSON)
            //BinaryFormatter formatter = new BinaryFormatter();
            //using (var item = new FileStream("curency.txt", FileMode.Open))
            //{
            //    money = formatter.Deserialize(item) as Money;
            //}

            //using (var item = new FileStream("accoutns.txt", FileMode.Open))
            //{
            //    accounts = formatter.Deserialize(item) as Account[];
            //}

            // JSON вариант
            var json = File.ReadAllText("curency.json");
            money = JsonSerializer.Deserialize<Money>(json);

            var json2 = File.ReadAllText("accoutns.json");
            accounts = JsonSerializer.Deserialize<Account[]>(json2);
        }
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            int index = 0;
            Account[] accounts = new Account[1];
            Money money = new Money();

            if (File.Exists("curency.json") && File.Exists("accoutns.json"))
            {
                ReadFromFile(ref accounts, money);
                index = accounts.Length;
            }
            while (true)
            {
                bool whileExit = true;
                Console.Clear();
                Console.WriteLine("\t\t\t\t1) Регистрация новой карты\n\t\t\t\t2) Вставить карту\n\t\t\t\t3) Выйти\n\t\t\t\tВыбор: ");
                Console.SetCursorPosition(39, 3);
                int choose = int.Parse(Console.ReadLine());
                Console.Beep(400, 200);
                if (choose == 1)
                {
                    if (index == accounts.Length)
                    {
                        Array.Resize(ref accounts, accounts.Length + 1);
                    }
                    accounts[index] = new Account();
                    // Метод регистрирующий новую карту
                    RegistrationNewCard(accounts, index);
                    index++;
                    // Метод запись в файл
                    WriteToFile(accounts, money);
                }
                else if (choose == 2)
                {
                    if (index == 0)
                    {
                        Console.WriteLine("\t\t\t\tУ вас нет карты, зарегистрируйтесь\n\t\t\t\tНажмите любую кнопку для продолжения");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }
                    string userCardNumber;
                    int userPin;
                    while (whileExit == true)
                    {
                        if (whileExit == false)
                        {
                            break;
                        }
                        Console.Write("\t\t\t\tВведите номер карты: ");
                        userCardNumber = Console.ReadLine();
                        Console.Beep(400, 200);
                        int user = CheckUser(accounts, userCardNumber);
                        if (user != -1)
                        {
                        line2:
                            Console.Write("\t\t\t\tВведите PIN: ");
                            userPin = int.Parse(Console.ReadLine());
                            Console.Beep(400, 200);
                            if (userPin == accounts[user].Pin)
                            {
                                while (true)
                                {
                                    // Метод запись в файл
                                    WriteToFile(accounts, money);
                                    Console.Clear();
                                    Console.Write("\t\t\t\tВы вошли в систему, выберите операцию\n");
                                    Console.Write("\t\t\t\t1) Положить деньги\n\t\t\t\t2) Снять деньги\n\t\t\t\t3) Перевод с карты на карту\n\t\t\t\t4) Информация о пользователе\n\t\t\t\t5) Вернуть карту\n\t\t\t\tВыбор: ");
                                    int choose2 = int.Parse(Console.ReadLine());
                                    Console.Beep(400, 200);
                                    if (choose2 == 1)
                                    {
                                        /////////////////////////////////////////////////////////////////////
                                        string valueTypeIn = ChooseValueType();
                                        Console.Write("\t\t\t\tВведите сумму для вклада денег: ");
                                        int moneyIn = int.Parse(Console.ReadLine());
                                        Console.Beep(400, 200);
                                        money.CashIn(accounts, user, valueTypeIn, moneyIn);
                                        Console.Write("\t\t\t\tДеньги успешно перечислены на вашу карту");
                                        Thread.Sleep(1000);

                                    }
                                    else if (choose2 == 2)
                                    {
                                        string valueTypeOut = ChooseValueType();
                                        Console.Write("\t\t\t\tВведите сумму для снятия денег: ");
                                        int moneyOut = int.Parse(Console.ReadLine());
                                        Console.Beep(400, 200);
                                        money.CashOut(accounts, user, valueTypeOut, moneyOut);
                                        Console.Write("\t\t\t\tДеньги успешно сняты с вашей карты");
                                        Thread.Sleep(1000);
                                    }
                                    else if (choose2 == 3)
                                    {
                                        if (index == 1)
                                        {
                                            Console.WriteLine("\t\t\t\tВы единственный клиент банка :(\n\t\t\t\tНажмите любую кнопку для продолжения");
                                            Console.ReadKey();
                                            Console.Clear();
                                            continue;
                                        }
                                        else
                                        {
                                            string cardNumberReceiver;
                                            Console.Write("\t\t\t\tВведите номер карты получателя: ");
                                            cardNumberReceiver = Console.ReadLine();
                                            Console.Beep(400, 200);
                                            int receiverCard = CheckUser(accounts, userCardNumber);
                                            if (receiverCard != -1)
                                            {
                                            }
                                            for (int receiver = 0; receiver < index; receiver++)
                                            {
                                                if (cardNumberReceiver == accounts[receiver].CardNumber)
                                                {
                                                    int receiverMoney;
                                                    Console.Write("\t\t\t\tВведите сумму: ");
                                                    receiverMoney = int.Parse(Console.ReadLine());
                                                    Console.Beep(400, 200);

                                                    if (receiverMoney > accounts[user].Amount) //Если денег на счету недостаточно
                                                    {
                                                        Console.Write($"\t\t\t\tУ вас недостаточно денег на счету\n\t\t\t\tНа вашем счету {accounts[user].Amount} {accounts[user].Currency}\n\t\t\t\tНажмите любую кнопку для продолжения\n");
                                                        Console.ReadKey();
                                                        Console.Clear();
                                                        continue;
                                                    }
                                                    else //Денег на счету хватает
                                                    {
                                                        //cardToCard(Account[] accounts, int user_from, int user_to, int amount)
                                                        money.CardToCard(accounts, user, receiver, receiverMoney);
                                                        Console.WriteLine($"\t\t\t\tДеньги успешно перечислены на карту {accounts[receiver].CardNumber}");
                                                        Thread.Sleep(2000);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (choose2 == 4)
                                    {
                                        Console.WriteLine($"\t\t\t\tИмя: {accounts[user].Name}" +
                                            $"\n\t\t\t\tФамилия: {accounts[user].Surname}" +
                                            $"\n\t\t\t\tНомер карты: {accounts[user].CardNumber}" +
                                            $"\n\t\t\t\tНа вашем счету {accounts[user].Amount} {accounts[user].Currency}" +
                                            $"\n\t\t\t\tНажмите любую кнопку для продолжения");
                                        Console.ReadKey();
                                        continue;
                                    }
                                    else if (choose2 == 5)
                                    {
                                        whileExit = false;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Console.Write("\t\t\t\tPIN неверный, введите его заново\n");
                                Thread.Sleep(1000);
                                Console.Clear();
                                goto line2;
                            }
                        }
                        else
                        {
                            Console.Write("\t\t\t\tНомер карты неверный, введите его заново\n");
                            Thread.Sleep(1000);
                            Console.Clear();
                        }

                    }
                }
                else if (choose == 3)
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("\t\t\t\tВы ввели неверные данные");
                    Thread.Sleep(1000);
                    Console.Clear();
                    continue;
                }
            }
        }
    }
}