using System;
using System.Collections.Generic;
using System.Linq;
using Planes;
using airCompany;
using CustomExceptions;
using FileManager;
using System.Text;
using LAB4_OOP;
using System.Data;
using System.Diagnostics;

// Використані патерни:
// 1. Factory Method — для створення літаків різних типів (Boeing, Mriya).
// 2. Decorator — для додавання можливостей до літака (WiFi).
// 3. Strategy — для сортування і фільтрації літаків за різними критеріями.

class Program
{
    
    static HashSet<Plane> planes = new();
    static AirCompany company = new AirCompany();
    static PlaneContext context = new PlaneContext();

    static void Main()
    {
        Console.OutputEncoding = UTF8Encoding.UTF8;
        double a_ = 1, b_ = 9;
        var func = new Function();
        var calc = new IntegralCalculator();

        // два набори n: малий і великий
        var tests = new (string Name, long n)[]
        {
            ("Малий", 1000),      // < 10^3
            ("Великий", 1000000) // > 10^6
        };

        Console.WriteLine("Threads | Test   |   Result   | Time (ms)");
        Console.WriteLine("--------+--------+------------+-----------");

        foreach (var (Name, n) in tests)
        {
            for (int threads = 1; threads <= 20; threads++)
            {
                var sw = Stopwatch.StartNew();
                double result = calc.Calculate(a_, b_, n, func, threads);
                sw.Stop();

                Console.WriteLine(
                    $"{threads,7} | {Name,-6} | {result,10:F6} | {sw.ElapsedMilliseconds,9}"
                );
            }
        }

        Console.WriteLine("\nАналітичне значення інтегралу = 52");
        Console.ReadLine();

        while (true)
        {
            Console.WriteLine("\n1. Завантажити літаки з файлу");
            Console.WriteLine("2. Зберегти літаки у файл");
            Console.WriteLine("3. Порахувати загальну місткість і вантажопідйомність");
            Console.WriteLine("4. Відсортувати літаки за дальністю польоту");
            Console.WriteLine("5. Знайти літаки за споживанням пального");
            Console.WriteLine("6. Вийти");
            Console.WriteLine("7. Додати новий літак");
            Console.WriteLine("8. Вивести всі літаки");
            Console.Write("Вибір: ");
            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        planes = PlaneSerializer.Load();
                        Console.WriteLine("Завантажено.");
                        break;

                    case "2":
                        PlaneSerializer.Save(planes);
                        Console.WriteLine("Збережено.");
                        break;

                    case "3":
                        if (!planes.Any()) throw new EmptyPlaneSetException();
                        company.CountCapAndLoad(planes.ToArray());
                        break;

                    case "4":
                        if (!planes.Any()) throw new EmptyPlaneSetException();
                        context.SetStrategy(new SortByRangeStrategy());      // 3. Strategy — для сортування і фільтрації літаків за різними критеріями.
                        context.ExecuteStrategy(planes.ToArray());
                        break;

                    case "5":
                        if (!planes.Any()) throw new EmptyPlaneSetException();
                        Console.Write("Введіть межу A: ");
                        int a = int.Parse(Console.ReadLine());
                        Console.Write("Введіть межу B: ");
                        int b = int.Parse(Console.ReadLine());
                        if (a >= b) throw new InvalidFuelRangeException();
                        context.SetStrategy(new FilterByFuelStrategy(a, b)); // 3. Strategy — для сортування і фільтрації літаків за різними критеріями.
                        context.ExecuteStrategy(planes.ToArray());
                        break;

                    case "6":
                        return;

                    case "7":
                        Console.WriteLine("Оберіть тип літака (1 - Boeing, 2 - Mriya, 3 - Boeing757, 4 - AirbusA330): ");
                        string type = Console.ReadLine();
                        PlaneFactory factory = type switch               // 1. Factory Method — для створення літаків різних типів (Boeing, Mriya).
                        {
                            "1" => new BoeingFactory(),
                            "2" => new MriyaFactory(),
                            "3" => new Boeing757Factory(),
                            "4" => new AirbusA330Factory(),
                            _ => throw new Exception("Невідомий тип.")
                        };

                        Console.Write("Введіть дальність польоту: ");
                        int range = int.Parse(Console.ReadLine());
                        Console.Write("Введіть місткість: ");
                        int cap = int.Parse(Console.ReadLine());
                        Console.Write("Введіть вантажопідйомність: ");
                        int load = int.Parse(Console.ReadLine());
                        Console.Write("Введіть споживання пального: ");
                        int fuel = int.Parse(Console.ReadLine());

                        Plane newPlane = factory.CreatePlane(range, cap, load, fuel);

                        // Декоратор
                        Console.Write("Додати WiFi? (так/ні): ");
                        string wifi = Console.ReadLine();
                        if (wifi.Trim().ToLower() == "так")       // 2. Decorator — для додавання можливостей до літака (WiFi).
                        {
                            newPlane = new WifiDecorator(newPlane);
                            ((WifiDecorator)newPlane).EnableWifi();
                        }

                        if (planes.Add(newPlane))
                            Console.WriteLine("Літак успішно додано.");
                        else
                            Console.WriteLine("Такий літак уже існує у множині.");
                        break;

                    case "8":
                        if (!planes.Any())
                        {
                            Console.WriteLine("Список літаків порожній.");
                        }
                        else
                        {
                            foreach (var plane in planes)
                            {
                                Console.WriteLine($"Літак: Дальність: {plane.FlightRange}, Місткість: {plane.Capacity}, Вантажопідйомність: {plane.LoadCapacity}, Пальне: {plane.FuelConsuption}");
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Помилка]: {ex.Message}");
            }
        }
    }
}
