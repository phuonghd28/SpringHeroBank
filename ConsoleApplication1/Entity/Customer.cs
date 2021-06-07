using System;
using Google.Protobuf.WellKnownTypes;

namespace ConsoleApplication1.Entity
{
    public class Customer
    {
        public int Id { get; set; }
        
        public string AccountNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public double Balance { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime DeleteAt { get; set; }

        public Customer()
        {
            
        }

        public Customer(string accountNumber, string firstName, string lastName, string username, string password, string salt, string email, string phone, string address)
        {
            AccountNumber = accountNumber;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            Salt = salt;
            Email = email;
            Phone = phone;
            Address = address;
        }

        public Customer(string accountNumber,string firstName, string lastName, string username, string password, string salt, double balance, string email, string phone, string address, int status)
        {
            AccountNumber = accountNumber;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            Salt = salt;
            Balance = balance;
            Email = email;
            Phone = phone;
            Address = address;
            Status = status;
        }

        public Customer(int id, string accountNumber, string firstName, string lastName, string username, string password, string salt, double balance, string email, string phone, string address, int status, DateTime createAt, DateTime updateAt, DateTime deleteAt)
        {
            Id = id;
            AccountNumber = accountNumber;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            Salt = salt;
            Balance = balance;
            Email = email;
            Phone = phone;
            Address = address;
            Status = status;
            CreateAt = createAt;
            UpdateAt = updateAt;
            DeleteAt = deleteAt;
        }

        public override string ToString()
        {
            return $"Id={Id},AccountNumber={AccountNumber},FirstName={FirstName},LastName={LastName},UserName={Username},Password={Password},Salt={Salt},Email={Email},Phone={Phone},Address={Address},Status={Status},CreateAt={CreateAt},UpdateAt={UpdateAt},DeleteAt={DeleteAt}";
        }

        public string ShowFormBalance()
        {
            return $"Hello {FirstName} {LastName}\nYour current balance : {Balance}$";
        }
    }
}