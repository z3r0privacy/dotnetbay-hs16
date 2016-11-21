using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace DotNetBay.WebApi.DTOs
{
    public class Auction
    {
        public long Id { get; }
        [Required]
        public double StartPrice { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; }
        public double CurrentPrice { get; set; }
        [Required]
        public DateTime StartDateTimeUtc { get; set; }
        [Required]
        public DateTime EndDateTimeUtc { get; set; }
        public DateTime CloseDateTimeUtc { get; set; }
        [Required]
        public string Seller { get; set; }
        public string Winner { get; }
        public long ActiveBid { get; }
        public bool IsRunning { get; }
        public bool IsClosed { get; }

        public Auction()
        {
        }

        public Auction(Data.Entity.Auction a)
        {
            StartPrice = a.StartPrice;
            Title = a.Title;
            Description = a.Description;
            Image = $"/api/auctions/{a.Id}/image";
            CurrentPrice = a.CurrentPrice;
            StartDateTimeUtc = a.StartDateTimeUtc;
            EndDateTimeUtc = a.EndDateTimeUtc;
            CloseDateTimeUtc = a.CloseDateTimeUtc;
            Seller = a.Seller.DisplayName;
            Winner = a.Winner?.DisplayName;
            ActiveBid = a.ActiveBid?.Id ?? 0;
            IsRunning = a.IsRunning;
            IsClosed = a.IsClosed;
            Id = a.Id;
        }
    }
}
