using System;
using ConsoleApplication1.Entity;
using ConsoleApplication1.Model;

namespace ConsoleApplication1.Service
{
    public class EntryService
    {
        private EntryModel _entryModel = new EntryModel();
        private Random _random = new Random();
        private ServiceManager _serviceManager = new ServiceManager();
        public void CreateAccount(Customer customer)
        {
             var accountNumber = _random.Next(1000,9000).ToString() + _random.Next(1000,9000).ToString() + _random.Next(1000,9000).ToString() + _random.Next(1000,9000).ToString();
             var salt = _random.Next(1000, 9000).ToString();
             var passwordHash = _serviceManager.HashPassword(customer.Password, salt);
             var firstName = customer.FirstName;
             var lastName = customer.LastName;
             var username = customer.Username;
             var email = customer.Email;
             var phone = customer.Phone;
             var address = customer.Address;
             var creatAccount = new Customer(accountNumber,firstName,lastName,username,passwordHash,salt,email,phone,address);
             _entryModel.Store(creatAccount);

        }

        public Customer Login(string username, string password)
        {
            return _entryModel.Login(username, password);
        }
    }
}