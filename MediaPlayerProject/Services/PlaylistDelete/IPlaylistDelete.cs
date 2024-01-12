using MediaPlayerProject.Models;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.PlaylistDelete
{
    public interface IPlaylistDelete
    {
        Task DeletePlaylist(Playlist playlist);
    }
}
