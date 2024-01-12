using MediaPlayerProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.PlaylistProviders
{
    public interface IPlaylistProvider
    {
        Task<IEnumerable<Playlist>> GetAllPlaylist();
    }
}
