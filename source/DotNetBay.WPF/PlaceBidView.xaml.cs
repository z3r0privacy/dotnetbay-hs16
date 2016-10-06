using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DotNetBay.Data.Entity;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for PlaceBidView.xaml
    /// </summary>
    public partial class PlaceBidView : Window
    {
        public Auction Auction { get; }

        public PlaceBidView(Auction auction)
        {
            InitializeComponent();
            this.Auction = auction;
            DataContext = this;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            double bid;
            if (double.TryParse(dblBid.Text, out bid))
            {
                if (bid < Auction.StartPrice)
                {
                    ShowError("Bid must be higher than or eqaul to starting price");
                    return;
                }
                else if (Auction.ActiveBid != null && Auction.ActiveBid.Amount >= bid)
                {
                    ShowError("Bid must be higher than the currently highest bid");
                    return;
                }
            }
            else
            {
                ShowError("Not a valid number");
                return;
            }

            (Application.Current as App).AuctionService.PlaceBid(Auction, bid);
            
            this.Close();
        }

        private void ShowError(string msg)
        {
            MessageBox.Show(this, msg, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
