using MediaPlayerProject.Models;
using MediaPlayerProject.Services;
using MediaPlayerProject.ViewModels;
using System;
using System.ComponentModel;

namespace MediaPlayerProject.Commands
{
    public class DeletePlaylistCommand : CommandBase
    {
        private readonly PlaylistList _playlistList;
        private readonly NavigationService _playlistListingNavigationService;
        private readonly PlaylistListingViewModel _playlistListingViewModel;

        public DeletePlaylistCommand(PlaylistList playlistList, PlaylistListingViewModel playlistListingViewModel)
        {
            _playlistList = playlistList;
            _playlistListingViewModel = playlistListingViewModel;
            _playlistListingViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PlaylistListingViewModel.SelectedPlaylist))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_playlistListingViewModel.SelectedPlaylist != null && base.CanExecute(parameter));
        }

        public override async void Execute(object? parameter)
        {
            var playlist = new Playlist(_playlistListingViewModel.SelectedPlaylist.Name, _playlistListingViewModel.SelectedPlaylist.TimeCreated, _playlistListingViewModel.SelectedPlaylist.Id);
            try
            {
                await _playlistList.deletePlaylist(playlist);
                _playlistListingViewModel.UpdatePlaylistList();
            }
            catch (Exception e)
            {
            }
        }
    }
}
