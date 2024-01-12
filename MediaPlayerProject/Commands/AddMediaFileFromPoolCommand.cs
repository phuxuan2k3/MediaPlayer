using MediaPlayerProject.Helpers;
using MediaPlayerProject.Models;
using MediaPlayerProject.Services.MediaFileCreator;
using MediaPlayerProject.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Commands
{
    public class AddMediaFileFromPoolCommand : AsyncCommandBase
    {
        private readonly Playlist playlist;
        private readonly MediaFileListingViewModel mediaFileListingViewModel;

        public AddMediaFileFromPoolCommand(Playlist playlist, MediaFileListingViewModel mediaFileListingViewModel)
        {
            this.playlist = playlist;
            this.mediaFileListingViewModel = mediaFileListingViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            System.Collections.IList items = (System.Collections.IList)parameter!;
            var selectedFiles = items.Cast<MediaFile>();

            var sv = App.GetService<IMediaFileCreator>();
            foreach (var file in selectedFiles)
            {
                var mpl = await sv.AddMediaFiletoPlaylist(this.playlist, file);
            }

            this.mediaFileListingViewModel.UpdateViewModel();
        }
    }
}
