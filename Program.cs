namespace TriangleAreaCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.Write("Введите a: ");
            if (!double.TryParse(System.Console.ReadLine(), out double a) || a <= 0)
            {
                System.Console.WriteLine("Ошибка: введите положительное вещественное число для a.");
                return;
            }

            System.Console.Write("Введите b: ");
            if (!double.TryParse(System.Console.ReadLine(), out double b) || b <= 0)
            {
                System.Console.WriteLine("Ошибка: введите положительное вещественное число для b.");
                return;
            }

            System.Console.Write("Введите c: ");
            if (!double.TryParse(System.Console.ReadLine(), out double c) || c <= 0)
            {
                System.Console.WriteLine("Ошибка: введите положительное вещественное число для c.");
                return;
            }

            if (a + b <= c || a + c <= b || b + c <= a)
            {
                System.Console.WriteLine("Ошибка: треугольник с такими сторонами не существует (проверьте неравенство треугольника).");
                return;
            }

            double p = (a + b + c) / 2; // полупериметр
            double s = System.Math.Sqrt(p * (p - a) * (p - b) * (p - c));

            System.Console.WriteLine($"Площадь треугольника: {s:F2}");
        }
    }
}