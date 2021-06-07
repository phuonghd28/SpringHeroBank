using System;
using System.Collections.Generic;
using ConsoleApplication1.Entity;
using ConsoleApplication1.Model;
using ConsoleApplication1.Service;

namespace ConsoleApplication1.Controller
{
    public class AccountController
    {
        private ServiceManager _service = new ServiceManager();
        private TransactionService _transactionService = new TransactionService();
        public Customer Payment(Customer customer)
        {
            Console.WriteLine("Enter the amount you want to deposit:");
            double amount = Double.Parse(Console.ReadLine());
            Console.WriteLine("Press 1 to complete the transaction");
            Console.WriteLine("Press 0 to cancel the transaction");
            int choice = int.Parse(Console.ReadLine());
            if (choice == 1)
            {
                return _transactionService.Payment(customer,amount);
            }
            else
            {
                return customer;
            }
        }

        public Customer Withdrawal(Customer customer)
        {
            Console.WriteLine("Enter the amount you want to withdraw");
            double amount = Double.Parse(Console.ReadLine());
            if (amount > customer.Balance)
            {
                Console.WriteLine("Your account does not have enough funds to make this transaction");
                return customer;
            }
            else
            {
                Console.WriteLine("Press 1 to complete the transaction");
                Console.WriteLine("Press 0 to cancel the transaction");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    return _transactionService.Withdrawal(customer,amount);
                }
                else
                {
                    return customer;
                }
            }
        }

        public Customer Transfer(Customer customer)
        {
            Console.WriteLine("Enter receiver account");
            string receiver = Console.ReadLine();
            Customer check = _service.Receive(receiver);
            if (check == null)
            {
                Console.WriteLine($"Account {receiver} not found");
                return customer;
            }
            else if (customer.Username.Equals(check.Username))
            {
                Console.WriteLine("Unable to transfer money to yourself");
                return customer;
            }
            else
            {
                Console.WriteLine("----------Receiver Information-----------");
                Console.WriteLine(
                    $"Name : {check.FirstName} {check.LastName}\nEmail : {check.Email}\nPhone : {check.Phone}\nAddress : {check.Address}");
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("1. Continue");
                Console.WriteLine("2. Cancel");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    Console.WriteLine($"Enter the money you want to transfer to {check.Username}");
                    double amount = double.Parse(Console.ReadLine());
                    if (amount > customer.Balance)
                    {
                        Console.WriteLine("Your account does not have enough funds to make a transaction");
                        return customer;
                    }
                    else
                    {
                        Console.WriteLine("Please enter content");
                        string content = Console.ReadLine();
                        Customer customer1 = _transactionService.Transfer(customer.Username, check.Username, amount, content);
                        if (customer1 != null)
                        {
                            return customer1;
                        }
                        else
                        {
                            return customer;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("The transaction is cancelled");
                    return customer;
                }
            }
        }

        public void TransactionHistory(string accountNumber)
        {
            Console.WriteLine("1. Show Transaction History");
            Console.WriteLine("2. Check transaction history by day");
            var choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    var transactions = _transactionService.Transactions(accountNumber);
                    if (transactions.Count == 0)
                    {
                        Console.WriteLine("You are not have any transaction");
                    }
                    else
                    {
                        Console.WriteLine("-------------------Transaction History-----------------------");
                        foreach (var trans in transactions)
                        {
                            Console.WriteLine(trans.ToString());
                        }

                        Console.WriteLine("-------------------------------------------------------------");
                    }

                    break;
                    case 2:

                    Console.WriteLine("Enter start time");
                    var startTime = Console.ReadLine();
                    Console.WriteLine("Enter end time");
                    var endTime = Console.ReadLine();
                    var listTransaction = _transactionService.FilterTransaction(accountNumber, startTime, endTime);
                    if (listTransaction.Count == 0)
                    {
                        Console.WriteLine("You are not have any transaction");
                    }
                    else
                    {
                        Console.WriteLine("-------------------Transaction History-----------------------");
                        foreach (var tsc in listTransaction)
                        {
                            Console.WriteLine(tsc.ToString());
                        }

                        Console.WriteLine("-------------------------------------------------------------");
                    }
                    break;
            }


        }
    }
}