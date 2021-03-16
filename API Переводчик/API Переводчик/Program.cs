using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
//using System.Text.Json;

namespace Translator
{
    class Program
    {
        static void LanguageFrom(ref string languageFrom)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Выберите язык вашего текста\n" +
                                "1) Английский\n" +
                                "2) Русский\n" +
                                "3) Азербайджанский\n" +
                                "4) Вписать самому (для опытных пользователей)");
                    int languageChoose = int.Parse(Console.ReadLine());
                    if (languageChoose == 1)
                    {
                        languageFrom = "en";
                        break;
                    }
                    else if (languageChoose == 2)
                    {
                        languageFrom = "ru";
                        break;
                    }
                    else if (languageChoose == 3)
                    {
                        languageFrom = "az";
                        break;
                    }
                    else if (languageChoose == 4)
                    {
                        Console.Write("Напишите язык (синтаксис: \"en\",\"ru\"): ");
                        languageFrom = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели неверные данные!");
                        PushAndClear();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    PushAndClear();
                }
            }            
        }
        static void LanguageTo(ref string languageTo)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Выберите на какой язык вы хотите перевести\n" +
                                "1) Английский\n" +
                                "2) Русский\n" +
                                "3) Азербайджанский\n" +
                                "4) Вписать самому (для опытных пользователей)");
                    int targetLanguage = int.Parse(Console.ReadLine());
                    if (targetLanguage == 1)
                    {
                        languageTo = "en";
                        break;
                    }
                    else if (targetLanguage == 2)
                    {
                        languageTo = "ru";
                        break;
                    }
                    else if (targetLanguage == 3)
                    {
                        languageTo = "az";
                        break;
                    }
                    else if (targetLanguage == 4)
                    {
                        Console.Write("Напишите язык (синтаксис: \"en\",\"RU\"): ");
                        languageTo = Console.ReadLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели неверные данные!");
                        PushAndClear();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    PushAndClear();
                }
            }
        }
        static void PushAndClear()
        {
            Console.WriteLine("Нажмите любую кнопку для продолжения");
            Console.ReadKey();
            Console.Clear();
        }
        static void Main(string[] args)
        {
            while (true)
            {   // Сам файл "program.cs" сохраняется по умолчанию в UTF-8, 
                // а кодировка консоли обычно chcp 1251 поэтому такая проблема.

                // Можно сменить кодировку консоли по умолчанию и тогда не придется в коде ничего писать, 
                // однако на другой машине проблема опять даст о себе знать, поэтому лучше писать это.
                Console.OutputEncoding = Encoding.UTF8;

                Console.Clear();
                string languageFrom = "";
                string languageTo = "";
                string languagePath = "";
                Console.WriteLine("Приветствую тебя в программе \"Мега супер крутой и невероятный переводчик 2000\" дорогой пользователь.\n" +
                    "1) Перевести текст\n" +
                    "2) Прочесть тест с файла и перевести\n" +
                    "3) Выйти\n" +
                    "Выбор: ");
                int choose = int.Parse(Console.ReadLine());
                if (choose == 1)
                {
                    LanguageFrom(ref languageFrom);
                    LanguageTo(ref languageTo);
                }
                else if (choose == 2)
                {
                    LanguageFrom(ref languageFrom);
                    LanguageTo(ref languageTo);
                    while (true)
                    {
                        try
                        {
                            Console.Clear();
                            Console.Write("Введите путь к файлу: ");
                            languagePath = Console.ReadLine();
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }                        
                    }
                    
                }
                else if (choose == 3)
                {
                    Environment.Exit(0);
                }

                try
                {
                    Console.Clear();
                    TranslateRequest translate = new TranslateRequest() { source = languageFrom, target = languageTo, format = "text" };
                    if (choose == 1)
                    {
                        Console.Write("Введите текст: ");
                        translate.q = Console.ReadLine();
                    }
                    else if (choose == 2)
                    {
                        translate.q = File.ReadAllText(languagePath);
                        Console.WriteLine($"Ваш текст: {translate.q}");

                    }

                    WebClient web = new WebClient();
                    string url = "https://translation.googleapis.com/language/translate/v2?key=AIzaSyCqwaXLLd9JraElDHNGKFIN2zfbSAgAHms";
                    //string answer = web.UploadString(url, JsonSerializer.Serialize(translate));
                    string answer = web.UploadString(url, JsonConvert.SerializeObject(translate));

                    var response = JsonConvert.DeserializeObject<TranslateResponse>(answer);
                    //var response = JsonSerializer.Deserialize<TranslateResponse>(answer);
                    Console.Write("Ваш перевод: ");
                    Console.WriteLine(response.data.translations[0].translatedText);
                    PushAndClear();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    PushAndClear();
                }
            }
        }
    }
}