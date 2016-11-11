using System;
using Microsoft.Owin.Hosting;

namespace DotNetBay.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var binding = "http://localhost:1901/";

            WebApp.Start<Startup>(binding);

            Console.WriteLine($"Webserver is running on {binding}");
            Console.WriteLine("");

            Console.Write("Press Enter to quit.");
            Console.ReadLine();
        }
    }
}
