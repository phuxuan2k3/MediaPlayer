using MediaPlayerProject.Commands;
using MediaPlayerProject.Models;
using MediaPlayerProject.Services.NavigationServiceProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaPlayerProject.ViewModels
{
    public class AddPlaylistViewModel : ViewModelBase
    {
        public string? PlaylistName { get; set; }
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        public AddPlaylistViewModel()
        {
            SubmitCommand = new AddPlaylistCommand(this);
            var nsp = App.GetService<INavigationServiceProvider>();
            var ns = nsp.GetNavigationService(PlaylistListingViewModel.LoadViewModel);
            CancelCommand = new NavigateCommand(ns);
        }
    }
}
