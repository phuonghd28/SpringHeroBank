using System.Collections.Generic;
using ConsoleApplication1.Entity;
using ConsoleApplication1.Model;

namespace ConsoleApplication1.Service
{
    public class TransactionService
    {
        private AccountModel _accountModel = new AccountModel();
        private TransactionModel _transactionModel = new TransactionModel();
        
        public Customer Payment(Customer customer, double amount)
        {
            return _accountModel.Payment(customer.Username, amount);
        }

        public Customer Withdrawal(Customer customer, double amount)
        {
            return _accountModel.Withdrawal(customer.Username, amount);
        }

        public Customer Transfer(string sender, string receiver, double amount, string content)
        {
            return _accountModel.Transfer(sender, receiver, amount, content);
        }
        public List<Transaction> Transactions(string accountNumber)
        {
            return _transactionModel.ListTransaction(accountNumber);
        }

        public List<Transaction> FilterTransaction(string accountNumber, string startTime , string endTime)
        {
            return _transactionModel.FilterTransaction(accountNumber, startTime, endTime);
        }
    }
}