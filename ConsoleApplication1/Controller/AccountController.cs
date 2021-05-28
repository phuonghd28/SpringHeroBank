using System;
using System.Collections.Generic;
using ConsoleApplication1.Entity;
using ConsoleApplication1.Model;
using ConsoleApplication1.Service;

namespace ConsoleApplication1.Controller
{
    public class AccountController
    {
        private AccountModel _accountModel = new AccountModel();
        private Customer IsLogin = null;
        private ServiceManager _service = new ServiceManager();
        public Customer Login()
        {
            Console.WriteLine("Enter your account");
            var account = Console.ReadLine();
            Console.WriteLine("Enter your password");
            var password = Console.ReadLine();
            IsLogin = _accountModel.Login(account, password);
            return IsLogin;
        }

        public Customer Payment(Customer customer)
        {
            Console.WriteLine("Enter the amount you want to deposit:");
            double money = Double.Parse(Console.ReadLine());
            Console.WriteLine("Press 1 to complete the transaction");
            Console.WriteLine("Press 0 to cancel the transaction");
            int choice = int.Parse(Console.ReadLine());
            if (choice == 1)
            {
                return _accountModel.Payment(customer.Username, money);
            }
            else
            {
                return customer;
            }

        }

        public Customer Withdrawal(Customer customer)
        {
            Console.WriteLine($"Your account currently has {customer.Balance}$");
            Console.WriteLine("Enter the amount you want to withdraw");
            double money = Double.Parse(Console.ReadLine());
            if (money > customer.Balance)
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
                    return _accountModel.Withdrawal(customer.Username, money);
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
            }else if (customer.Username.Equals(check.Username))
            {
                Console.WriteLine("Unable to transfer money to yourself");
                return customer;
            }
            else
            {
                Console.WriteLine("----------Receiver Information-----------");
                Console.WriteLine($"Name : {check.FirstName} {check.LastName}\nEmail : {check.Email}\nPhone : {check.Phone}\nAddress : {check.Address}");
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("1. Continue");
                Console.WriteLine("2. Cancel");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    Console.WriteLine($"Enter the money you want to transfer to {check.Username}");
                    double money = double.Parse(Console.ReadLine());
                    if (money > customer.Balance)
                    {
                        Console.WriteLine("Your account does not have enough funds to make a transaction");
                        return customer;
                    }
                    else
                    {
                        Customer customer1 = _accountModel.Transfer(customer.Username, check.Username, money);
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

        public void TransactionHistory(string username)
        {
            List<Transaction> transactions = _accountModel.ListTransaction(username);
            if (transactions.Count == 0)
            {
                Console.WriteLine("You are not have any transaction");
            }
            else
            {
                Console.WriteLine("-------------------Transaction History-----------------------");
                foreach (Transaction tsc in transactions)
                {
                    Console.WriteLine(tsc.ToString());
                }

                Console.WriteLine("-------------------------------------------------------------");
            }
        }
        
        
    }
}