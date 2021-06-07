using System;
using System.Runtime.InteropServices;
using System.Text;
using ConsoleApplication1.Controller;
using ConsoleApplication1.Entity;
using ConsoleApplication1.Helper;
using ConsoleApplication1.Model;
using ConsoleApplication1.View;
using MySql.Data.MySqlClient;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            /*TransactionModel transactionModel = new TransactionModel();*/
            var menu = new SpringHeroBankMenu();
            menu.GetMenuBank();
            /*var accountNumber = "4960418374783042";
            var startTime = "2021-06-05";
            var endTime = "2021-06-10";
            var list = transactionModel.FilterTransaction(accountNumber,startTime, endTime);
            for (int i = 0; i < list.Count; i++)
            {
                var listTran = list[i];
                Console.WriteLine(listTran.ToString());
            }*/


        }
    }
}