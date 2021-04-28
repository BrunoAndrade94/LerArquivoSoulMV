using ConsoleApp1.Interfaces;
using ConsoleApp1.Views;
using System;
using System.Globalization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                VMenu menu = new VMenu();
                menu.Executar();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message.ToString(CultureInfo.CurrentUICulture));
            }

        }
    }
}