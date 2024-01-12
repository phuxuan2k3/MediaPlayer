using MediaPlayerProject.Models;
using MediaPlayerProject.Services.MediaFileCreators;
using MediaPlayerProject.Services.MediaFileProviders;
using MediaPlayerProject.Services.RemoveMediaFile;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.PlaylistProviders
{
    public interface IPlaylistProvider
    {
        Task<IEnumerable<Playlist>> GetAllPlaylist(IMediaFileProvider mediaFileProvider, IMediaFileCreator mediaFileCreator, IRemoveMediaFile removeMediaFile);
    }
}
