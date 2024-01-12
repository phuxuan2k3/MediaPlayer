using MediaPlayerProject.Commands;
using MediaPlayerProject.Models;
using MediaPlayerProject.Services;
using MediaPlayerProject.Services.MediaFIlePoolProvider;
using MediaPlayerProject.Services.NavigationServiceProvider;
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
        public MediaFile? SelectedMediaFile { get; set; }
        public ObservableCollection<MediaFile> MediaFiles { get; set; }

        public ICommand AddMediaFileCommand { get; }
        public ICommand RemoveMediaFilePoolCommand { get; }
        public ICommand BackCommand { get; }

        public MediaFilePoolViewModel()
        {
            var nsp = App.GetService<INavigationServiceProvider>();
            var ns_PLVM = nsp.GetNavigationService(() => new PlaylistListingViewModel());
            MediaFiles = new ObservableCollection<MediaFile>();
            BackCommand = new NavigateCommand(ns_PLVM);
            AddMediaFileCommand = new AddMediaFilePoolCommand(this);
            RemoveMediaFilePoolCommand = new RemoveMediaPoolCommand(this);
            UpdateMediaFileList();
        }

        public async void UpdateMediaFileList()
        {
            MediaFiles.Clear();
            var sv = App.GetService<IMediaFIlePoolProvider>();
            var mediaFiles = await sv.getMediaFIlePool();
            foreach (var mediaFile in mediaFiles)
            {
                MediaFiles.Add(mediaFile);
            }
        }
    }
}
