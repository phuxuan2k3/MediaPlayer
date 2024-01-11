using MediaPlayerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.PlaylistCreators
{
    public interface IPlaylistCreators
    {
        Task CreatePlaylist(Playlist playlist);
    }
}
