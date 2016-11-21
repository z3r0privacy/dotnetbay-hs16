using DotNetBay.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.Data.Entity;
using DotNetBay.Interfaces;
using System.Net.Http;
using DotNetBay.Data.EF;
using Newtonsoft.Json;

namespace DotNetBay.WebApi.Services
{
    public class RemoteAuctionService : IAuctionService
    {
        private IMainRepository repo;
        private HttpClient client;

        public RemoteAuctionService()
        {
            repo = new EFMainRepository();
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:1901/api/");
        }

        public IQueryable<Auction> GetAll()
        {
            List<long> ids = null;
            var task = client.GetAsync("auctions")
                .ContinueWith(response =>
                {
                    var result = response.Result;
                    var jsonstring = result.Content.ReadAsStringAsync();
                    jsonstring.Wait();
                    ids = JsonConvert.DeserializeObject<List<AuctionDTO>>(jsonstring.Result).Select(a => a.id).ToList();
                });
            task.Wait();

            var auctions = new List<AuctionDTO>();
            foreach(var id in ids)
            {
                var aTask = client.GetAsync($"auctions/{id}")
                    .ContinueWith(response =>
                    {
                        var result = response.Result;
                        var jsonstring = result.Content.ReadAsStringAsync();
                        jsonstring.Wait();
                        auctions.Add(JsonConvert.DeserializeObject<AuctionDTO>(jsonstring.Result));
                    });
                aTask.Wait();
            }


            return new List<Auction>().AsQueryable();
        }

        public Bid PlaceBid(Auction auction, double amount)
        {
            throw new NotImplementedException();
        }

        public Auction Save(Auction auction)
        {
            throw new NotImplementedException();
        }
    }
}
