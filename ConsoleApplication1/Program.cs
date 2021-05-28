using System;
using System.Runtime.InteropServices;
using System.Text;
using ConsoleApplication1.Controller;
using ConsoleApplication1.Entity;
using ConsoleApplication1.Helper;
using ConsoleApplication1.View;
using MySql.Data.MySqlClient;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var menu = new SpringHeroBankMenu();
            menu.GetMenuBank();


        }
    }
}