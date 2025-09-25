using System;

namespace OrdersApp
{
    public enum OrderStatus
    {
        New = 0,     
        Processing = 1,  
        Shipped = 2,    
        Delivered = 3,   
        Cancelled = 4    
    }

    public class Order
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public OrderStatus Status { get; set; }

        public void DisplayOrderInfo()
        {
            Console.WriteLine($"Заказ #{Id}: {ProductName}");

            switch (Status)
            {
                case OrderStatus.New:
                    Console.WriteLine("Статус: Новый");
                    break;
                case OrderStatus.Processing:
                    Console.WriteLine("Статус: В обработке");
                    break;
                case OrderStatus.Shipped:
                    Console.WriteLine("Статус: Отправлен");
                    break;
                case OrderStatus.Delivered:
                    Console.WriteLine("Статус: Доставлен");
                    break;
                case OrderStatus.Cancelled:
                    Console.WriteLine("Статус: Отменен");
                    break;
            }
            Console.WriteLine();
        }

        public bool CanCancelOrder()
        {
            return Status == OrderStatus.New || Status == OrderStatus.Processing;
        }
    }

    class Program
    {
        static void Main()
        {
            int count;
            Console.Write("Сколько заказов хотите создать? ");
            while (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
            {
                Console.WriteLine("Ошибка: введите положительное число!");
                Console.Write("Сколько заказов хотите создать? ");
            }

            Order[] orders = new Order[count];

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\nСоздание заказа #{i + 1}");

        
                int id;
                Console.Write("Введите Id заказа: ");
                while (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
                {
                    Console.WriteLine("Ошибка: Id должен быть положительным числом!");
                    Console.Write("Введите Id заказа: ");
                }

             
                Console.Write("Введите название товара: ");
                string productName = Console.ReadLine();

             
                int statusInput;
                Console.WriteLine("Выберите статус заказа:");
                Console.WriteLine("0 - Новый");
                Console.WriteLine("1 - В обработке");
                Console.WriteLine("2 - Отправлен");
                Console.WriteLine("3 - Доставлен");
                Console.WriteLine("4 - Отменен");

                Console.Write("Ваш выбор: ");
                while (!int.TryParse(Console.ReadLine(), out statusInput) || statusInput < 0 || statusInput > 4)
                {
                    Console.WriteLine("Ошибка: нужно число от 0 до 4.");
                    Console.Write("Ваш выбор: ");
                }

                OrderStatus status = (OrderStatus)statusInput;

                orders[i] = new Order { Id = id, ProductName = productName, Status = status };
            }

            Console.WriteLine("\n=== Информация о заказах ===");
            foreach (var order in orders)
            {
                order.DisplayOrderInfo();
                Console.WriteLine($"Можно отменить заказ? {order.CanCancelOrder()}");
                Console.WriteLine();
            }

            Console.WriteLine("Работа программы завершена.");
        }
    }
}
