using MediaPlayerProject.Models;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.PlaylistCreators
{
    public interface IPlaylistCreators
    {
        Task CreatePlaylist(Playlist playlist);
    }
}
