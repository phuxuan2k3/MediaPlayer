using MediaPlayerProject.Models;
using System.Collections.ObjectModel;

namespace MediaPlayerProject.ViewModels
{
    public class PlayFileViewModel : ViewModelBase
    {
        public Playlist PlaylistData { get; set; }
        public ObservableCollection<MediaFile> MediaFiles { get; set; }
        public PlayFileViewModel(Playlist playlistData)
        {
            PlaylistData = playlistData;
            MediaFiles = new ObservableCollection<MediaFile>();
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
