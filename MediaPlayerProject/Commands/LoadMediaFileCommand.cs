using MediaPlayerProject.Models;
using MediaPlayerProject.Services;
using MediaPlayerProject.Services.NavigationServiceProvider;
using MediaPlayerProject.Stores;
using MediaPlayerProject.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace MediaPlayerProject.Commands
{
    public class LoadMediaFileCommand : CommandBase
    {
        public LoadMediaFileCommand()
        {
        }

        public override void Execute(object? parameter)
        {
            try
            {
                var pl = parameter as Playlist;
                if (pl != null)
                {
                    var nsp = App.GetService<INavigationServiceProvider>();
                    var ns = nsp.GetNavigationService(() => new MediaFileListingViewModel(pl));
                    ns.Navigate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
