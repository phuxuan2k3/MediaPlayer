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
    /// Interaction logic for PlaylistListingView.xaml
    /// </summary>
    public partial class PlaylistListingView : UserControl
    {
        public PlaylistListingView()
        {
            InitializeComponent();
        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var viewModel = (PlaylistListingViewModel)(this.DataContext);
            var playlist = (Playlist)(((Button)(sender)).DataContext);
            viewModel.LoadMediaFileCommand.Execute(playlist);
        }
    }
}
