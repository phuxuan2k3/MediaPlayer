using MediaPlayerProject.Models;
using MediaPlayerProject.Services;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace MediaPlayerProject.Commands
{
    public class LoadMediaFileCommand : AsyncCommandBase
    {
        private readonly Func<Playlist, NavigationService> createMediaFileNavigationService;

        public LoadMediaFileCommand(Func<Playlist, NavigationService> createMediaFileNavigationService)
        {
            this.createMediaFileNavigationService = createMediaFileNavigationService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                var pl = (Playlist)parameter;
                var ns = createMediaFileNavigationService.Invoke(pl);
                ns.Navigate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
