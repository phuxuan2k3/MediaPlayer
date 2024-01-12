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
    public class MediaFileListingViewModel : ViewModelBase
    {
        private readonly Playlist playlist;
        private readonly NavigationService playlistListingNavigationService;
        public MediaFile SelectedMediaFile { get; set; }
        public ObservableCollection<MediaFile> MediaFiles { get; set; }

        public Playlist Playlist { get { return this.playlist; } }

        public ICommand LoadMediaFileCommand { get; }
        public ICommand CreatePlaylistCommand { get; }
        public ICommand RemoveMediaFileCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand MediaFilePoolNavigateCommand { get; }
        public MediaFileListingViewModel(Playlist playlist, NavigationService playlistListingNavigationService, NavigationService mediaFilePoolNavigationService)
        {
            MediaFiles = new ObservableCollection<MediaFile>();
            this.playlist = playlist;
            this.playlistListingNavigationService = playlistListingNavigationService;
            CreatePlaylistCommand = new AddMediaFilesCommand(playlist, this);
            BackCommand = new NavigateCommand(playlistListingNavigationService);
            RemoveMediaFileCommand = new RemoveMediaFileCommand(this);
            MediaFilePoolNavigateCommand = new NavigateCommand(mediaFilePoolNavigationService);
        }

        public static MediaFileListingViewModel LoadViewModel(Playlist playlist, NavigationService playlistListingNavigationService, NavigationService mediaFilePoolNavigationService)
        {
            MediaFileListingViewModel mediaFileListingViewModel = new MediaFileListingViewModel(playlist, playlistListingNavigationService,mediaFilePoolNavigationService);
            mediaFileListingViewModel.UpdateMediaFileList();
            return mediaFileListingViewModel;
        }


        public async void UpdateMediaFileList()
        {
            MediaFiles.Clear();
            var mediaFiles = await this.playlist.GetItems();
            foreach (var mediaFile in mediaFiles)
            {
                MediaFiles.Add(mediaFile);
            }
        }
    }
}
