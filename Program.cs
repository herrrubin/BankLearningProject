using bank_test_project;
using System.IO;
using System.Threading;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        var user = RegisterUser();
            
            if (!AuthenticateUser(user))
            {
                Console.WriteLine("\nОшибка аутентификации. Завершение работы.");
                return;
            }

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== Главное меню ===");
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - История операций");
                Console.WriteLine("2 - Перевод средств");
                Console.WriteLine("3 - Информация о профиле");
                Console.WriteLine("Q - Выход");

                var input = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (input)
                {
                    case '1':
                        Bank.ShowTransferHistory(user);
                        break;
                    case '2':
                        Bank.MakeTransfer(user);
                        break;
                    case '3':
                        Bank.ShowProfileInfo(user);
                        break;
                    case 'Q':
                    case 'q':
                        exit = true;
                        Console.WriteLine("Завершение работы...");
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        Thread.Sleep(1500);
                        break;
                }
            }
        }

        private static User RegisterUser()
    {
        var user = new User();

        Console.WriteLine("=== Регистрация ===\n");
        Console.Write("Введите имя: ");
        user.Name = Console.ReadLine() ?? string.Empty;
        Console.Write("Введите фамилию: ");
        user.SecondName = Console.ReadLine() ?? string.Empty;

        Console.Write("Введите отчество: ");
        user.LastName = Console.ReadLine() ?? string.Empty;
        Console.Write("Создайте пароль (минимум 6 символов): ");
        string password = Console.ReadLine() ?? string.Empty;

        if (password.Length < 6)
        {
            Console.WriteLine("Пароль слишком короткий. Установлен стандартный пароль '123456'");
            password = "123456";
        }


        user.Pass = password;
        user.Balance = 2000; // приветственный бонус, хз как по-другому это реализовать
        user.AddTransaction(TransactionType.Payment, 2000, "Приветственный бонус при регистрации");

        Console.WriteLine($"\nРегистрация завершена, {user.Name}!");
        Console.WriteLine($"Ваш номер счета: {user.AccountNumber}");
        Console.WriteLine("На Ваш счет зачислено 2000₽ в качестве приветственного бонуса");
        Thread.Sleep(2500);
   
    return user;
}

        private static bool AuthenticateUser(User user)
        {
            Console.WriteLine("\n=== Аутентификация ===");
            for (int i = 0; i < 3; i++)
            {
                Console.Write("Введите пароль: ");
                string input = Console.ReadLine() ?? string.Empty;
                
                if (input == user.Pass)
                {
                    return true;
                }
                
                Console.WriteLine($"Неверный пароль. Осталось попыток: {2 - i}");
            }
            
            return false;
    }
}