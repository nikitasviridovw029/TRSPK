using System;

namespace PermissionsApp
{
    [Flags]
    public enum UserPermissions
    {
        None = 0,
        Read = 1,
        Write = 2,
        Delete = 4,
        Execute = 8,

        ReadWrite = Read | Write,
        ReadWriteDelete = Read | Write | Delete
    }

    public class User
    {
        public string Name { get; set; }
        public UserPermissions Permissions { get; set; }

        public bool HasPermission(UserPermissions permission)
        {
            return (Permissions & permission) == permission;
        }

        public void AddPermission(UserPermissions permission)
        {
            Permissions |= permission;
        }

        public void RemovePermission(UserPermissions permission)
        {
            Permissions &= ~permission;
        }

        // Вывод всех активных прав по отдельности
        public void DisplayInfo()
        {
            if (Permissions == UserPermissions.None)
            {
                Console.WriteLine($"Пользователь: {Name}, Права: Нет");
                return;
            }

            var activePermissions = Enum.GetValues(typeof(UserPermissions));
            string result = "";

            foreach (UserPermissions perm in activePermissions)
            {
                if (perm == UserPermissions.None) continue; // пропускаем None
                if (Permissions.HasFlag(perm) && IsSingleFlag(perm))
                {
                    result += perm + ", ";
                }
            }

            if (result.EndsWith(", "))
                result = result.Substring(0, result.Length - 2);

            Console.WriteLine($"Пользователь: {Name}, Права: {result}");
        }

        // Проверка, что флаг одиночный (не комбинированный)
        private bool IsSingleFlag(UserPermissions permission)
        {
            return permission == UserPermissions.Read ||
                   permission == UserPermissions.Write ||
                   permission == UserPermissions.Delete ||
                   permission == UserPermissions.Execute;
        }
    }

    class Program
    {
        static void Main()
        {
            Console.Write("Сколько пользователей хотите создать? ");
            int count;
            while (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
            {
                Console.WriteLine("Ошибка! Введите положительное число.");
                Console.Write("Сколько пользователей хотите создать? ");
            }

            User[] users = new User[count];

            // --- Ввод пользователей ---
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\nСоздание пользователя #{i + 1}");

                Console.Write("Введите имя пользователя: ");
                string name = Console.ReadLine();

                Console.WriteLine("Выберите права (можно указать несколько через запятую):");
                Console.WriteLine("1 - Read");
                Console.WriteLine("2 - Write");
                Console.WriteLine("4 - Delete");
                Console.WriteLine("8 - Execute");
                Console.WriteLine("Пример: 1,2 (даст Read + Write)");
                Console.Write("Ваш выбор: ");

                string input = Console.ReadLine();
                string[] parts = input.Split(',');
                UserPermissions permissions = UserPermissions.None;

                foreach (string part in parts)
                {
                    if (int.TryParse(part.Trim(), out int permValue))
                    {
                        permissions |= (UserPermissions)permValue;
                    }
                }

                users[i] = new User { Name = name, Permissions = permissions };
            }

            // --- Меню управления ---
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=== Меню ===");
                Console.WriteLine("1 - Показать всех пользователей");
                Console.WriteLine("2 - Добавить право пользователю");
                Console.WriteLine("3 - Удалить право у пользователя");
                Console.WriteLine("4 - Проверить право пользователя");
                Console.WriteLine("0 - Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\n=== Список пользователей ===");
                        foreach (var user in users)
                            user.DisplayInfo();
                        break;

                    case "2":
                        ModifyPermission(users, true);
                        break;

                    case "3":
                        ModifyPermission(users, false);
                        break;

                    case "4":
                        CheckPermission(users);
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Ошибка! Выберите правильный пункт меню.");
                        break;
                }
            }

            Console.WriteLine("Работа программы завершена.");
        }

        // Метод для добавления/удаления прав
        static void ModifyPermission(User[] users, bool add)
        {
            Console.Write("Введите имя пользователя: ");
            string name = Console.ReadLine();
            User user = Array.Find(users, u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (user == null)
            {
                Console.WriteLine("Пользователь не найден.");
                return;
            }

            Console.WriteLine("Выберите право(а) (можно несколько через запятую):");
            Console.WriteLine("1 - Read");
            Console.WriteLine("2 - Write");
            Console.WriteLine("4 - Delete");
            Console.WriteLine("8 - Execute");
            Console.Write("Ваш выбор: ");

            string input = Console.ReadLine();
            string[] parts = input.Split(',');

            foreach (string part in parts)
            {
                if (int.TryParse(part.Trim(), out int permValue))
                {
                    if (add)
                        user.AddPermission((UserPermissions)permValue);
                    else
                        user.RemovePermission((UserPermissions)permValue);
                }
            }

            Console.WriteLine(add
                ? $"Права обновлены для {user.Name} (добавлены)"
                : $"Права обновлены для {user.Name} (удалены)");
        }

        // Метод для проверки права
        static void CheckPermission(User[] users)
        {
            Console.Write("Введите имя пользователя: ");
            string name = Console.ReadLine();
            User user = Array.Find(users, u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (user == null)
            {
                Console.WriteLine("Пользователь не найден.");
                return;
            }

            Console.WriteLine("Выберите право для проверки:");
            Console.WriteLine("1 - Read");
            Console.WriteLine("2 - Write");
            Console.WriteLine("4 - Delete");
            Console.WriteLine("8 - Execute");
            Console.Write("Ваш выбор: ");

            if (int.TryParse(Console.ReadLine(), out int permValue))
            {
                UserPermissions perm = (UserPermissions)permValue;
                Console.WriteLine(user.HasPermission(perm)
                    ? $"У {user.Name} есть право {perm}"
                    : $"У {user.Name} нет права {perm}");
            }
            else
            {
                Console.WriteLine("Ошибка ввода!");
            }
        }
    }
}
