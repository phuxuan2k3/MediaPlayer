using MediaPlayerProject.Models;
using MediaPlayerProject.Services.NavigationServiceProvider;
using MediaPlayerProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediaPlayerProject.Commands
{
    public class PlayFileCommand : CommandBase
    {
        public PlayFileCommand() { }
        public override async void Execute(object? parameter)
        {
            try
            {
                var pl = parameter as Playlist;
                if (pl != null)
                {
                    if ((await pl.getFiles()).Any())
                    {
                        var nsp = App.GetService<INavigationServiceProvider>();
                        var ns = nsp.GetNavigationService(() => new PlayFileViewModel(pl));
                        ns.Navigate();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
