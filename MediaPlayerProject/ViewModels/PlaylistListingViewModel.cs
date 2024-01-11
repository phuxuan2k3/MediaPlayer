using MediaPlayerProject.Commands;
using MediaPlayerProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaPlayerProject.ViewModels
{
    public class PlaylistListingViewModel : ViewModelBase
    {
        public ObservableCollection<PlaylistViewModel> Playlists { get; }
        //public IEnumerable<PlaylistViewModel> Playlists => _playlists;

        private PlaylistViewModel _selectedPlaylist;
        public PlaylistViewModel SelectedPlaylist
        {
            get { return _selectedPlaylist; }
            set
            {
                _selectedPlaylist = value;
            }
        }

        public ICommand LoadPlaylistCommand { get; }
        public ICommand CreatePlaylistCommand { get; }
        public ICommand DeletePlaylistCommand { get; }

        public PlaylistListingViewModel(PlaylistList playlistList, Services.NavigationService? addPlaylistNavigateService, Services.NavigationService playlistListingNavigationService)
        {
            Playlists = new ObservableCollection<PlaylistViewModel>();
            CreatePlaylistCommand = new NavigateCommand(addPlaylistNavigateService);
            LoadPlaylistCommand = new LoadPlaylistCommand(playlistList, this);
            DeletePlaylistCommand = new DeletePlaylistCommand(playlistList, playlistListingNavigationService, this);
        }

        public static PlaylistListingViewModel LoadViewModel(PlaylistList playlistList, Services.NavigationService? addPlaylistNavigateService, Services.NavigationService playlistListingNavigationService)
        {
            PlaylistListingViewModel playlistListingViewModel = new PlaylistListingViewModel(playlistList, addPlaylistNavigateService, playlistListingNavigationService);
            playlistListingViewModel.LoadPlaylistCommand.Execute(null);

            return playlistListingViewModel;
        }

        public async void UpdatePlaylistList(IEnumerable<Playlist> playlistList)
        {
            Playlists.Clear();

            foreach (var playlist in playlistList)
            {
                PlaylistViewModel playlistViewModel = new PlaylistViewModel(playlist);
                Playlists.Add(playlistViewModel);
            }
        }
    }
}
