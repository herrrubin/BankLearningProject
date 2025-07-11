using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank_test_project
{
    public class User : Person
    {
        public decimal Balance { get;  set; }
        public string AccountNumber { get; private set; }
        public List<Transaction> Transactions { get; private set; } = new();

        public User()
        {
            AccountNumber = GenerateAccountNumber();
        }

        private string GenerateAccountNumber()
        {
            Random rand = new();
            return $"ACC-{rand.Next(1000, 9999)}-{rand.Next(1000, 9999)}";
        }

        public void AddTransaction(TransactionType type, decimal amount, string description)
        {
            Transactions.Add(new Transaction
            {
                Date = DateTime.Now,
                Type = type,
                Amount = amount,
                Description = description
            });
        }

        public void DecreaseBalance(decimal amount)
        {
            Balance -= amount;
        }
        
        

    }
}