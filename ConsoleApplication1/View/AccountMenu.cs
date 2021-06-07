using System;
using System.Runtime.InteropServices;
using ConsoleApplication1.Controller;
using ConsoleApplication1.Entity;
using Org.BouncyCastle.OpenSsl;

namespace ConsoleApplication1.View
{
    public class AccountMenu
    {
        public void ShowAccountMenu(Customer login)
        {
            var menu = new SpringHeroBankMenu();
            var accountController = new AccountController();
            while (true)
            {
                Console.WriteLine($"Welcome {login.FirstName} {login.LastName}");
                Console.WriteLine(login.AccountNumber);
                Console.WriteLine("-------------------Menu------------------");
                Console.WriteLine("1.Inquiry account balance");
                Console.WriteLine("2. Payment on account");
                Console.WriteLine("3. Withdraw money from your account");
                Console.WriteLine("4. Transfers ");
                Console.WriteLine("5. View transaction history");
                Console.WriteLine("6. Logout");
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Choose from (1-5): ");
                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine(login.ShowFormBalance());
                        break;
                    case 2:
                         login = accountController.Payment(login);
                        break;
                    case 3:
                        login = accountController.Withdrawal(login);
                        break;
                    case 4:
                        login = accountController.Transfer(login);
                        break;
                    case 5:
                        accountController.TransactionHistory(login.AccountNumber);
                        break;
                    case 6:
                        login = null;
                        Console.WriteLine("Logout Success");
                        menu.GetMenuBank();
                        break;
                }


            }
        }
    }
}