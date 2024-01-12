using MediaPlayerProject.Services.MediaFileCreator;
using MediaPlayerProject.Services.MediaFIlePoolProvider;
using MediaPlayerProject.Services.MediaFileProviders;
using MediaPlayerProject.Services.PlaylistCreators;
using MediaPlayerProject.Services.PlaylistDelete;

using MediaPlayerProject.Services.PlaylistProviders;
using MediaPlayerProject.Services.RemoveMediaFile;
using MediaPlayerProject.Services.RemoveMediaFilePool;
using System.Collections.Generic;
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
        private readonly IRemoveMediaFile removeMediaFile;
        private readonly IMediaFIlePoolProvider  mediaFIlePoolProvider;
        private readonly IMediaFileCreator mediaFilePoolCreator;
        private readonly IRemoveMediaFilePool removeMediaFilePool;

        public PlaylistList()
        {
            this.playlistCreators = App.GetService<IPlaylistCreators>();
            this.playlistProvider = App.GetService<IPlaylistProvider>();
            this.playlistDeletor = App.GetService<IPlaylistDelete>();
        }
        public async Task<IEnumerable<Playlist>> GetItems()
        {
            return await playlistProvider.GetAllPlaylist();
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
