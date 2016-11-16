using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBay.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:1901/";
            WebApp.Start<Startup>(url: url);

            Console.WriteLine($"Webserver is up and running on {url}");

            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
        }
    }
}
