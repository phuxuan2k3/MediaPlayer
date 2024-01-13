using MediaPlayerProject.Commands;
using MediaPlayerProject.Helpers;
using MediaPlayerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace MediaPlayerProject.ViewModels
{
    public class PlayFileViewModel : ViewModelBase
    {
        private static readonly Random _rng = new Random();
        private readonly List<MediaFile> _mediaFiles;

        public Playlist PlaylistData { get; set; }
        public List<MediaFile> DisplayMediaFiles { get; set; }
        public ICommand BackCommand { get; set; }

        private bool _isShuffle;
        public bool IsShuffle
        {
            get => _isShuffle;
            set
            {
                _isShuffle = value;
                if (_isShuffle == true)
                {
                    DisplayMediaFiles = _mediaFiles.OrderBy(a => _rng.Next()).ToList();
                }
                CurrentIndex = 0;
                OnPropertyChanged(nameof(DisplayMediaFiles));
            }
        }

        public void SaveTimeSpan()
        {
            //...
        }

        private int _currentIndex;
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                // Trc khi chuyen video
                // TODO:
                //SaveTimeSpan();
                _currentIndex = value;
                // Sau 
                if (_currentIndex >= _mediaFiles.Count)
                {
                    _currentIndex = 0;
                }
                else if (_currentIndex < 0)
                {
                    _currentIndex = _mediaFiles.Count - 1;
                }
                OnPropertyChanged(nameof(CurrentPlayingMediaFile));
                OnPropertyChanged(nameof(CurrentMediaSource));
            }
        }
        public MediaFile? CurrentPlayingMediaFile => DisplayMediaFiles[CurrentIndex];
        public Uri? CurrentMediaSource => PathHelper.fileToUri(CurrentPlayingMediaFile);


        public PlayFileViewModel(Playlist playlistData)
        {
            PlaylistData = playlistData;
            _mediaFiles = new List<MediaFile>();
            DisplayMediaFiles = new List<MediaFile>();
            BackCommand = new LoadMediaFileCommand();
            //SaveTimeSpanCommand = new SaveMediaFileTimeSpanCommand();
            UpdateViewModel();
        }

        public async void UpdateViewModel()
        {
            _mediaFiles.Clear();
            DisplayMediaFiles.Clear();
            var mediaFiles = await PlaylistData.getFiles();
            foreach (var file in mediaFiles)
            {
                _mediaFiles.Add(file);
                DisplayMediaFiles.Add(file);
            }
        }
    }
}
