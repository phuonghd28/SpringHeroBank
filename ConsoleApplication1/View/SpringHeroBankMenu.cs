using System;
using ConsoleApplication1.Controller;
using ConsoleApplication1.Entity;

namespace ConsoleApplication1.View
{
    public class SpringHeroBankMenu
    {
        public Customer IsLogin = null;
        public void GetMenuBank()
        {
            while (true)
            {
                var customerController = new CustomerController();
                var accountController = new AccountController();
                var accountMenu = new AccountMenu();
                var aboutUs = new AboutUs();
                var contactUs = new ContactUs();
                Console.WriteLine("Welcome to Spring Hero Bank");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("1. About Us");
                Console.WriteLine("2. Register Account");
                Console.WriteLine("3. Login");
                Console.WriteLine("4. Contact Us");
                Console.WriteLine("5. Exit");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Please enter your choice (1 - 5): ");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        aboutUs.GetView();
                        break;
                    case 2:
                        customerController.Create();
                        break;
                    case 3:
                        IsLogin = accountController.Login();
                        accountMenu.GetAccountMenu(IsLogin);
                        break;
                    case 4:
                        contactUs.GetView();
                        break;
                }
                if (choice == 5)
                {
                    break;
                }
            }
        }
    }
}