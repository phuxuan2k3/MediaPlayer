using MediaPlayerProject.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediaPlayerProject.Commands
{
    public class RemoveMediaPoolCommand : AsyncCommandBase
    {
        private readonly MediaFilePoolViewModel mediaFilePoolViewModel;

        public RemoveMediaPoolCommand(MediaFilePoolViewModel MediaFilePoolViewModel)
        {
            this.mediaFilePoolViewModel = MediaFilePoolViewModel;
            mediaFilePoolViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MediaFilePoolViewModel.SelectedMediaFile))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (mediaFilePoolViewModel.SelectedMediaFile != null && base.CanExecute(parameter));
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await mediaFilePoolViewModel.App.removeMediaPool(mediaFilePoolViewModel.SelectedMediaFile);
                mediaFilePoolViewModel.UpdateMediaFileList();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
