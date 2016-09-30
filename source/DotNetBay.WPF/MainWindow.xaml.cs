using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DotNetBay.Core;
using DotNetBay.Data.Entity;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Auction> auctions;
        private IAuctionService auctionService;
        private App app = ((App) Application.Current);

        public ObservableCollection<Auction> Auctions
        {
            get { return auctions; }
        }

        public MainWindow()
        {
            InitializeComponent();
            auctionService = new AuctionService(app.MainRepository, new SimpleMemberService(app.MainRepository));
            auctions = new ObservableCollection<Auction>(auctionService.GetAll());

            DataContext = this;
        }

        private void NewAuctionButton_Click(object sender, RoutedEventArgs e)
        {
            new NewAuctionView().ShowDialog();
        }

        private void PlaceBidButton_OnClick(object sender, RoutedEventArgs e)
        {
            var auction = (Auction) AuctionsDataGrid.SelectedItem;
            new PlaceBidView(auction).ShowDialog();
        }
    }
}
