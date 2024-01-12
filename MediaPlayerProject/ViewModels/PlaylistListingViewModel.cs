using MediaPlayerProject.Commands;
using MediaPlayerProject.Models;
using MediaPlayerProject.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MediaPlayerProject.ViewModels
{
    public class PlaylistListingViewModel : ViewModelBase
    {
        public ObservableCollection<Playlist> Playlists { get; set; }
        //public IEnumerable<PlaylistViewModel> Playlists => _playlists;

        private Playlist _selectedPlaylist;
        private readonly PlaylistList playlistList;

        public Playlist SelectedPlaylist
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
        public ICommand LoadMediaFileCommand { get; }
        public ICommand MediaFilePoolNavigateCommand { get; }
        public PlaylistListingViewModel(PlaylistList playlistList, NavigationService addPlaylistNavigateService,
            Func<Playlist, NavigationService> createMediaFileListingNavigationService, NavigationService mediaFilePoolNavigateCommand)
        {
            Playlists = new ObservableCollection<Playlist>();
            CreatePlaylistCommand = new NavigateCommand(addPlaylistNavigateService);
            LoadPlaylistCommand = new LoadPlaylistCommand(playlistList, this);
            DeletePlaylistCommand = new DeletePlaylistCommand(playlistList, this);
            LoadMediaFileCommand = new LoadMediaFileCommand(createMediaFileListingNavigationService);
            this.playlistList = playlistList;
            MediaFilePoolNavigateCommand = new NavigateCommand(mediaFilePoolNavigateCommand);
        }

        public static PlaylistListingViewModel LoadViewModel(PlaylistList playlistList, NavigationService addPlaylistNavigateService, Func<Playlist, NavigationService> createMediaFileListingNavigationService, NavigationService mediaFilePoolNavigateCommand)
        {
            PlaylistListingViewModel playlistListingViewModel = new PlaylistListingViewModel(playlistList,
                addPlaylistNavigateService, createMediaFileListingNavigationService, mediaFilePoolNavigateCommand);
            playlistListingViewModel.LoadPlaylistCommand.Execute(null);

            return playlistListingViewModel;
        }

        public async void UpdatePlaylistList()
        {
            this.Playlists = new ObservableCollection<Playlist>((await playlistList.GetItems()).ToList());
            //Playlists.Clear();

            //foreach (var playlist in playlistList)
            //{
            //    //PlaylistViewModel playlistViewModel = new PlaylistViewModel(playlist);
            //    //Playlists.Add(playlistViewModel);

            //}
        }
    }
}
