using MediaPlayerProject.Models;
using MediaPlayerProject.Services;
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
        private readonly PlaylistList _playlistList;
        private readonly NavigationService playlistListingNavigationService;
        private readonly AddPlaylistViewModel _addPlaylistViewModel;
        public AddPlaylistCommand(ViewModels.AddPlaylistViewModel addPlaylistViewModel, PlaylistList playlistList, NavigationService playlistListViewNaviationService)
        {
            _playlistList = playlistList;
            this.playlistListingNavigationService = playlistListViewNaviationService;
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
                await _playlistList.addPlaylist(playlist);
            }
            catch (Exception e)
            {
            }

            playlistListingNavigationService.Navigate();
        }
        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_addPlaylistViewModel.PlaylistName) && base.CanExecute(parameter);
        }
    }
}
