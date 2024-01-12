using MediaPlayerProject.Models;
using MediaPlayerProject.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

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
