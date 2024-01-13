using MediaPlayerProject.Commands;
using MediaPlayerProject.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MediaPlayerProject.ViewModels
{
    public class PlayFileViewModel : ViewModelBase
    {
        public Uri mediaSource { get; set; }

        public Playlist PlaylistData { get; set; }
        public ObservableCollection<MediaFile> MediaFiles { get; set; }
        public ICommand BackCommand { get; set; }
        public PlayFileViewModel(Playlist playlistData)
        {
            PlaylistData = playlistData;
            MediaFiles = new ObservableCollection<MediaFile>();
            BackCommand = new LoadMediaFileCommand();
            UpdateViewModel();
        }
        public async void UpdateViewModel()
        {
            MediaFiles.Clear();
            var mediaFiles = await PlaylistData.getFiles();
            foreach (var file in mediaFiles)
            {
                MediaFiles.Add(file);
            }
        }

    }
}
