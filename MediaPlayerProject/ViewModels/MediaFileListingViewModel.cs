using MediaPlayerProject.Commands;
using MediaPlayerProject.Models;
using MediaPlayerProject.Services;
using MediaPlayerProject.Services.MediaFileProviders;
using MediaPlayerProject.Services.NavigationServiceProvider;
using MediaPlayerProject.Services.PlaylistProviders;
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
        public Playlist PlaylistData { get; }

        public MediaFile? SelectedMediaFile { get; set; }
        public ObservableCollection<MediaFile> MediaFiles { get; set; }

        public ICommand AddMediaFilesFromSystemCommand { get; }
        public ICommand RemoveMediaFileCommand { get; }
        public ICommand BackCommand { get; }

        public MediaFileListingViewModel(Playlist playlist)
        {
            PlaylistData = playlist;
            MediaFiles = new ObservableCollection<MediaFile>();
            AddMediaFilesFromSystemCommand = new AddMediaFilesFromSystemCommand(playlist, this);
            var nsp = App.GetService<INavigationServiceProvider>();
            var ns_PLVM = nsp.GetNavigationService(PlaylistListingViewModel.LoadViewModel);
            BackCommand = new NavigateCommand(ns_PLVM);
            RemoveMediaFileCommand = new RemoveMediaFileCommand(this);
            UpdateMediaFileList();
        }

        public static MediaFileListingViewModel LoadViewModel(Playlist playlist)
        {
            MediaFileListingViewModel mediaFileListingViewModel = new MediaFileListingViewModel(playlist);
            return mediaFileListingViewModel;
        }

        public async void UpdateMediaFileList()
        {
            var sv = App.GetService<IMediaFileProvider>();
            MediaFiles.Clear();
            var mediaFiles = await sv.GetMediaFiles(this.PlaylistData);
            foreach (var mediaFile in mediaFiles)
            {
                MediaFiles.Add(mediaFile);
            }
        }
    }
}
