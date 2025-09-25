using System;

namespace TextCommandParserApp
{
    public enum TextCommand
    {
        Unknown = 0,
        Start = 1,
        Stop = 2,
        Pause = 3,
        Resume = 4
    }

    public class CommandParser
    {
        public static TextCommand ParseCommand(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return TextCommand.Unknown;

            input = input.Trim();
            if (int.TryParse(input, out int numValue) && Enum.IsDefined(typeof(TextCommand), numValue))
            {
                return (TextCommand)numValue;
            }
            string cleanedInput = "";
            foreach (char c in input)
            {
                if (char.IsLetter(c))
                    cleanedInput += c;
            }
            if (Enum.TryParse<TextCommand>(cleanedInput, true, out TextCommand command))
                return command;

            return TextCommand.Unknown;
        }
        public static void ExecuteCommand(TextCommand command)
        {
            switch (command)
            {
                case TextCommand.Start:
                    Console.WriteLine("Выполняется команда: Start");
                    break;
                case TextCommand.Stop:
                    Console.WriteLine("Выполняется команда: Stop");
                    break;
                case TextCommand.Pause:
                    Console.WriteLine("Выполняется команда: Pause");
                    break;
                case TextCommand.Resume:
                    Console.WriteLine("Выполняется команда: Resume");
                    break;
                default:
                    Console.WriteLine("Неизвестная команда!");
                    break;
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Доступные команды:");
            Console.WriteLine("0 - Unknown");
            Console.WriteLine("1 - Start");
            Console.WriteLine("2 - Stop");
            Console.WriteLine("3 - Pause");
            Console.WriteLine("4 - Resume");

            while (true)
            {
                Console.Write("\nВведите команду: ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    break;

                TextCommand command = CommandParser.ParseCommand(input);
                CommandParser.ExecuteCommand(command);
            }

            Console.WriteLine("Программа завершена.");
        }
    }
}
