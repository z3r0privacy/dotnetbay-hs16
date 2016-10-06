using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Core.Execution;
using DotNetBay.Data.Entity;
using DotNetBay.Data.Provider.FileStorage;
using DotNetBay.Interfaces;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IMainRepository MainRepository { get; }
        public IAuctionRunner AuctionRunner { get; }
        public IMemberService MemberService { get; }
        public IAuctionService AuctionService { get; }

        public App()
        {
            MainRepository = new FileSystemMainRepository("Repo1.rp");
            AuctionRunner = new AuctionRunner(MainRepository);
            AuctionRunner.Start();

            MemberService = new SimpleMemberService(this.MainRepository);
            AuctionService = new AuctionService(this.MainRepository, MemberService);
            if (!AuctionService.GetAll().Any())
            {
                var me = MemberService.GetCurrentMember();
                AuctionService.Save(new Auction
                {
                    Title = "My First Auction", StartDateTimeUtc = DateTime.UtcNow.AddSeconds(10), EndDateTimeUtc = DateTime.UtcNow.AddDays(14), StartPrice = 72, Seller = me
                });
            }
        }
    }
}
