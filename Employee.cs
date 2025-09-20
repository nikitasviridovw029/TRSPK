


namespace CompanyApp
{
    class Employee
    {
        public const string CompanyName = "TechCorp";
        public static readonly DateTime CompanyFoundedDate;
        public readonly int EmployeeId;

        public string Name { get; set; }
        public decimal BaseSalary { get; set; }

        static Employee()
        {
            CompanyFoundedDate = new DateTime(2000, 1, 1);
            Console.WriteLine("Вызван статический конструктор Employee. Дата основания инициализирована.");
        }

        public Employee(int employeeId, string name, decimal baseSalary)
        {
            EmployeeId = employeeId;
            Name = name;
            BaseSalary = baseSalary;

            Console.WriteLine($"Создан сотрудник: {Name}, ID: {EmployeeId}");
        }

        public virtual decimal CalculateSalary()
        {
            return BaseSalary;
        }

        public string GetEmployeeInfo()
        {
            return $"ID: {EmployeeId}, Имя: {Name}, Базовая зарплата: {BaseSalary:C}";
        }
    }

    class Manager : Employee
    {
        public decimal Bonus { get; set; }

        public Manager(int employeeId, string name, decimal baseSalary, decimal bonus)
            : base(employeeId, name, baseSalary)
        {
            Bonus = bonus;
            Console.WriteLine($"Создан менеджер: {Name}, ID: {EmployeeId}, Бонус: {Bonus:C}");
        }

        public override decimal CalculateSalary()
        {
            return BaseSalary + Bonus;
        }
    }

    class Developer : Employee
    {
        public int CompletedProjects { get; set; }
        public const decimal ProjectBonus = 500m;

        public Developer(int employeeId, string name, decimal baseSalary, int completedProjects)
            : base(employeeId, name, baseSalary)
        {
            CompletedProjects = completedProjects;
            Console.WriteLine($"Создан разработчик: {Name}, ID: {EmployeeId}, Проектов завершено: {CompletedProjects}");
        }

        public override decimal CalculateSalary()
        {
            return BaseSalary + CompletedProjects * ProjectBonus;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Компания: {Employee.CompanyName}, Основана: {Employee.CompanyFoundedDate:d}\n");

            Employee[] employees = new Employee[3];

            // Ввод обычного сотрудника
            Console.WriteLine("Введите данные для обычного сотрудника:");
            employees[0] = CreateEmployee();

            // Ввод менеджера
            Console.WriteLine("\nВведите данные для менеджера:");
            employees[1] = CreateManager();

            // Ввод разработчика
            Console.WriteLine("\nВведите данные для разработчика:");
            employees[2] = CreateDeveloper();

            Console.WriteLine("\n--- Информация о сотрудниках ---");
            foreach (var emp in employees)
            {
                Console.WriteLine(emp.GetEmployeeInfo());
                Console.WriteLine($"Рассчитанная зарплата: {emp.CalculateSalary():C}\n");
            }
        }

        static Employee CreateEmployee()
        {
            Console.Write("ID: ");
            string idInput = Console.ReadLine() ?? "";
            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Ошибка: неверный формат ID. Используется значение по умолчанию: 1");
                id = 1;
            }

            Console.Write("Имя: ");
            string name = Console.ReadLine() ?? "Неизвестно";
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ошибка: имя не может быть пустым. Используется значение по умолчанию: 'Сотрудник'");
                name = "Сотрудник";
            }

            Console.Write("Базовая зарплата: ");
            string salaryInput = Console.ReadLine() ?? "";
            if (!decimal.TryParse(salaryInput.Replace(",", "."), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out decimal salary))
            {
                Console.WriteLine("Ошибка: неверный формат зарплаты. Используется значение по умолчанию: 50000");
                salary = 50000;
            }

            return new Employee(id, name, salary);
        }

        static Manager CreateManager()
        {
            Console.Write("ID: ");
            string idInput = Console.ReadLine() ?? "";
            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Ошибка: неверный формат ID. Используется значение по умолчанию: 2");
                id = 2;
            }

            Console.Write("Имя: ");
            string name = Console.ReadLine() ?? "Неизвестно";
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ошибка: имя не может быть пустым. Используется значение по умолчанию: 'Менеджер'");
                name = "Менеджер";
            }

            Console.Write("Базовая зарплата: ");
            string salaryInput = Console.ReadLine() ?? "";
            if (!decimal.TryParse(salaryInput.Replace(",", "."), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out decimal salary))
            {
                Console.WriteLine("Ошибка: неверный формат зарплаты. Используется значение по умолчанию: 70000");
                salary = 70000;
            }

            Console.Write("Бонус: ");
            string bonusInput = Console.ReadLine() ?? "";
            if (!decimal.TryParse(bonusInput.Replace(",", "."), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out decimal bonus))
            {
                Console.WriteLine("Ошибка: неверный формат бонуса. Используется значение по умолчанию: 10000");
                bonus = 10000;
            }

            return new Manager(id, name, salary, bonus);
        }

        static Developer CreateDeveloper()
        {
            Console.Write("ID: ");
            string idInput = Console.ReadLine() ?? "";
            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Ошибка: неверный формат ID. Используется значение по умолчанию: 3");
                id = 3;
            }

            Console.Write("Имя: ");
            string name = Console.ReadLine() ?? "Неизвестно";
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ошибка: имя не может быть пустым. Используется значение по умолчанию: 'Разработчик'");
                name = "Разработчик";
            }

            Console.Write("Базовая зарплата: ");
            string salaryInput = Console.ReadLine() ?? "";
            if (!decimal.TryParse(salaryInput.Replace(",", "."), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out decimal salary))
            {
                Console.WriteLine("Ошибка: неверный формат зарплаты. Используется значение по умолчанию: 80000");
                salary = 80000;
            }

            Console.Write("Количество завершённых проектов: ");
            string projectsInput = Console.ReadLine() ?? "";
            if (!int.TryParse(projectsInput, out int projects))
            {
                Console.WriteLine("Ошибка: неверный формат количества проектов. Используется значение по умолчанию: 5");
                projects = 5;
            }

            return new Developer(id, name, salary, projects);
        }
    }
}
