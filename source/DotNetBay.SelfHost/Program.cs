using System;
using System.Data.Entity.SqlServer;
using Microsoft.Owin.Hosting;
using System.Reflection;
using System.Net.Http;
using DotNetBay.Core.Execution;
using DotNetBay.Data.EF;
using DotNetBay.Core;

namespace DotNetBay.SelfHost
{
    class Program
    {
        // ROLA - This is a hack to ensure that Entity Framework SQL Provider is copied across to the output folder.
        // As it is installed in the GAC, Copy Local does not work. It is required for probing.
        // Fixed "Provider not loaded" error
        SqlProviderServices ensureDLLIsCopied = SqlProviderServices.Instance;

        static void Main(string[] args)
        {
            

            var binding = "http://localhost:1901/";

            using (WebApp.Start<Startup>(binding)) {
                // debug stuff...
                WebApi.AuctionController.AuctionRunner.Auctioneer.AuctionEnded += Auctioneer_AuctionEnded;
                WebApi.AuctionController.AuctionRunner.Auctioneer.AuctionStarted += Auctioneer_AuctionStarted;
                WebApi.AuctionController.AuctionRunner.Auctioneer.BidAccepted += Auctioneer_BidAccepted;
                WebApi.AuctionController.AuctionRunner.Auctioneer.BidDeclined += Auctioneer_BidDeclined;
                // end debug stuff

                Console.WriteLine($"Webserver is running on {binding}");
                Console.WriteLine("");


                // Create HttpClient and make a request to api/values 
                HttpClient client = new HttpClient();
                var response = client.GetAsync(binding + "api/auctions").Result;
                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                Console.WriteLine();
                Console.Write("Press Enter to quit.");
                Console.ReadLine();
                WebApi.AuctionController.AuctionRunner.Stop();
           }

        }

        private static void Auctioneer_BidDeclined(object sender, ProcessedBidEventArgs e)
        {
            Console.WriteLine($"Declined Bid: {e.Bid.Id} on Auction {e.Auction.Id}");
        }

        private static void Auctioneer_BidAccepted(object sender, ProcessedBidEventArgs e)
        {
            Console.WriteLine($"Accepted Bid: {e.Bid.Id} on Auction {e.Auction.Id}");
        }

        private static void Auctioneer_AuctionStarted(object sender, AuctionEventArgs e)
        {
            Console.WriteLine($"Auction started: {e.Auction.Id}, Success: {e.IsSuccessful}");
        }

        private static void Auctioneer_AuctionEnded(object sender, AuctionEventArgs e)
        {
            Console.WriteLine($"Auction ended: {e.Auction.Id}, Success: {e.IsSuccessful}");
        }
    }
}
