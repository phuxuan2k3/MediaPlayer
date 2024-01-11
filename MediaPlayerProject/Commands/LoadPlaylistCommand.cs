using MediaPlayerProject.Models;
using MediaPlayerProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediaPlayerProject.Commands
{
    public class LoadPlaylistCommand : AsyncCommandBase
    {
        private readonly PlaylistList playlistList;
        private readonly PlaylistListingViewModel viewModel;

        public LoadPlaylistCommand(PlaylistList playlistList, PlaylistListingViewModel viewModel)
        {
            this.playlistList = playlistList;
            this.viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                IEnumerable<Playlist> playlists = await playlistList.GetItems();
                viewModel.UpdatePlaylistList(playlists);
            }catch (Exception ex)
            {
                MessageBox.Show("Error Loading!");
            }
        }
    }
}
