using MediaPlayerProject.Helpers;
using MediaPlayerProject.Models;
using MediaPlayerProject.Services.MediaFileCreator;
using MediaPlayerProject.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaPlayerProject.Commands
{
    public class AddMediaFilesFromPoolCommand : AsyncCommandBase
    {
        private readonly Playlist playlist;
        private readonly MediaFileListingViewModel mediaFileListingViewModel;

        public AddMediaFilesFromPoolCommand(Playlist playlist, MediaFileListingViewModel mediaFileListingViewModel)
        {
            this.mediaFileListingViewModel = mediaFileListingViewModel;
            this.playlist = playlist;
            this.mediaFileListingViewModel = mediaFileListingViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var mfs = parameter as MediaFile[] ?? throw new Exception();
            var sv = App.GetService<IMediaFileCreator>();
            foreach (var mf in mfs)
            {
                var mpl = await sv.AddMediaFiletoPlaylist(this.playlist, mf);
                if (!mpl)
                {
                    throw new Exception();
                }
            }
            this.mediaFileListingViewModel.UpdateMediaFileList();
        }
    }
}

