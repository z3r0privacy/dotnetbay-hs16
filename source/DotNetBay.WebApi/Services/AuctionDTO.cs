using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBay.WebApi.Services
{
    class AuctionDTO
    {
        public long id { get; set; }
        public double startPrice { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public double currentPrice { get; set; }
        public DateTime startDateTimeUtc { get; set; }
        public DateTime endDateTimeUtc { get; set; }
        public DateTime closeDateTimeUtc { get; set; }
        public string seller { get; set; }
        public string winner { get; set; }
        public long activeBid { get; set; }
        public bool isRunning { get; set; }
        public bool isClosed { get; set; }
    }
}
