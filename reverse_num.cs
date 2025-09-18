class Program_2
{
    static void Main()
    {
        try
        {
            Console.Write("Введите трехзначное число: ");
            int number = int.Parse(Console.ReadLine());

            if (number < 100 || number > 999)
            {
                Console.WriteLine("Вы ввели не трехзначное число.");
                return;
            }

            int reversed = 0, temp = number;
            while (temp > 0)
            {
                reversed = reversed * 10 + temp % 10;
                temp /= 10;
            }

            Console.WriteLine("Перевернутое число: {reversed}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Вы ввели не число.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
}
