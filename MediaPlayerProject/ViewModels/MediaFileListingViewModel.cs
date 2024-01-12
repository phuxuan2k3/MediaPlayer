using MediaPlayerProject.Commands;
using MediaPlayerProject.Models;
using MediaPlayerProject.Services.MediaFileProviders;
using MediaPlayerProject.Services.NavigationServiceProvider;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MediaPlayerProject.ViewModels
{
    public class MediaFileListingViewModel : ViewModelBase
    {
        public Playlist PlaylistData { get; }

        public MediaFile? SelectedMediaFile { get; set; }
        public ObservableCollection<MediaFile> MediaFiles { get; set; }

        public ICommand ChooseFromSystemCommand { get; }
        public ICommand AddMediaFilesCommand { get; }
        public ICommand RemoveMediaFileCommand { get; }
        public ICommand BackCommand { get; }

        public MediaFileListingViewModel(Playlist playlist)
        {
            PlaylistData = playlist;
            var nsp = App.GetService<INavigationServiceProvider>();
            var ns_PLVM = nsp.GetNavigationService(PlaylistListingViewModel.LoadViewModel);
            MediaFiles = new ObservableCollection<MediaFile>();
            ChooseFromSystemCommand = new AddMediaFilesFromSystemCommand(playlist, this);
            AddMediaFilesCommand = new AddMediaFilesFromPoolCommand(playlist, this);
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
