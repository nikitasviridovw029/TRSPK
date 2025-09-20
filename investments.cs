using System;
using System.Collections.Generic;
using System.Linq;

static class MathExtensions
{
    // --- Метод расширения для массива int ---
    public static double StandardDeviation(this int[] numbers)
    {
        if (numbers == null || numbers.Length == 0) return 0;

        double mean = numbers.Average();
        double sumSquares = numbers.Sum(x => Math.Pow(x - mean, 2));
        return Math.Sqrt(sumSquares / numbers.Length);
    }

    // --- Метод с in и out ---
    public static bool TryCalculateCircle(in double radius, out double area, out double circumference)
    {
        if (radius < 0)
        {
            area = 0;
            circumference = 0;
            return false;
        }

        area = Math.PI * radius * radius;
        circumference = 2 * Math.PI * radius;
        return true;
    }

    // --- Метод с ref ---
    public static void SwapValues(ref int a, ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }

    // --- Метод с params и необязательным параметром ---
    public static string FormatNumbers(string separator = ", ", params double[] numbers)
    {
        return string.Join(separator, numbers.Select(n => n.ToString("F2")));
    }

    // --- Метод с именованными параметрами ---
    // Формула сложного процента: A = P * (1 + r/n)^(n*t)
    public static double CalculateInvestment(
        double principal,
        double rate,
        int years,
        bool compoundMonthly = true)
    {
        int n = compoundMonthly ? 12 : 1;
        return principal * Math.Pow(1 + rate / n, n * years);
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Программа демонстрации методов C# ===\n");

        // --- 1. Вызов метода расширения StandardDeviation ---
        Console.WriteLine("1. Расчет стандартного отклонения");
        Console.Write("Введите числа через пробел: ");
        string input = Console.ReadLine() ?? "";
        int[] data = ParseIntArray(input);
        if (data.Length > 0)
        {
            Console.WriteLine($"Стандартное отклонение: {data.StandardDeviation():F2}\n");
        }
        else
        {
            Console.WriteLine("Неверный ввод. Используются значения по умолчанию: 10, 20, 30, 40, 50");
            data = new int[] { 10, 20, 30, 40, 50 };
            Console.WriteLine($"Стандартное отклонение: {data.StandardDeviation():F2}\n");
        }

        // --- 2. Использование TryCalculateCircle ---
        Console.WriteLine("2. Расчет площади и длины окружности");
        Console.Write("Введите радиус круга: ");
        string radiusInput = Console.ReadLine() ?? "";
        if (double.TryParse(radiusInput, out double radius))
        {
            if (MathExtensions.TryCalculateCircle(in radius, out double area, out double circumference))
            {
                Console.WriteLine($"Площадь круга: {area:F2}, Длина окружности: {circumference:F2}\n");
            }
            else
            {
                Console.WriteLine("Ошибка: радиус не может быть отрицательным\n");
            }
        }
        else
        {
            Console.WriteLine("Неверный ввод. Используется радиус по умолчанию: 5");
            radius = 5;
            MathExtensions.TryCalculateCircle(in radius, out double area, out double circumference);
            Console.WriteLine($"Площадь круга: {area:F2}, Длина окружности: {circumference:F2}\n");
        }

        // --- 3. Обмен значений через SwapValues ---
        Console.WriteLine("3. Обмен значений");
        Console.Write("Введите первое число: ");
        string xInput = Console.ReadLine() ?? "";
        Console.Write("Введите второе число: ");
        string yInput = Console.ReadLine() ?? "";

        if (int.TryParse(xInput, out int x) && int.TryParse(yInput, out int y))
        {
            Console.WriteLine($"До Swap: x={x}, y={y}");
            MathExtensions.SwapValues(ref x, ref y);
            Console.WriteLine($"После Swap: x={x}, y={y}\n");
        }
        else
        {
            Console.WriteLine("Неверный ввод. Используются значения по умолчанию: x=10, y=20");
            x = 10; y = 20;
            Console.WriteLine($"До Swap: x={x}, y={y}");
            MathExtensions.SwapValues(ref x, ref y);
            Console.WriteLine($"После Swap: x={x}, y={y}\n");
        }

        // --- 4. Форматирование чисел через FormatNumbers ---
        Console.WriteLine("4. Форматирование чисел");
        Console.Write("Введите числа через пробел: ");
        string numbersInput = Console.ReadLine() ?? "";
        double[] numbers = ParseDoubleArray(numbersInput);
        if (numbers.Length > 0)
        {
            string formatted = MathExtensions.FormatNumbers("; ", numbers);
            Console.WriteLine($"Форматированные числа: {formatted}\n");
        }
        else
        {
            Console.WriteLine("Неверный ввод. Используются значения по умолчанию: 1.2345, 5.6789, 9.1011");
            numbers = new double[] { 1.2345, 5.6789, 9.1011 };
            string formatted = MathExtensions.FormatNumbers("; ", numbers);
            Console.WriteLine($"Форматированные числа: {formatted}\n");
        }

        // --- 5. Использование CalculateInvestment с именованными параметрами ---
        Console.WriteLine("5. Расчет инвестиций");
        Console.Write("Введите начальную сумму: ");
        string principalInput = Console.ReadLine() ?? "";
        Console.Write("Введите годовую процентную ставку (например, 0.05 или 0,05 для 5%): ");
        string rateInput = Console.ReadLine() ?? "";
        Console.Write("Введите количество лет: ");
        string yearsInput = Console.ReadLine() ?? "";
        Console.Write("Капитализация ежемесячная? (y/n): ");
        string compoundInput = Console.ReadLine() ?? "";

        // Проверяем каждое поле отдельно с поддержкой разных форматов
        bool principalValid = double.TryParse(principalInput.Replace(",", "."), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double principal);
        bool rateValid = double.TryParse(rateInput.Replace(",", "."), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double rate);
        bool yearsValid = int.TryParse(yearsInput, out int years);

        if (principalValid && rateValid && yearsValid)
        {
            // Улучшенная логика определения типа капитализации
            bool compoundMonthly = compoundInput.ToLower().Trim().StartsWith("y") || 
                                  compoundInput.ToLower().Trim().StartsWith("д") ||
                                  compoundInput.ToLower().Trim().StartsWith("yes") ||
                                  compoundInput.ToLower().Trim().StartsWith("да") ||
                                  compoundInput.ToLower().Trim() == "1";
            
            Console.WriteLine($"\nВведенные данные:");
            Console.WriteLine($"- Начальная сумма: {principal:C}");
            Console.WriteLine($"- Процентная ставка: {rate:P}");
            Console.WriteLine($"- Количество лет: {years}");
            Console.WriteLine($"- Тип капитализации: {(compoundMonthly ? "ежемесячная" : "годовая")}");
            Console.WriteLine($"- Ответ на вопрос о капитализации: '{compoundInput}'");
            
            double investment1 = MathExtensions.CalculateInvestment(principal: principal, rate: rate, years: years, compoundMonthly: compoundMonthly);
            double investment2 = MathExtensions.CalculateInvestment(principal: principal, rate: rate, years: years, compoundMonthly: !compoundMonthly);

            string type1 = compoundMonthly ? "ежемесячная" : "годовая";
            string type2 = compoundMonthly ? "годовая" : "ежемесячная";
            
            Console.WriteLine($"\nРезультаты:");
            Console.WriteLine($"Инвестиции ({type1} капитализация): {investment1:C}");
            Console.WriteLine($"Инвестиции ({type2} капитализация): {investment2:C}\n");
        }
        else
        {
            Console.WriteLine("\n❌ Ошибки ввода:");
            if (!principalValid) Console.WriteLine($"- Неверный формат начальной суммы: '{principalInput}' (ожидается число)");
            if (!rateValid) Console.WriteLine($"- Неверный формат процентной ставки: '{rateInput}' (ожидается число, например: 0.05 или 0,05 для 5%)");
            if (!yearsValid) Console.WriteLine($"- Неверный формат количества лет: '{yearsInput}' (ожидается целое число)");
            
            Console.WriteLine("\nИспользуются значения по умолчанию: сумма=1000, ставка=0.05, годы=10");
            double investment1 = MathExtensions.CalculateInvestment(principal: 1000, rate: 0.05, years: 10);
            double investment2 = MathExtensions.CalculateInvestment(principal: 1000, rate: 0.05, years: 10, compoundMonthly: false);
            Console.WriteLine($"Инвестиции (ежемесячная капитализация): {investment1:F2}");
            Console.WriteLine($"Инвестиции (годовая капитализация): {investment2:F2}\n");
        }

        Console.WriteLine("Программа завершена. Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    // Вспомогательный метод для парсинга массива целых чисел
    private static int[] ParseIntArray(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return new int[0];

        string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        List<int> numbers = new List<int>();

        foreach (string part in parts)
        {
            if (int.TryParse(part, out int number))
            {
                numbers.Add(number);
            }
        }

        return numbers.ToArray();
    }

    // Вспомогательный метод для парсинга массива чисел с плавающей точкой
    private static double[] ParseDoubleArray(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return new double[0];

        string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        List<double> numbers = new List<double>();

        foreach (string part in parts)
        {
            if (double.TryParse(part, out double number))
            {
                numbers.Add(number);
            }
        }

        return numbers.ToArray();
    }
}
