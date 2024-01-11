using MediaPlayerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.PlaylistDelete
{
    public interface IPlaylistDelete
    {
        Task DeletePlaylist(Playlist playlist);
    }
}
