using System;
using System.Net;
using System.Threading;
using System.Text.Json;

namespace WeatherForecast
{
    class Program
    {
        public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
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
            {
                try
                {
                    Console.Clear();
                    Console.Write("Введите город: ");
                    string countryName = Console.ReadLine();
                    WebClient webClient = new WebClient();
                    var url = $"https://api.openweathermap.org/data/2.5/weather?q={countryName}&appid=5a4ce2a8ae57aa2b133298afe95c08f9";
                    var json = webClient.DownloadString(url);

                    var result = JsonSerializer.Deserialize<AllWeatherInformation>(json); 
                    //dynamic result = JsonConvert.DeserializeObject(json);
                    while (true)
                    {
                        Console.Write("Какую информацию вы хотите получить?\n" +
                            "1) Координаты\n" +
                            "2) Ветер\n" +
                            "3) Облачность\n" +
                            "4) Погода\n" +
                            "5) Видимость\n" +
                            "6) Основная информация\n" +
                            "7) Дополнительная информация\n" +
                            "8) Выбрать другой город\n" +
                            "9) Выйти\n" +
                            "Выберите: ");
                        int choose = int.Parse(Console.ReadLine());
                        if (choose == 1)
                        {
                            Console.WriteLine($"Координаты в городе {countryName}");
                            Console.WriteLine($"Долгота: {result.coord.lon}");
                            Console.WriteLine($"Ширина: {result.coord.lat}");
                            PushAndClear();
                        }
                        else if (choose == 2)
                        {
                            Console.WriteLine($"Ветер в городе {countryName}");
                            Console.WriteLine($"Скорость ветра: {result.wind.speed} м/с");
                            Console.WriteLine($"Направление ветра: {result.wind.deg} градусов (метеорологические)");
                            PushAndClear();
                        }
                        else if (choose == 3)
                        {
                            Console.WriteLine($"Облачность в городе {countryName}");
                            Console.WriteLine($"Облачность: {result.clouds.all}%");
                            PushAndClear();
                        }
                        else if (choose == 4)
                        {
                            Console.WriteLine($"Погода в городе {countryName}");
                            Console.WriteLine($"Описание погоды: {result.weather[0].description}");
                            Console.WriteLine($"Weather icon id: {result.weather[0].icon}");
                            Console.WriteLine($"Идентификатор погодного состояния: {result.weather[0].id}");
                            Console.WriteLine($"Параметры погоды: {result.weather[0].main}");
                            PushAndClear();
                        }
                        else if (choose == 5)
                        {
                            Console.WriteLine($"Видимость в городе {countryName}");
                            Console.WriteLine($"Дальность видимости: {result.visibility} метров");
                            PushAndClear();
                        }
                        else if (choose == 6)
                        {
                            Console.WriteLine("Основная информация");
                            Console.WriteLine($"Температура: {result.main.temp - 273} градусов цельсия");
                            Console.WriteLine($"Этот температурный параметр учитывает восприятие погоды человеком: {result.main.feels_like - 273} градусов цельсия");
                            Console.WriteLine($"Минимальная температура на данный момент: {result.main.temp_min - 273}");
                            Console.WriteLine($"Максимальная температура на данный момент: {result.main.temp_max - 273}");
                            Console.WriteLine($"Влажность: {result.main.humidity}%");
                            Console.WriteLine($"Атмосферное давление: {result.main.pressure} гПа");
                            PushAndClear();
                        }
                        else if (choose == 7)
                        {
                            int timeZoneResult = result.timezone / 3600;
                            Console.WriteLine($"Дополнительная информация");
                            Console.WriteLine($"Время восхода солнца: {UnixTimeStampToDateTime(result.sys.sunrise)}");
                            Console.WriteLine($"Время заката солнца: {UnixTimeStampToDateTime(result.sys.sunset)}");
                            Console.WriteLine($"Время восхода солнца: {result.sys.sunrise}");
                            Console.WriteLine($"Время заката солнца: {result.sys.sunset}");
                            Console.WriteLine($"Код страны: {result.sys.country}");
                            Console.WriteLine($"День и время: {UnixTimeStampToDateTime(result.dt)}");
                            Console.WriteLine($"Город: {result.name}");
                            Console.WriteLine($"Идентификатор города: {result.id}");
                            if (timeZoneResult >= 0)
                            {
                                Console.WriteLine($"Часовой пояс: +{result.timezone / 3600} UTC");
                            }
                            else
                            {
                                Console.WriteLine($"Часовой пояс: {result.timezone / 3600} UTC");
                            }
                            // Без понятия нужна ли эта информация
                            //Console.WriteLine($"Внутренний параметр: {result.sys.id}");
                            //Console.WriteLine($"Внутренний параметр: {result.sys.type}");
                            //Console.WriteLine($"Внутренний параметр: {result.cod}");
                            PushAndClear();
                        }
                        else if (choose == 8)
                        {
                            break;
                        }
                        else if (choose == 9)
                        {
                            Environment.Exit(0);
                        }
                    }
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