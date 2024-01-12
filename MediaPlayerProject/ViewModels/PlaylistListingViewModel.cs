using MediaPlayerProject.Commands;
using MediaPlayerProject.Models;
using MediaPlayerProject.Services.NavigationServiceProvider;
using MediaPlayerProject.Services.PlaylistProviders;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MediaPlayerProject.ViewModels
{
    public class PlaylistListingViewModel : ViewModelBase
    {
        public Playlist? SelectedPlaylist { get; set; }
        public ObservableCollection<Playlist> Playlists { get; set; }

        public ICommand CreatePlaylistCommand { get; }
        public ICommand DeletePlaylistCommand { get; }
        public ICommand LoadMediaFileCommand { get; }
        public ICommand MediaFilePoolNavigateCommand { get; }

        public PlaylistListingViewModel()
        {
            Playlists = new ObservableCollection<Playlist>();

            var nsp = App.GetService<INavigationServiceProvider>();
            var ns_APLVM = nsp.GetNavigationService(() => new AddPlaylistViewModel());
            var ns_MFPVM = nsp.GetNavigationService(() => new MediaFilePoolViewModel());
            CreatePlaylistCommand = new NavigateCommand(ns_APLVM);
            MediaFilePoolNavigateCommand = new NavigateCommand(ns_MFPVM);
            DeletePlaylistCommand = new DeletePlaylistCommand(this);
            LoadMediaFileCommand = new LoadMediaFileCommand();
            UpdatePlaylistList();
        }

        public static PlaylistListingViewModel LoadViewModel()
        {
            PlaylistListingViewModel playlistListingViewModel = new PlaylistListingViewModel();
            return playlistListingViewModel;
        }

        public async void UpdatePlaylistList()
        {
            var sv = App.GetService<IPlaylistProvider>();
            this.Playlists = new ObservableCollection<Playlist>((await sv.GetAllPlaylist()).ToList());
        }
    }
}
