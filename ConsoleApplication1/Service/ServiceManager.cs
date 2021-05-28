using System;
using System.Security.Cryptography;
using System.Text;
using ConsoleApplication1.Entity;
using ConsoleApplication1.Helper;
using MySql.Data.MySqlClient;

namespace ConsoleApplication1.Service
{
    public class ServiceManager
    {
        public Customer Receive(string receiver)
        {
            var cnt = new ConnectionHandler();
            MySqlConnection connection = cnt.GetConnection();
            var cmd = new MySqlCommand();
            cmd.Connection = connection;
            try
            {
                cmd.CommandText = $"SELECT * FROM Customer WHERE Username = '{receiver}'";
                var result = cmd.ExecuteReader();
                if (result.HasRows)
                {
                    Customer customer = null;
                    while (result.Read())
                    {
                        var firstname = result["FirstName"].ToString();
                        var lastname = result["LastName"].ToString();
                        var username = result["Username"].ToString();
                        var password1 = result["Password"].ToString();
                        var salt1 = result["Salt"].ToString();
                        var balance = double.Parse(result["Balance"].ToString());
                        var email = result["Email"].ToString();
                        var phone = result["Phone"].ToString();
                        var address = result["Address"].ToString();
                        var status = int.Parse(result["Status"].ToString());
                       customer = new Customer(firstname, lastname, username, password1, salt1, balance,
                            email, phone, address, status);
                    }
                    result.Close();
                    return customer;
                }
                else
                {
                    result.Close();
                    return null;
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public string HashPassword(string password, string salt)
        {
            string pass_string = password + salt;
            StringBuilder affter_pass = new StringBuilder();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytes = md5.ComputeHash(new UTF8Encoding().GetBytes(pass_string));
            for (int i = 0; i < bytes.Length; i++)
            {
                affter_pass.Append(bytes[i].ToString("x2"));
            }

            return affter_pass.ToString();
        }
    }
}