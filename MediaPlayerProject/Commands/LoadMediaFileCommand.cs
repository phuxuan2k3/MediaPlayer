using MediaPlayerProject.Models;
using MediaPlayerProject.Services;
using MediaPlayerProject.Stores;
using MediaPlayerProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediaPlayerProject.Commands
{
    public class LoadMediaFileCommand : AsyncCommandBase
    {
        private readonly NavigationService playlistListingNavigationService;

        public LoadMediaFileCommand(NavigationStore navigationStore, NavigationService playlistListingNavigationService)
        {
            this.playlistListingNavigationService = playlistListingNavigationService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                var pl = (Playlist) parameter;
                var navSer = new NavigationService(navigationStore, () => new MediaFileListingViewModel(pl, ));
                IEnumerable<MediaFile> mediaFiles = await pl.GetItems();
                this.playlistListingNavigationService.Navigate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
