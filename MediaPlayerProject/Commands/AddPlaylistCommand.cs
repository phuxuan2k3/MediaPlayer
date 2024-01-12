using MediaPlayerProject.Models;
using MediaPlayerProject.Services;
using MediaPlayerProject.Services.NavigationServiceProvider;
using MediaPlayerProject.Services.PlaylistCreators;
using MediaPlayerProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Commands
{
    public class AddPlaylistCommand : AsyncCommandBase
    {
        private readonly AddPlaylistViewModel _addPlaylistViewModel;
        public AddPlaylistCommand(AddPlaylistViewModel addPlaylistViewModel)
        {
            _addPlaylistViewModel = addPlaylistViewModel;
            _addPlaylistViewModel.PropertyChanged += OnViewModelPropertyChanged!;
        }
        private void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AddPlaylistViewModel.PlaylistName))
            {
                OnCanExecuteChanged();
            }
        }
        public override async Task ExecuteAsync(object? parameter)
        {
            Playlist playlist = new Playlist(_addPlaylistViewModel.PlaylistName!, DateTime.Now);
            try
            {
                var sv = App.GetService<IPlaylistCreators>();
                await sv.CreatePlaylist(playlist);
            }
            catch (Exception e)
            {
            }
            var nsp = App.GetService<INavigationServiceProvider>();
            var ns = nsp.GetNavigationService(PlaylistListingViewModel.LoadViewModel);
            ns.Navigate();
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_addPlaylistViewModel.PlaylistName) && base.CanExecute(parameter);
        }
    }
}
