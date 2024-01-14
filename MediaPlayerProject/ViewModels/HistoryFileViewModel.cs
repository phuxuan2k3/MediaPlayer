using MediaPlayerProject.Commands;
using MediaPlayerProject.Helpers;
using MediaPlayerProject.Models;
using MediaPlayerProject.Services.HistoryFileProvider;
using MediaPlayerProject.Services.NavigationServiceProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaPlayerProject.ViewModels
{
    public class HistoryFileViewModel : ViewModelBase
    {
        public ObservableCollection<MediaFile> HistoryFiles { get; set; }
        public Func<MediaFile>? GetMediaFileData { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand PlayMediaFileCommand { get; set; }
        public HistoryFileViewModel()
        {
            HistoryFiles = new ObservableCollection<MediaFile>();

            var nsp = App.GetService<INavigationServiceProvider>();
            var ns_PLVM = nsp.GetNavigationService(() => new PlaylistListingViewModel());
            var ns_PFSVM = nsp.GetNavigationService(() => new PlayFileSingleViewModel(GetMediaFileData!.Invoke(), "HistoryFileViewModel"));
            BackCommand = new NavigateCommand(ns_PLVM);
            PlayMediaFileCommand = new NavigateCommand(ns_PFSVM);
            UpdateViewModel();
        }
        public void UpdateViewModel()
        {
            var sv = App.GetService<IHistoryFileProvider>();
            var listFile = sv.GetHistoryFile();
            HistoryFiles.Clear();
            foreach (var file in listFile)
            {
                HistoryFiles.Add(file);
            }
        }
    }
}
