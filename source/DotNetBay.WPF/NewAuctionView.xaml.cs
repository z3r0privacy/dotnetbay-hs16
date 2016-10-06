using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using DotNetBay.Core;
using DotNetBay.Data.Entity;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for NewAuctionView.xaml
    /// </summary>
    public partial class NewAuctionView : Window
    {
        public NewAuctionView()
        {
            InitializeComponent();
        }

        private void SearchImageButton_Click(object sender, RoutedEventArgs e)
        {
            var diag = new Microsoft.Win32.OpenFileDialog();
            diag.Filter =
                "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            var res = diag.ShowDialog();
            if (res.HasValue && res.Value)
            {
                txtImgPath.Text = diag.FileName;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (txtTitle.Text.Length < 5)
            {
                ShowError("Title is too short");
                return;
            }

            if (txtDesc.Text.Length < 10)
            {
                ShowError("Description is too short");
                return;
            }

            DateTime startDate;
            if (DateTime.TryParse(dateStart.Text, out startDate))
            {
                if (startDate < DateTime.Now)
                {
                    ShowError("Start date has to be in the future");
                    return;
                }
            }
            else
            {
                ShowError("Not a valid start date");
                return;
            }

            DateTime endDate;
            if (DateTime.TryParse(dateEnd.Text, out endDate))
            {
                if (endDate < DateTime.Now)
                {
                    ShowError("End date has to be in the future");
                    return;
                }

                if (endDate <= startDate.AddDays(1))
                {
                    ShowError("End date has to be at least one day after Startdate");
                    return;
                }
            }
            else
            {
                ShowError("Not a valid end date");
                return;
            }

            double price;
            if (double.TryParse(dblStartPrice.Text, out price))
            {
                if (price <= 0)
                {
                    ShowError("Startprice must be greater than 0");
                    return;
                }
            }
            else
            {
                ShowError("Not a valid start price");
                return;
            }

            if (!File.Exists(txtImgPath.Text))
            {
                ShowError("Picture not found");
                return;
            }

            var auction = new Auction
            {
                Title = txtTitle.Text,
                Description = txtDesc.Text,
                StartDateTimeUtc = startDate,
                EndDateTimeUtc = endDate,
                StartPrice = price,
                Seller = (Application.Current as App).MemberService.GetCurrentMember(),
                Image = File.ReadAllBytes(txtImgPath.Text)
            };

            (Application.Current as App).AuctionService.Save(auction);

            this.Close();
        }

        private void ShowError(string msg)
        {
            MessageBox.Show(this, msg, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
