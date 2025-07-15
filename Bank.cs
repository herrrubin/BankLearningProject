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
                Console.WriteLine("ТИП ОПЕРАЦИИ | СУММА | ДЕТАЛИ");
                Console.WriteLine("------------------------------");

                foreach (var transaction in user.Transactions.OrderByDescending(t => t.Date))
                {
                    string typeColor = transaction.Type switch
                    {
                        TransactionType.Transfer => "\x1B[93m", // желтый
                        TransactionType.Deposit => "\x1B[92m",  // зеленый
                        TransactionType.Withdrawal => "\x1B[91m",// красный
                        TransactionType.Payment => "\x1B[94m",  // синий
                        _ => ""
                    };

                    Console.WriteLine($"{typeColor}{transaction.Type,-12}\x1B[0m | " +
                                     $"{transaction.Amount,8:C} | " +
                                     $"{transaction.Description} " +
                                     $"\x1B[90m({transaction.Date:dd.MM.yyyy HH:mm})\x1B[0m");
                }
            }
            Console.WriteLine("\nТИПЫ ОПЕРАЦИЙ:");
            Console.WriteLine("\x1B[93mTransfer\x1B[0m - Перевод средств");
            Console.WriteLine("\x1B[92mDeposit\x1B[0m  - Пополнение счета");
            Console.WriteLine("\x1B[91mWithdrawal\x1B[0m - Снятие средств");
            Console.WriteLine("\x1B[94mPayment\x1B[0m  - Платежи и бонусы");

            Console.WriteLine("\nНажмите любую клавишу для возврата...");
            Console.ReadKey();
        }

        public static void MakeTransfer(User user)
        {
            Console.Clear();
            Console.WriteLine("=== Перевод средств ===\n");
            bool exitTransfer = false;
            while (!exitTransfer)
            {
                Console.WriteLine("Выберите способ перевода:");
                Console.WriteLine("\x1B[93m1\x1B[0m - Новый перевод");
                // Console.WriteLine("\x1B[93m2\x1B[0m - Список контактов");            - пока что на стадии идеи
                Console.WriteLine("\x1B[93m3\x1B[0m - Последние переводы");
                // Console.WriteLine("\x1B[93m4\x1B[0m - Запланированные переводы");        - пока что на стадии идеи
                Console.WriteLine("\x1B[91mQ\x1B[0m - Возврат в главное меню\n");

                var choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (choice)
                {
                    case '1':
                        ProcessNewTransfer(user);
                        break;

                    // case '2':
                    //     DisplayContacts(user);           - пока что на стадии идеи
                    //     break;

                    case '3':
                        DisplayRecentTransfers(user);        
                        break;

                    // case '4':
                    //     ManageScheduledTransfers(user);      - пока что на стадии идеи
                    //     break;

                    case 'Q':
                    case 'q':
                        exitTransfer = true;
                        break;

                    default:
                        Console.WriteLine("\x1B[91mНеверный выбор. Пожалуйста, выберите один из предложенных вариантов.\x1B[0m");
                        Thread.Sleep(1500);
                        break;
                }

                if (!exitTransfer)
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private static void ProcessNewTransfer(User user)
        {
            Console.WriteLine("\n--- Новый перевод ---");
            Console.Write("Введите номер счета получателя: ");
            string recipientAccount = Console.ReadLine() ?? string.Empty;

            Console.Write("Введите сумму перевода: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("\x1B[91mОшибка: Неверный формат суммы.\x1B[0m");
                return;
            }

            Console.Write("Введите описание перевода: ");
            string description = Console.ReadLine() ?? "Перевод";

            if (amount <= 0)
            {
                Console.WriteLine("\x1B[91mОшибка: Сумма должна быть положительной.\x1B[0m");
                return;
            }

            if (user.Balance < amount)
            {
                Console.WriteLine("\x1B[91mОшибка: Недостаточно средств на счете.\x1B[0m");
                return;
            }

            Console.Clear();
            Console.WriteLine("=== Подтверждение перевода ===\n");
            Console.WriteLine($"\x1B[93mПолучатель:\x1B[0m {recipientAccount}");
            Console.WriteLine($"\x1B[93mСумма:\x1B[0m {amount:C}");
            Console.WriteLine($"\x1B[93mОписание:\x1B[0m {description}");
            Console.WriteLine("\nПодтвердите перевод (Y/N): ");

            var confirm = Console.ReadKey().KeyChar;
            if (confirm == 'Y' || confirm == 'y')
            {
                user.Balance -= amount;
                user.AddTransaction(TransactionType.Transfer, amount, $"Перевод на счет {recipientAccount} - {description}");
                Console.WriteLine("\n\x1B[92m✓ Перевод успешно выполнен!\x1B[0m");
            }
            else
            {
                Console.WriteLine("\n\x1B[91m× Перевод отменен.\x1B[0m");
            }
        }
        private static void DisplayRecentTransfers(User user)
        {
            Console.WriteLine("\n--- Последние переводы ---");
            var recentTransfers = user.Transactions
                .Where(t => t.Type == TransactionType.Transfer)
                .OrderByDescending(t => t.Date)
                .Take(5);

            if (!recentTransfers.Any())
            {
                Console.WriteLine("Нет недавних переводов");
                return;
            }

            foreach (var transaction in recentTransfers)
            {
                Console.WriteLine($"{transaction.Date:HH:mm dd.MM} | {transaction.Amount:C} | {transaction.Description}");
            }
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