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
                ImgPath.Text = diag.FileName;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
