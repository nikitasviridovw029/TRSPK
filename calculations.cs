class Program
{
    static void Main()
    {
        System.Console.Write("Введите символ a: ");
        string? input = System.Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            System.Console.WriteLine("Ошибка: пустой ввод.");
            return;
        }

        char a = input[0];

        // Вариант 1: через if
        int f_if;
        if (a >= '0' && a <= '9')
        {
            f_if = 100;
        }
        else if (a >= 'A' && a <= 'Z')
        {
            f_if = 200;
        }
        else if (a >= 'a' && a <= 'z')
        {
            f_if = 300;
        }
        else
        {
            f_if = 400;
        }

        // Вариант 2: через switch
        int f_switch;
        switch (a)
        {
            case char ch when ch >= '0' && ch <= '9':
                f_switch = 100;
                break;
            case char ch when ch >= 'A' && ch <= 'Z':
                f_switch = 200;
                break;
            case char ch when ch >= 'a' && ch <= 'z':
                f_switch = 300;
                break;
            default:
                f_switch = 400;
                break;
        }

        System.Console.WriteLine($"a = '{a}'");
        System.Console.WriteLine($"f (if) = {f_if}");
        System.Console.WriteLine($"f (switch) = {f_switch}");
    }
}
