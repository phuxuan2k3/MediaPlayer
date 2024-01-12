using MediaPlayerProject.Models;
using MediaPlayerProject.Services;
using MediaPlayerProject.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Commands
{
    public class AddMediaFilesCommand : AsyncCommandBase
    {
        private readonly Playlist playlist;
        private readonly MediaFileListingViewModel mediaFileListingViewModel;

        public AddMediaFilesCommand(Playlist playlist, MediaFileListingViewModel mediaFileListingViewModel)
        {
            this.playlist = playlist;
            this.mediaFileListingViewModel = mediaFileListingViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Media Files|*.mp4;*.mov;*.wav;*.mp3";
            Nullable<bool> result = openFileDialog.ShowDialog();

            string[] filenames;
            if (result == true)
            {
                filenames = openFileDialog.FileNames;
                foreach (var filename in filenames)
                {
                    await this.playlist.CreateMediaFile(new MediaFile(filename, filename));
                }

                this.mediaFileListingViewModel.UpdateMediaFileList();
            }

        }
    }
}
