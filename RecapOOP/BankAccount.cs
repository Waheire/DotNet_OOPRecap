using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace RecapOOP
{
    internal class BankAccount
    {
        private static int s_accountNumberSeed = 1234567890;
        private List<Transaction> _allTransactions = new List<Transaction>();

        public BankAccount(string name, decimal initialBalance)
        {
            Number = s_accountNumberSeed;
            s_accountNumberSeed++;

            Owner = name;
            MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
        }

        public int Number { get; set; }
        public string Owner { get; set; }
        public decimal Balance 
        {
            get
            {
                decimal balance = 0;
                foreach (var t in _allTransactions) 
                {
                    balance += t.Amount;
                }
                return balance;
            }
        }

        public void MakeDeposit(decimal amount, DateTime date, string note) 
        {
            if (amount < 0) 
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }
            var deposit = new Transaction(amount, date, note);
            _allTransactions.Add(deposit);
        }

        public void  MakeWithdrawal(decimal amount, DateTime date, string note) 
        {
            if(amount < 0) 
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
             }
            if (Balance - amount < 0) 
            {
                throw new InvalidOperationException("Not Sufficient funds for this withdrawal");
            }
            var withdrawal = new Transaction(amount, date, note); _allTransactions.Add(withdrawal);
            _allTransactions.Add(withdrawal);
        }

        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            decimal balance = 0;
            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            foreach (var item in _allTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
            }

            return report.ToString();
        }
    }
}
