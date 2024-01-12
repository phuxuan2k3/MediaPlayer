using MediaPlayerProject.Services.RemoveMediaFile;
using MediaPlayerProject.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace MediaPlayerProject.Commands
{
    public class RemoveMediaFileCommand : AsyncCommandBase
    {
        private readonly MediaFileListingViewModel mediaFileListingViewModel;

        public RemoveMediaFileCommand(MediaFileListingViewModel mediaFileListingViewModel)
        {
            this.mediaFileListingViewModel = mediaFileListingViewModel;
            mediaFileListingViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MediaFileListingViewModel.SelectedMediaFile))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (mediaFileListingViewModel.SelectedMediaFile != null && base.CanExecute(parameter));
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                var sv = App.GetService<IRemoveMediaFile>();
                await sv.removeMediaFile(mediaFileListingViewModel.PlaylistData, mediaFileListingViewModel.SelectedMediaFile!);
                mediaFileListingViewModel.UpdateViewModel();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
