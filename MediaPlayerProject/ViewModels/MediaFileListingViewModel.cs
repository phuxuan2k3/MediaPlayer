using MediaPlayerProject.Commands;
using MediaPlayerProject.Models;
using MediaPlayerProject.Services;
using MediaPlayerProject.Services.MediaFIlePoolProvider;
using MediaPlayerProject.Services.MediaFileProviders;
using MediaPlayerProject.Services.NavigationServiceProvider;
using MediaPlayerProject.Services.PlaylistProviders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MediaPlayerProject.ViewModels
{
    public class MediaFileListingViewModel : ViewModelBase
    {
        public Playlist PlaylistData { get; }

        public MediaFile? SelectedMediaFile { get; set; }
        public ObservableCollection<MediaFile> SelectedMediaFilePool { get; set; }
        public ObservableCollection<MediaFile> MediaFiles { get; set; }
        public ObservableCollection<MediaFile> MediaFilesPool { get; set; }

        public ICommand AddMediaFilesFromSystemCommand { get; }
        public ICommand AddMediaFilesFromPoolCommand { get; }
        public ICommand RemoveMediaFileCommand { get; }
        public ICommand BackCommand { get; }

        public MediaFileListingViewModel(Playlist playlist)
        {
            SelectedMediaFilePool = new ObservableCollection<MediaFile>();
            PlaylistData = playlist;
            MediaFiles = new ObservableCollection<MediaFile>();
            MediaFilesPool = new ObservableCollection<MediaFile>();
            AddMediaFilesFromSystemCommand = new AddMediaFilesFromSystemCommand(playlist, this);
            AddMediaFilesFromPoolCommand = new AddMediaFileFromPoolCommand(playlist, this);
            var nsp = App.GetService<INavigationServiceProvider>();
            var ns_PLVM = nsp.GetNavigationService(PlaylistListingViewModel.LoadViewModel);
            BackCommand = new NavigateCommand(ns_PLVM);
            RemoveMediaFileCommand = new RemoveMediaFileCommand(this);
            UpdateViewModel();
        }

        public static MediaFileListingViewModel LoadViewModel(Playlist playlist)
        {
            MediaFileListingViewModel mediaFileListingViewModel = new MediaFileListingViewModel(playlist);
            return mediaFileListingViewModel;
        }

        public async void UpdateViewModel()
        {
            var sv = App.GetService<IMediaFileProvider>();
            MediaFiles.Clear();
            var mediaFiles = await sv.GetMediaFiles(this.PlaylistData);
            foreach (var mediaFile in mediaFiles)
            {
                MediaFiles.Add(mediaFile);
            }

            var sv_MFP = App.GetService<IMediaFIlePoolProvider>();
            MediaFilesPool.Clear();
            var mediaFilesPool = await sv_MFP.getMediaFIlePool();
            foreach (var mediaFile in mediaFilesPool)
            {
                MediaFilesPool.Add(mediaFile);
            }
        }
    }
}
