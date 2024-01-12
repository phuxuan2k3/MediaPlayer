using MediaPlayerProject.Models;
using System;

namespace MediaPlayerProject.ViewModels
{
    public class MediaFileViewModel
    {
        private readonly MediaFile mediaFile;
        public string FileName => mediaFile.FileName;
        public Guid Id => mediaFile.Id;
        public string Path => mediaFile.Path;
        public MediaFileViewModel(MediaFile mediaFile)
        {
            this.mediaFile = mediaFile;
        }
    }
}
