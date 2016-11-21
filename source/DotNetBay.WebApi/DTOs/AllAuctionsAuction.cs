using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBay.WebApi.DTOs
{
    public class AllAuctionsAuction
    {
        private Data.Entity.Auction _auction;

        public long Id => _auction.Id;
        public string Title => _auction.Title;
        public double CurrentPrice => _auction.CurrentPrice;
        public string Seller => _auction.Seller.DisplayName;
        public string Winner => _auction.Winner?.DisplayName;
        public bool IsRunning => _auction.IsRunning;

        public AllAuctionsAuction(Data.Entity.Auction _auction)
        {
            this._auction = _auction;
        }

        public AllAuctionsAuction() { }
    }
}
