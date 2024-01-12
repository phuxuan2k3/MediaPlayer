using MediaPlayerProject.Services.MediaFileCreators;
using MediaPlayerProject.Services.MediaFilePoolCreator;
using MediaPlayerProject.Services.MediaFIlePoolProvider;
using MediaPlayerProject.Services.MediaFileProviders;
using MediaPlayerProject.Services.PlaylistCreators;
using MediaPlayerProject.Services.PlaylistDelete;
using MediaPlayerProject.Services.PlaylistProviders;
using MediaPlayerProject.Services.RemoveMediaFile;
using MediaPlayerProject.Services.RemoveMediaFilePool;
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
        private readonly IRemoveMediaFile removeMediaFile;
        private readonly IMediaFIlePoolProvider  mediaFIlePoolProvider;
        private readonly IMediaFilePoolCreator mediaFilePoolCreator;
        private readonly IRemoveMediaFilePool removeMediaFilePool;

        public PlaylistList(IPlaylistCreators playlistCreators, IPlaylistProvider playlistProvider, IPlaylistDelete playlistDeletor, IMediaFileProvider mediaFileProvider, IMediaFileCreator mediaFileCreator, IRemoveMediaFile removeMediaFile, IMediaFIlePoolProvider mediaFIlePoolProvider, IMediaFilePoolCreator mediaFilePoolCreator, IRemoveMediaFilePool removeMediaFilePool)
        {
            this.playlistCreators = playlistCreators;
            this.playlistProvider = playlistProvider;
            this.playlistDeletor = playlistDeletor;
            this.mediaFileProvider = mediaFileProvider;
            this.mediaFileCreator = mediaFileCreator;
            this.removeMediaFile = removeMediaFile;
            this.mediaFIlePoolProvider = mediaFIlePoolProvider;
            this.mediaFilePoolCreator = mediaFilePoolCreator;
            this.removeMediaFilePool = removeMediaFilePool;
        }
        public async Task<IEnumerable<Playlist>> GetItems()
        {
            return await playlistProvider.GetAllPlaylist(mediaFileProvider, mediaFileCreator, removeMediaFile);
        }

        public async Task addPlaylist(Playlist playlist)
        {
            await playlistCreators.CreatePlaylist(playlist);
        }
        public async Task deletePlaylist(Playlist playlist)
        {
            await playlistDeletor.DeletePlaylist(playlist);
        } 
        public async Task<IEnumerable<MediaFile>> GetMediaFilePool()
        {
            return await mediaFIlePoolProvider.getMediaFIlePool();
        }  
        public async Task addMediaFilePool(MediaFile m)
        {
             await mediaFilePoolCreator.addMediaFiletoPool( m);
        }     
        public async Task removeMediaPool(MediaFile m)
        {
             await removeMediaFilePool.removeMediaFile( m);
        }

    }
}
