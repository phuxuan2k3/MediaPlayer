using MediaPlayerProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.MediaFileProviders
{
    public interface IMediaFileProvider
    {
        Task<IEnumerable<MediaFile>> GetMediaFiles(Playlist playlist);

    }
}
