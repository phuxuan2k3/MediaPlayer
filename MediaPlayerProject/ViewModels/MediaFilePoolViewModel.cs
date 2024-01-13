﻿using MediaPlayerProject.Commands;
using MediaPlayerProject.Models;
using MediaPlayerProject.Services;
using MediaPlayerProject.Services.MediaFIlePoolProvider;
using MediaPlayerProject.Services.NavigationServiceProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ICommand PlayMediaFileCommand { get; }

        public MediaFilePoolViewModel()
        {
            var nsp = App.GetService<INavigationServiceProvider>();
            var ns_PLVM = nsp.GetNavigationService(() => new PlaylistListingViewModel());
            var ns_PFSVM = nsp.GetNavigationService(() => new PlayFileSingleViewModel(GetMediaFileData!.Invoke()));
            MediaFiles = new ObservableCollection<MediaFile>();
            BackCommand = new NavigateCommand(ns_PLVM);
            AddMediaFileCommand = new AddMediaFilePoolCommand(this);
            RemoveMediaFilePoolCommand = new RemoveMediaPoolCommand(this);
            PlayMediaFileCommand = new NavigateCommand(ns_PFSVM);
            UpdateMediaFileList();
        }

        public Func<MediaFile> GetMediaFileData { get; set; }

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
