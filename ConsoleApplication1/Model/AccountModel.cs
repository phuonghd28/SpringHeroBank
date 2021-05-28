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
        private Customer _customer = null;
        private Boolean isLogin = false;

        public Customer Login(string account, string password)
        {
            MySqlConnection connection = connectionHandler.GetConnection();
            var cmd = new MySqlCommand();
            cmd.Connection = connection;
            try
            {
                cmd.CommandText = $"SELECT * FROM Customer WHERE Username = '{account}'";
                var result = cmd.ExecuteReader();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        var salt = result["Salt"].ToString();
                        var hasPass = _serviceManager.HashPassword(password, salt);
                        Console.WriteLine();
                        if (hasPass.Equals(result["Password"].ToString()))
                        {
                            result.Close();
                            cmd.CommandText = $"SELECT * FROM Customer WHERE Password = '{hasPass}'";
                            MySqlDataReader customerData = cmd.ExecuteReader();
                            while (customerData.Read())
                            {
                                var firstname = customerData["FirstName"].ToString();
                                var lastname = customerData["LastName"].ToString();
                                var username = customerData["Username"].ToString();
                                var password1 = customerData["Password"].ToString();
                                var salt1 = customerData["Salt"].ToString();
                                var balance = double.Parse(customerData["Balance"].ToString());
                                var email = customerData["Email"].ToString();
                                var phone = customerData["Phone"].ToString();
                                var address = customerData["Address"].ToString();
                                var status = int.Parse(customerData["Status"].ToString());
                                _customer = new Customer(firstname, lastname, username, password1, salt1, balance,
                                    email, phone, address, status);
                                _customer.Balance = balance;
                                customerData.Close();
                                Console.WriteLine("Login Success");
                                return _customer;
                            }

                        }
                        else
                        {
                            isLogin = false;
                        }
                    }

                    if (!isLogin)
                    {
                        Console.WriteLine("Login Failed");
                    }
                    result.Close();
                }

            }
            catch (MySqlException e)
            {
                Console.WriteLine(e);
                throw;
            }

            return null;
        }

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
                    _customer = new Customer(firstname, lastname, username1, password, salt, balance, email, phone,
                        address, status);
                    _customer.Balance = balance;
                    Console.WriteLine("Payment Success");
                }
                result1.Close();
                cmd.CommandText = "INSERT INTO Transaction (AccountName,Content,Type,Status)" + "values(@AccountName,@Content,@Type,@Status)";
                cmd.Parameters.AddWithValue("@AccountName", username);
                cmd.Parameters.AddWithValue("@Content", $"has paid {money}$ to the account") ;
                cmd.Parameters.AddWithValue("@Type","Payment");
                cmd.Parameters.AddWithValue("@Status", 1);
                cmd.ExecuteNonQuery();
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
                    _customer = new Customer(firstname, lastname, username1, password, salt, balance, email, phone,
                        address, status);
                    _customer.Balance = balance;
                    Console.WriteLine("Withdrawal Success");
                }
                result1.Close();
                cmd.CommandText = "INSERT INTO Transaction (AccountName,Content,Type,Status)" + "values(@AccountName,@Content,@Type,@Status)";
                cmd.Parameters.AddWithValue("@AccountName", username);
                cmd.Parameters.AddWithValue("@Content",$"have withdrawn {money}$ from the account");
                cmd.Parameters.AddWithValue("@Type", "Withdrawal");
                cmd.Parameters.AddWithValue("@Status", 1);
                cmd.ExecuteNonQuery();
                return _customer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            

        }

        public Customer Transfer(string sender, string receiver, double money)
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
                        _customer = new Customer(firstname, lastname, username, password, salt, balance, email, phone,
                            address, status);
                        _customer.Balance = balance;
                    }
                    result2.Close();
                    cmd.CommandText = "INSERT INTO Transaction (AccountName,Content,Type,Status)" + "values(@AccountName,@Content,@Type,@Status)";
                    cmd.Parameters.AddWithValue("@AccountName",sender);
                    cmd.Parameters.AddWithValue("@Content", $"transferred {money}$ to {receiver} account");
                    cmd.Parameters.AddWithValue("@Type", "Transfer");
                    cmd.Parameters.AddWithValue("@Status", 1);
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Transaction (AccountName,Content,Type,Status)" + "values(@AccountName1,@Content1,@Type1,@Status1)";
                    cmd.Parameters.AddWithValue("@AccountName1",receiver);
                    cmd.Parameters.AddWithValue("@Content1",$"received {money}$ from the account {sender}");
                    cmd.Parameters.AddWithValue("@Type1", "Transfer");
                    cmd.Parameters.AddWithValue("@Status1", 1);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Transfer Success");
                }
                else
                {
                    cmd.CommandText = "INSERT INTO Transaction (AccountName,Content,Type,Status)" + "values(@AccountName,@Content,@Type,@Status)";
                    cmd.Parameters.AddWithValue("@AccountName",sender);
                    cmd.Parameters.AddWithValue("@Content", $"transferred {money}$ to {receiver} account");
                    cmd.Parameters.AddWithValue("@Type", "Transfer");
                    cmd.Parameters.AddWithValue("@Status", 0);
                    cmd.ExecuteNonQuery();
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

        public List<Transaction> ListTransaction(string username)
        {
            var transactions = new List<Transaction>();
            MySqlConnection connection = connectionHandler.GetConnection();
            var cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM Transaction WHERE AccountName = '{username}'";
            var result = cmd.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    var status = int.Parse(result["Status"].ToString());
                    string statusEnum;
                    if (status == 1)
                    {
                        statusEnum = "Complete";
                    }
                    else
                    {
                        statusEnum = "UnComplete";
                    }

                    var time = DateTime.Parse(result["CreatAt"].ToString());
                    var name = result["AccountName"].ToString();
                    var content = result["Content"].ToString();
                    var type = result["Type"].ToString();
                    var transaction = new Transaction(statusEnum, content, type, time, name);
                    transactions.Add(transaction);
                }
            }
            result.Close();
            return transactions;
        }
    }
}