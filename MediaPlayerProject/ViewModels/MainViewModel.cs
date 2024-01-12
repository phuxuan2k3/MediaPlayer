using MediaPlayerProject.Models;
using MediaPlayerProject.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public MainViewModel()
        {
            _navigationStore = App.GetService<NavigationStore>();
            _navigationStore.CurrentViewModelChanged += OncurrentViewModelChanged;
        }

        private void OncurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
