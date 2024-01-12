using MediaPlayerProject.Services.MediaFileCreators;
using MediaPlayerProject.Services.MediaFileProviders;
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
        private readonly IMediaFileProvider mediaFileProvider;
        private readonly IMediaFileCreator mediaFileCreator;

        public PlaylistList(IPlaylistCreators playlistCreators, IPlaylistProvider playlistProvider, IPlaylistDelete playlistDeletor, IMediaFileProvider mediaFileProvider, IMediaFileCreator mediaFileCreator)
        {
            this.playlistCreators = playlistCreators;
            this.playlistProvider = playlistProvider;
            this.playlistDeletor = playlistDeletor;
            this.mediaFileProvider = mediaFileProvider;
            this.mediaFileCreator = mediaFileCreator;
        }
        public async Task<IEnumerable<Playlist>> GetItems()
        {
            return await playlistProvider.GetAllPlaylist(mediaFileProvider, mediaFileCreator);
        }

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
