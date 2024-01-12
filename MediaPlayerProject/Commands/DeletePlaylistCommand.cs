using MediaPlayerProject.Models;
using MediaPlayerProject.Services;
using MediaPlayerProject.Services.PlaylistDelete;
using MediaPlayerProject.ViewModels;
using System;
using System.ComponentModel;

namespace MediaPlayerProject.Commands
{
    public class DeletePlaylistCommand : CommandBase
    {
        private readonly PlaylistListingViewModel _playlistListingViewModel;

        public DeletePlaylistCommand(PlaylistListingViewModel playlistListingViewModel)
        {
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
                var sv = App.GetService<IPlaylistDelete>();
                await sv.DeletePlaylist(playlist);
                _playlistListingViewModel.UpdatePlaylistList();
            }
            catch (Exception e)
            {
            }
        }
    }
}
