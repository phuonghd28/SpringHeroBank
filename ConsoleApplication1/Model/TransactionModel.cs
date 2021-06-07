using System;
using System.Collections.Generic;
using ConsoleApplication1.Entity;
using ConsoleApplication1.Helper;
using ConsoleApplication1.Service;
using MySql.Data.MySqlClient;

namespace ConsoleApplication1.Model
{
    public class TransactionModel
    {
        private ConnectionHandler _connection = new ConnectionHandler();
        private ServiceManager _serviceManager = new ServiceManager();

        public void SavePayment(Customer customer, double amount)
        {
            MySqlConnection connection = _connection.GetConnection();
            var cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "INSERT INTO Transaction (Id,AccountNumber,Amount,Message,Type,Status)" +
                              "values(@Id,@AccountNumber,@Amount,@Message,@Type,@Status)";
            cmd.Parameters.AddWithValue("@Id", _serviceManager.RandomString(6));
            cmd.Parameters.AddWithValue("@AccountNumber", customer.AccountNumber);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@Message", $"has paid {amount}$ to the account");
            cmd.Parameters.AddWithValue("@Type", "Payment");
            cmd.Parameters.AddWithValue("@Status", 1);
            cmd.ExecuteNonQuery();
        }

        public void SaveWithdrawal(Customer customer, double amount)
        {
            MySqlConnection connection = _connection.GetConnection();
            var cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "INSERT INTO Transaction (Id,AccountNumber,Amount,Message,Type,Status)" +
                              "values(@Id,@AccountNumber,@Amount,@Message,@Type,@Status)";
            cmd.Parameters.AddWithValue("@Id", _serviceManager.RandomString(6));
            cmd.Parameters.AddWithValue("@AccountNumber", customer.AccountNumber);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@Message", $"have withdrawn {amount}$ from the account");
            cmd.Parameters.AddWithValue("@Type", "Withdrawal");
            cmd.Parameters.AddWithValue("@Status", 1);
            cmd.ExecuteNonQuery();
        }

        public void SaveTransfer(Customer customer,string sender, string receiver, double amount, string content ,int status)
        {
            MySqlConnection connection = _connection.GetConnection();
            var cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "INSERT INTO Transaction (Id,AccountNumber,Sender,Receiver,Amount,Message,Content,Type,Status)" + "values(@Id,@AccountNumber,@Sender,@Receiver,@Amount,@Message,@Content,@Type,@Status)";
            cmd.Parameters.AddWithValue("@Id", _serviceManager.RandomString(6));
            cmd.Parameters.AddWithValue("@AccountNumber",customer.AccountNumber);
            cmd.Parameters.AddWithValue("@Sender",sender);
            cmd.Parameters.AddWithValue("@Receiver",receiver);
            cmd.Parameters.AddWithValue("@Amount",amount);
            cmd.Parameters.AddWithValue("@Message", $"transferred {amount}$ to {receiver} account");
            cmd.Parameters.AddWithValue("@Content", content);
            cmd.Parameters.AddWithValue("@Type", "Transfer");
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.ExecuteNonQuery();
        }

        public List<Transaction> ListTransaction(string accountNumber)
        {
            var transactions = new List<Transaction>();
            MySqlConnection connection = _connection.GetConnection();
            var cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM Transaction WHERE AccountNumber = '{accountNumber}'";
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

                    var time = DateTime.Parse(result["CreateAt"].ToString());
                    var name = result["AccountNumber"].ToString();
                    var message = result["Message"].ToString();
                    var type = result["Type"].ToString();
                    var transaction = new Transaction(statusEnum, message, type, time, name);
                    transactions.Add(transaction);
                }
            }

            result.Close();
            return transactions;
        }

        public List<Transaction> FilterTransaction(string accountNumber, string startTime , string endTime)
        {
            var transactions1 = new List<Transaction>();
            MySqlConnection connection = _connection.GetConnection();
            var cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM Transaction WHERE AccountNumber = '{accountNumber}' AND CreateAt BETWEEN '{startTime}' and '{endTime}'";
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

                    var time = DateTime.Parse(result["CreateAt"].ToString());
                    var name = result["AccountNumber"].ToString();
                    var message = result["Message"].ToString();
                    var type = result["Type"].ToString();
                    var transaction1 = new Transaction(statusEnum, message, type, time, name);
                    transactions1.Add(transaction1);
                }
            }
            result.Close();
            return transactions1;
        }
    }
}