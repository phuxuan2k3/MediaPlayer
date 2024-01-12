using MediaPlayerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.MediaFileProviders
{
    public interface IMediaFileProvider
    {
        Task<IEnumerable<MediaFile>> GetMediaFiles(Playlist playlist);

    }
}
