using MediaPlayerProject.Models;
using MediaPlayerProject.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void Options_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Options_Button.Visibility = Visibility.Collapsed;
            this.LayoutTriggerGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Options_Button.Visibility = Visibility.Visible;
            this.LayoutTriggerGrid.ColumnDefinitions[1].Width = new GridLength(0);
        }

        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            var items = this.PoolListView.SelectedItems;
            var vm = (MediaFileListingViewModel)this.DataContext;
            vm.AddMediaFilesFromPoolCommand.Execute(items);
        }
    }
}
