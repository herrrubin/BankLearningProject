using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank_test_project
{
    public class Bank
    {
        public static void ShowTransferHistory(User user)
        {
            Console.Clear();
            Console.WriteLine("=== История операций ===\n");
            
            if (user.Transactions.Count == 0)
            {
                Console.WriteLine("История операций пуста.");
            }
            else
            {
                foreach (var transaction in user.Transactions.OrderByDescending(t => t.Date))
                {
                    Console.WriteLine($"{transaction.Date:dd.MM.yyyy HH:mm} | {transaction.Type} | {transaction.Amount:C} | {transaction.Description}");
                }
            }
            
            Console.WriteLine("\nНажмите любую клавишу для возврата...");
            Console.ReadKey();
        }

        public static void MakeTransfer(User user)
        {
            Console.Clear();
            Console.WriteLine("=== Перевод средств ===\n");
            
            Console.Write("Введите номер счета получателя: ");
            string recipientAccount = Console.ReadLine() ?? string.Empty;
            
            Console.Write("Введите сумму перевода: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Ошибка: Неверный формат суммы.");
                return;
            }
            
            Console.Write("Введите описание перевода: ");
            string description = Console.ReadLine() ?? "Перевод";

            if (amount <= 0)
            {
                Console.WriteLine("Ошибка: Сумма должна быть положительной.");
                return;
            }
            
            if (user.Balance < amount)
            {
                Console.WriteLine("Ошибка: Недостаточно средств на счете.");
                return;
            }

            user.DecreaseBalance(amount);
            user.AddTransaction(TransactionType.Transfer, amount, $"Перевод на счет {recipientAccount} - {description}");
            
            Console.WriteLine($"\nУспешно: Переведено {amount:C} на счет {recipientAccount}");
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }


        public static void ShowProfileInfo(User user)
        {
            Console.Clear();
            Console.WriteLine("=== Информация о профиле ===\n");
            Console.WriteLine($"ФИО: {user.Name} {user.SecondName} {user.LastName}");
            Console.WriteLine($"Номер счета: {user.AccountNumber}");
            Console.WriteLine($"Текущий баланс: {user.Balance:C}");
            Console.WriteLine("\nНажмите любую клавишу для возврата...");
            Console.ReadKey();
        }
    }
}