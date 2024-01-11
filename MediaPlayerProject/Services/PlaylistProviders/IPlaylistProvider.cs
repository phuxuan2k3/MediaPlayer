using MediaPlayerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.PlaylistProviders
{
    public interface IPlaylistProvider
    {
        Task<IEnumerable<Playlist>> GetAllPlaylist();
    }
}
