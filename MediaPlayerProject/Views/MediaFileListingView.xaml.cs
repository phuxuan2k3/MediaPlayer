using MediaPlayerProject.Models;
using MediaPlayerProject.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediaPlayerProject.Views
{
    /// <summary>
    /// Interaction logic for MediaFileListingView.xaml
    /// </summary>
    public partial class MediaFileListingView : UserControl
    {
        public MediaFileListingView()
        {
            InitializeComponent();
        }

        private void ListViewItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var viewModel = (MediaFileListingViewModel)(this.DataContext);
            var playlist = (MediaFile)((ListViewItem)sender).DataContext;
        }


        private void UC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DataListView.UnselectAll();
            PoolListView.UnselectAll();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
