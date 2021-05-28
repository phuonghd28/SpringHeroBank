using System;
using ConsoleApplication1.Entity;
using ConsoleApplication1.Model;
using ConsoleApplication1.Service;

namespace ConsoleApplication1.Controller
{
    public class CustomerController
    {
        CustomerModel customerModel = new CustomerModel();
        public void Create()
        {
            var customer = new Customer();
            var serviceManager = new ServiceManager();
            var random = new Random();
            customer.Salt = random.Next(1000, 9000).ToString();
            Console.WriteLine("Please enter firstname");
            customer.FirstName = Console.ReadLine();
            Console.WriteLine("Please enter lastname");
            customer.LastName = Console.ReadLine();
            Console.WriteLine("Please enter username");
            customer.Username = Console.ReadLine();
            Console.WriteLine("Please enter password");
            customer.Password = serviceManager.HashPassword(Console.ReadLine(), customer.Salt);
            Console.WriteLine("Please enter email");
            customer.Email = Console.ReadLine();
            Console.WriteLine("Please enter phone");
            customer.Phone = Console.ReadLine();
            Console.WriteLine("Please enter address");
            customer.Address = Console.ReadLine();
            customerModel.Store(customer);


        }
    }
}