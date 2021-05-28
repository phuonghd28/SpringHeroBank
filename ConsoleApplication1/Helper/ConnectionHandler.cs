using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ConsoleApplication1.Helper
{
    public class ConnectionHandler
    {
        private const string Server = "localhost";
        private const string DbName = "spring_hero_bank";
        private const string Username = "root";
        private const string Password = "";
        private const string Port = "3306";

        private static readonly string Connection =
            $"Server={Server};Database={DbName};Port={Port};UID={Username};Password={Password}";

        public static MySqlConnection _Connection = null;

        public MySqlConnection GetConnection()
        {
            if (_Connection == null)
            {
                _Connection = new MySqlConnection(Connection);
                _Connection.Open();
                return _Connection;
            }
            else
            {
                return _Connection;
            }
            
        }
    }
}