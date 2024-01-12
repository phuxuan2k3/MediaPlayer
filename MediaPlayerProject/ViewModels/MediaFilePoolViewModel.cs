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
    public class MediaFilePoolViewModel : ViewModelBase
    {
        private readonly NavigationService playlistListingNavigationService;
        private readonly PlaylistList app;
        public PlaylistList App => app;
        public MediaFile SelectedMediaFile { get; set; }
        public ObservableCollection<MediaFile> MediaFiles { get; set; }

        public ICommand AddMediaFileCommand { get; }
        public ICommand RemoveMediaFilePoolCommand { get; }
        public ICommand BackCommand { get; }

        public MediaFilePoolViewModel(PlaylistList app, NavigationService playlistListingNavigationService)
        {
            this.app = app;
            MediaFiles = new ObservableCollection<MediaFile>();
            this.playlistListingNavigationService = playlistListingNavigationService;
            BackCommand = new NavigateCommand(playlistListingNavigationService);
            AddMediaFileCommand = new AddMediaFilePoolCommand(this);
            this.RemoveMediaFilePoolCommand = new RemoveMediaPoolCommand(this);
            UpdateMediaFileList();
        }

        //public static MediaFileListingViewModel LoadViewModel(Playlist playlist, NavigationService playlistListingNavigationService)
        //{
        //    MediaFileListingViewModel mediaFileListingViewModel = new MediaFileListingViewModel(playlist, playlistListingNavigationService);
        //    mediaFileListingViewModel.UpdateMediaFileList();
        //    return mediaFileListingViewModel;
        //}


        public async void UpdateMediaFileList()
        {
            MediaFiles.Clear();
            var mediaFiles = await this.app.GetMediaFilePool();
            foreach (var mediaFile in mediaFiles)
            {
                MediaFiles.Add(mediaFile);
            }
        }
    }
}
