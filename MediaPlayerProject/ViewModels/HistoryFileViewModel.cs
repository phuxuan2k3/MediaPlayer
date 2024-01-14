using MediaPlayerProject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.ViewModels
{
    public class HistoryFileViewModel : ViewModelBase
    {
        HistoryFileViewModel()
        {
            HistoryHelper = new HistoryHelper();
        }
        HistoryHelper HistoryHelper { get; set; }

    }
}
