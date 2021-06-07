using System;
using ConsoleApplication1.Entity;
using ConsoleApplication1.Model;
using ConsoleApplication1.Service;

namespace ConsoleApplication1.Controller
{
    public class EntryController
    {
        private Customer isLogin = null;
        private Customer _customer = new Customer();
        private EntryService _service = new EntryService();
        public void Create()
        {
            Console.WriteLine("Please enter firstname");
            _customer.FirstName = Console.ReadLine();
            Console.WriteLine("Please enter lastname");
            _customer.LastName = Console.ReadLine();
            Console.WriteLine("Please enter username");
            _customer.Username = Console.ReadLine();
            Console.WriteLine("Please enter password");
            _customer.Password = Console.ReadLine();
            Console.WriteLine("Please enter email");
            _customer.Email = Console.ReadLine();
            Console.WriteLine("Please enter phone");
            _customer.Phone = Console.ReadLine();
            Console.WriteLine("Please enter address");
            _customer.Address = Console.ReadLine();
            _service.CreateAccount(_customer);
        }
        public Customer Login()
        {
            Console.WriteLine("Enter your account");
            var account = Console.ReadLine();
            Console.WriteLine("Enter your password");
            var password = Console.ReadLine();
            isLogin = _service.Login(account, password);
            return isLogin;
        }
    }
}