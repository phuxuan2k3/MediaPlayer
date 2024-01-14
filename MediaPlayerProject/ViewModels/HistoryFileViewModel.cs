using MediaPlayerProject.Helpers;
using MediaPlayerProject.Models;
using MediaPlayerProject.Services.HistoryFileProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.ViewModels
{
    public class HistoryFileViewModel : ViewModelBase
    {
        public ObservableCollection<MediaFile> HistoryFiles { get; set; }
        HistoryFileViewModel()
        {
            HistoryFiles = new ObservableCollection<MediaFile>();
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
