using System;
using ConsoleApplication1.Entity;
using ConsoleApplication1.Helper;
using ConsoleApplication1.Service;
using MySql.Data.MySqlClient;

namespace ConsoleApplication1.Model
{
    public class EntryModel
    {
        private bool isLogin = false;
        private Customer _customer = null;
        private ServiceManager _serviceManager = new ServiceManager();
        private ConnectionHandler _connection = new ConnectionHandler();
        
        public void Store(Customer customer)
        {
            try
            {
               
                MySqlConnection mySqlConnection = _connection.GetConnection();
                MySqlCommand cmd = mySqlConnection.CreateCommand();
                cmd.CommandText = "Insert into Customer(AccountNumber,FirstName,LastName,Username,Password,Email,Phone,Address,Salt)" +
                                  "values(@AccountNumber,@FirstName,@LastName,@Username,@Password,@Email,@Phone,@Address,@Salt)";
                cmd.Parameters.AddWithValue("@AccountNumber", customer.AccountNumber);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@Username", customer.Username);
                cmd.Parameters.AddWithValue("@Password", customer.Password);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                cmd.Parameters.AddWithValue("@Salt", customer.Salt);
                cmd.ExecuteNonQuery();
            }   
            catch (MySqlException e)
            {
                Console.WriteLine(e);
            }
          
        }
           public Customer Login(string account, string password)
        {
            MySqlConnection connection = _connection.GetConnection();
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
                                var accountNumber = customerData["AccountNumber"].ToString();
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
                                _customer = new Customer(accountNumber,firstname, lastname, username, password1, salt1, balance,
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

    }
}