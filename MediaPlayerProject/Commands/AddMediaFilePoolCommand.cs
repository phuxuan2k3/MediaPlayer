using MediaPlayerProject.Helpers;
using MediaPlayerProject.Models;
using MediaPlayerProject.Services.MediaFileCreator;
using MediaPlayerProject.ViewModels;
using Microsoft.Win32;
using System;
using System.Threading.Tasks;

namespace MediaPlayerProject.Commands
{
    public class AddMediaFilePoolCommand : AsyncCommandBase
    {
        private readonly MediaFilePoolViewModel mediaFilePoolViewModel;

        public AddMediaFilePoolCommand(MediaFilePoolViewModel mediaFilePoolViewModel)
        {
            this.mediaFilePoolViewModel = mediaFilePoolViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Media Files|*.mp4;*.mov;*.wav;*.mp3";
            bool? result = openFileDialog.ShowDialog();

            string[] filenames;
            if (result == true)
            {
                var sv = App.GetService<IMediaFileCreator>();
                filenames = openFileDialog.FileNames;
                foreach (var filename in filenames)
                {
                    var parsedFileName = ParseFileName.parseFileName(filename);
                    await sv.AddMediaFiletoPool(new MediaFile(parsedFileName.Name, parsedFileName.Path, new TimeSpan(0)));
                }

                this.mediaFilePoolViewModel.UpdateMediaFileList();
            }
        }
    }
}
