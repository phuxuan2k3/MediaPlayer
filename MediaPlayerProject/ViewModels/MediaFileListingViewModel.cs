using MediaPlayerProject.Commands;
using MediaPlayerProject.Models;
using MediaPlayerProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaPlayerProject.ViewModels
{
    public class MediaFileListingViewModel
    {
        public ObservableCollection<MediaFileViewModel> MediaFiles { get; }
        //public IEnumerable<PlaylistViewModel> Playlists => _playlists;

        private PlaylistViewModel _selectedPlaylist;
        private readonly Playlist playlist;
        private readonly NavigationService playlistListingNavigationService;

        public PlaylistViewModel SelectedPlaylist
        {
            get { return _selectedPlaylist; }
            set
            {
                _selectedPlaylist = value;
            }
        }

        public ICommand LoadMediaFileCommand { get; }
        public ICommand CreatePlaylistCommand { get; }
        public ICommand DeletePlaylistCommand { get; }

        public MediaFileListingViewModel(Playlist playlist, NavigationService playlistListingNavigationService )
        {
            MediaFiles = new ObservableCollection<MediaFileViewModel>();
            this.playlist = playlist;
            this.playlistListingNavigationService = playlistListingNavigationService;
            //CreatePlaylistCommand = new NavigateCommand(addPlaylistNavigateService);
            //  DeletePlaylistCommand = new DeletePlaylistCommand(playlistList, playlistListingNavigationService, this);
        }

        public static MediaFileListingViewModel LoadViewModel(Playlist playlist, NavigationService playlistListingNavigationService)
        {
            MediaFileListingViewModel mediaFileListingViewModel = new MediaFileListingViewModel(playlist, playlistListingNavigationService);
            //mediaFileListingViewModel.LoadMediaFileCommand.Execute(null);
            return mediaFileListingViewModel;
        }



        public async void UpdateMediaFileList(IEnumerable<MediaFile> mediaFiles)
        {
            MediaFiles.Clear();

            foreach (var mediaFile in mediaFiles)
            {
                MediaFileViewModel mediaFileViewModel = new MediaFileViewModel(mediaFile);
                MediaFiles.Add(mediaFileViewModel);
            }
        }
    }
}
