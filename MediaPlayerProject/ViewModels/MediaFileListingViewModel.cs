using MediaPlayerProject.Commands;
using MediaPlayerProject.Models;
using MediaPlayerProject.Services.MediaFIlePoolProvider;
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

        public ObservableCollection<MediaFile> MediaFilesPool { get; set; }

        public ICommand AddMediaFilesFromSystemCommand { get; }
        public ICommand AddMediaFilesFromPoolCommand { get; }
        public ICommand RemoveMediaFileCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand PlayFileCommand { get; }

        public MediaFileListingViewModel(Playlist playlist)
        {
            PlayFileCommand = new PlayFileCommand();
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
            MediaFiles.Clear();
            var mediaFiles = await PlaylistData.getFiles();
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
