using MediaPlayerProject.Helpers;
using MediaPlayerProject.Models;
using MediaPlayerProject.Services.MediaFileCreator;
using MediaPlayerProject.ViewModels;
using Microsoft.Win32;
using System.Threading.Tasks;

namespace MediaPlayerProject.Commands
{
    public class AddMediaFilesFromSystemCommand : AsyncCommandBase
    {
        private readonly Playlist playlist;
        private readonly MediaFileListingViewModel mediaFileListingViewModel;

        public AddMediaFilesFromSystemCommand(Playlist playlist, MediaFileListingViewModel mediaFileListingViewModel)
        {
            this.playlist = playlist;
            this.mediaFileListingViewModel = mediaFileListingViewModel;
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
                filenames = openFileDialog.FileNames;
                var sv = App.GetService<IMediaFileCreator>();
                foreach (var filename in filenames)
                {
                    var parsedFileName = ParseFileName.parseFileName(filename);
                    var m = await sv.AddMediaFiletoPool(new MediaFile(parsedFileName.Name, parsedFileName.Path));
                    var mpl = await sv.AddMediaFiletoPlaylist(this.playlist, new MediaFile(m.Name, m.Path, m.Id));
                }

                this.mediaFileListingViewModel.UpdateMediaFileList();
            }

        }
    }
}
