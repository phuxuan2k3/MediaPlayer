using MediaPlayerProject.Services.PlaylistCreators;
using MediaPlayerProject.Services.PlaylistDelete;
using MediaPlayerProject.Services.PlaylistProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Models
{

    public class PlaylistList
    {
        private readonly IPlaylistCreators playlistCreators;
        private readonly IPlaylistProvider playlistProvider;
        private readonly IPlaylistDelete playlistDeletor;

        public PlaylistList(IPlaylistCreators playlistCreators, IPlaylistProvider playlistProvider, IPlaylistDelete playlistDeletor)
        {
            this.playlistCreators = playlistCreators;
            this.playlistProvider = playlistProvider;
            this.playlistDeletor = playlistDeletor;
        }
        public async Task<IEnumerable<Playlist>> GetItems()
        {
            return await playlistProvider.GetAllPlaylist();
        }
        //public IEnumerable<Playlist> GetItemsByName(string name)
        //{
        //    return Playlists.Where(x => x.Name == name);
        //}
        public async Task addPlaylist(Playlist playlist)
        {
            await playlistCreators.CreatePlaylist(playlist);
        }
        public async Task deletePlaylist(Playlist playlist)
        {
            await playlistDeletor.DeletePlaylist(playlist);
        }
    }
}
