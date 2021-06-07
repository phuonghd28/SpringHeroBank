using System;
using System.Collections.Generic;
using System.Globalization;
using ConsoleApplication1.Entity;
using ConsoleApplication1.Helper;
using ConsoleApplication1.Service;
using MySql.Data.MySqlClient;

namespace ConsoleApplication1.Model
{
    public class AccountModel
    {
        ConnectionHandler connectionHandler = new ConnectionHandler();
        private ServiceManager _serviceManager = new ServiceManager();
        private TransactionModel _transactionModel = new TransactionModel();
        private Customer _customer = null;

        public Customer Payment(string username, double money)
        {
            double newBalance = 0;
            MySqlConnection connection = connectionHandler.GetConnection();
            var cmd = new MySqlCommand();
            cmd.Connection = connection;
            try
            {
                cmd.CommandText = $"SELECT * FROM Customer WHERE Username = '{username}'";
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    newBalance = money + Double.Parse(result["Balance"].ToString());
                }
                result.Close();
                cmd.CommandText = $"UPDATE Customer SET Balance = {newBalance} WHERE Username = '{username}'";
                cmd.ExecuteNonQuery();
                cmd.CommandText = $"SELECT * FROM Customer WHERE Username = '{username}'";
                MySqlDataReader result1 = cmd.ExecuteReader();
                while (result1.Read())
                {
                    var accountNumber = result1["AccountNumber"].ToString();
                    var firstname = result1["FirstName"].ToString();
                    var lastname = result1["LastName"].ToString();
                    var username1 = result1["Username"].ToString();
                    var password = result1["Password"].ToString();
                    var salt = result1["Salt"].ToString();
                    var balance = double.Parse(result1["Balance"].ToString());
                    var email = result1["Email"].ToString();
                    var phone = result1["Phone"].ToString();
                    var address = result1["Address"].ToString();
                    var status = int.Parse(result1["Status"].ToString());
                    _customer = new Customer(accountNumber,firstname, lastname, username1, password, salt, balance, email, phone,
                        address, status);
                    _customer.Balance = balance;
                    Console.WriteLine("Payment Success");
                }
                result1.Close();
                _transactionModel.SavePayment(_customer,money);
                return _customer;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Customer Withdrawal(string username, double money)
        {
            double newBalance = 0;
            MySqlConnection connection = connectionHandler.GetConnection();
            var cmd = new MySqlCommand();
            cmd.Connection = connection;
            try
            {
                cmd.CommandText = $"SELECT * FROM Customer WHERE Username = '{username}'";
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    newBalance = Double.Parse(result["Balance"].ToString()) - money;
                }
                result.Close();
                cmd.CommandText = $"UPDATE Customer SET Balance = {newBalance} WHERE Username = '{username}'";
                cmd.ExecuteNonQuery();
                cmd.CommandText = $"SELECT * FROM Customer WHERE Username = '{username}'";
                var result1 = cmd.ExecuteReader();
                while (result1.Read())
                {
                    var accountNumber = result1["AccountNumber"].ToString();
                    var firstname = result1["FirstName"].ToString();
                    var lastname = result1["LastName"].ToString();
                    var username1 = result1["Username"].ToString();
                    var password = result1["Password"].ToString();
                    var salt = result1["Salt"].ToString();
                    var balance = double.Parse(result1["Balance"].ToString());
                    var email = result1["Email"].ToString();
                    var phone = result1["Phone"].ToString();
                    var address = result1["Address"].ToString();
                    var status = int.Parse(result1["Status"].ToString());
                    _customer = new Customer(accountNumber,firstname, lastname, username1, password, salt, balance, email, phone,
                        address, status);
                    _customer.Balance = balance;
                    Console.WriteLine("Withdrawal Success");
                }

                result1.Close();
                _transactionModel.SaveWithdrawal(_customer,money);
                cmd.ExecuteNonQuery();
                return _customer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            

        }

        public Customer Transfer(string sender, string receiver, double money, string content)
        {
            double newBalanceSender = 0;
            double newBalanceReceiver = 0;
            MySqlConnection connection = connectionHandler.GetConnection();
            var cmd = new MySqlCommand();
            cmd.Connection = connection;
            MySqlTransaction transaction;
            transaction = connection.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                cmd.CommandText = $"SELECT * FROM Customer WHERE Username = '{sender}'";
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    newBalanceSender = double.Parse(result["Balance"].ToString()) - money;
                }
                result.Close();
                cmd.CommandText = $"SELECT * FROM Customer WHERE Username = '{receiver}'";
                var result1 = cmd.ExecuteReader();
                while (result1.Read())
                {
                    newBalanceReceiver = double.Parse(result1["Balance"].ToString()) + money;
                }
                result1.Close();
                Console.WriteLine($"Transfer confirmation to {receiver}\n 1. Confirm\n 2. Cancel");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    cmd.CommandText = $"UPDATE Customer SET Balance = {newBalanceSender} WHERE Username = '{sender}'";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = $"UPDATE Customer SET Balance = {newBalanceReceiver} WHERE Username = '{receiver}'";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = $"SELECT * FROM Customer WHERE Username = '{sender}'";
                    var result2 = cmd.ExecuteReader();
                    while (result2.Read())
                    {
                        var accountNumber = result2["AccountNumber"].ToString();
                        var firstname = result2["FirstName"].ToString();
                        var lastname = result2["LastName"].ToString();
                        var username = result2["Username"].ToString();
                        var password = result2["Password"].ToString();
                        var salt = result2["Salt"].ToString();
                        var balance = double.Parse(result2["Balance"].ToString());
                        var email = result2["Email"].ToString();
                        var phone = result2["Phone"].ToString();
                        var address = result2["Address"].ToString();
                        var status = int.Parse(result2["Status"].ToString());
                        _customer = new Customer(accountNumber,firstname, lastname, username, password, salt, balance, email, phone,
                            address, status);
                        _customer.Balance = balance;
                    }
                    result2.Close();
                    _transactionModel.SaveTransfer(_customer,sender,receiver,money,content,1);
                    Console.WriteLine("Transfer Success");
                }
                else
                {
                    _transactionModel.SaveTransfer(_customer,sender,receiver,money,content,0);
                    Console.WriteLine("Canceled transaction");
                    _customer = null;
                }
                transaction.Commit();
                return _customer;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}