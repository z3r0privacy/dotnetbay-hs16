using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
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
using DotNetBay.Core.Execution;
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

            app.AuctionRunner.Auctioneer.AuctionEnded += AuctioneerOnAuctionEnded;
            app.AuctionRunner.Auctioneer.AuctionStarted += AuctioneerOnAuctionStarted; 
            app.AuctionRunner.Auctioneer.BidAccepted += AuctioneerOnBidAccepted; 
            app.AuctionRunner.Auctioneer.BidDeclined += AuctioneerOnBidDeclined;
        }

        private void NewAuctionButton_Click(object sender, RoutedEventArgs e)
        {
            new NewAuctionView().ShowDialog();
        }

        private void PlaceBidButton_OnClick(object sender, RoutedEventArgs e)
        {
            var auction = (Auction) AuctionsDataGrid.SelectedItem;
            new PlaceBidView(auction).ShowDialog();

            AuctionsDataGrid.Items.Refresh();
        }

        private void AuctioneerOnBidDeclined(object sender, ProcessedBidEventArgs processedBidEventArgs)
        {
            try
            {
                AuctionsDataGrid.Dispatcher.Invoke(new Action(() => AuctionsDataGrid.Items.Refresh()));
            }
            catch (InvalidOperationException ioex)
            {
                Debug.WriteLine($"OnBidDeclined: {ioex.Message}");
            }
        }

        private void AuctioneerOnBidAccepted(object sender, ProcessedBidEventArgs processedBidEventArgs)
        {
            try
            {
                AuctionsDataGrid.Dispatcher.Invoke(new Action(() => AuctionsDataGrid.Items.Refresh()));
            }
            catch (InvalidOperationException ioex)
            {
                Debug.WriteLine($"OnBidAccepted: {ioex.Message}");
            }
        }

        private void AuctioneerOnAuctionStarted(object sender, AuctionEventArgs auctionEventArgs)
        {
            try
            {
                AuctionsDataGrid.Dispatcher.Invoke(new Action(() => AuctionsDataGrid.Items.Refresh()));
            }
            catch (InvalidOperationException ioex)
            {
                Debug.WriteLine($"OnAuctionStarted: {ioex.Message}");
            }
        }

        private void AuctioneerOnAuctionEnded(object sender, AuctionEventArgs auctionEventArgs)
        {
            try
            {
                AuctionsDataGrid.Dispatcher.Invoke(new Action(() => AuctionsDataGrid.Items.Refresh()));
            }
            catch (InvalidOperationException ioex)
            {
                Debug.WriteLine($"OnAuctionEnded: {ioex.Message}");
            }
        }

    }

    public class BooleanToStatusTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (bool) value;
            return status ? "Open" : "Closed";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (string) value;
            return status == "Open";
        }
    }

    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool) value ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Visibility) value == Visibility.Visible;
        }
    }
}
