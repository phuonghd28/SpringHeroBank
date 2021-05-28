using System;
using ConsoleApplication1.Entity;
using ConsoleApplication1.Helper;
using MySql.Data.MySqlClient;

namespace ConsoleApplication1.Model
{
    public class CustomerModel
    {
        public void Store(Customer customer)
        {
            try
            {
                ConnectionHandler cnh = new ConnectionHandler();
                MySqlConnection mySqlConnection = cnh.GetConnection();
                MySqlCommand cmd = mySqlConnection.CreateCommand();
                cmd.CommandText = "Insert into Customer(FirstName,LastName,Username,Password,Email,Phone,Address,Salt)" +
                                  "values(@FirstName,@LastName,@Username,@Password,@Email,@Phone,@Address,@Salt)";
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@Username", customer.Username);
                cmd.Parameters.AddWithValue("@Password", customer.Password);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                cmd.Parameters.AddWithValue("@Salt", customer.Salt);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Insert success");
            }   
            catch (MySqlException e)
            {
                Console.WriteLine(e);
            }
          
        }
    }
}