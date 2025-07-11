using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank_test_project
{
     public enum TransactionType
    {
        Transfer,
        Deposit,
        Withdrawal,
        Payment
    }

    public class Transaction
    {
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
