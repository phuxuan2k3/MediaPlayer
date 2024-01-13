using MediaPlayerProject.Commands;
using MediaPlayerProject.Helpers;
using MediaPlayerProject.Models;
using MediaPlayerProject.Services.NavigationServiceProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace MediaPlayerProject.ViewModels
{
    public class PlayFileSingleViewModel : ViewModelBase
    {

        public ICommand BackCommand { get; set; }

        public void SaveTimeSpan()
        {
            //...
        }

        public MediaFile CurrentPlayingMediaFile { get; set; }
        public Uri CurrentMediaSource => PathHelper.fileToUri(CurrentPlayingMediaFile);

        public PlayFileSingleViewModel(MediaFile mediaFile)
        {
            var nsp = App.GetService<INavigationServiceProvider>();
            var ns_MFPVM = nsp.GetNavigationService(() => new MediaFilePoolViewModel());
            CurrentPlayingMediaFile = mediaFile;
            BackCommand = new NavigateCommand(ns_MFPVM);
        }
    }
}
