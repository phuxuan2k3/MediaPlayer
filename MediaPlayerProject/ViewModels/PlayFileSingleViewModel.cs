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

        public ICommand? BackCommand { get; set; }

        public void SaveTimeSpan()
        {
            //...
        }

        public MediaFile CurrentPlayingMediaFile { get; set; }
        public Uri CurrentMediaSource => PathHelper.fileToUri(CurrentPlayingMediaFile);

        public PlayFileSingleViewModel(MediaFile mediaFile, string backTo)
        {
            var nsp = App.GetService<INavigationServiceProvider>();
            var ns_MFPVM = nsp.GetNavigationService(() => new MediaFilePoolViewModel());
            var ns_PLVM = nsp.GetNavigationService(() => new HistoryFileViewModel());
            CurrentPlayingMediaFile = mediaFile;
            switch (backTo)
            {
                case "MediaFilePoolViewModel":
                    BackCommand = new NavigateCommand(ns_MFPVM);
                    break;
                case "HistoryFileViewModel":
                    BackCommand = new NavigateCommand(ns_PLVM);
                    break;
            }
        }
    }
}
