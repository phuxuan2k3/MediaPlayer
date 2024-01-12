using MediaPlayerProject.DbContexts;
using MediaPlayerProject.DTOs;
using MediaPlayerProject.Models;
using MediaPlayerProject.Services.MediaFileCreators;
using MediaPlayerProject.Services.MediaFileProviders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.PlaylistProviders
{
    public class DatabasePlaylistProvider : IPlaylistProvider
    {
        private readonly PlaylistListDbContextFactory _playlistListDbContextFactory;

        public DatabasePlaylistProvider(PlaylistListDbContextFactory playlistListDbContextFactory)
        {
            _playlistListDbContextFactory = playlistListDbContextFactory;
        }

        public async Task<IEnumerable<Playlist>> GetAllPlaylist(IMediaFileProvider mediaFileProvider, IMediaFileCreator mediaFileCreator, RemoveMediaFile.IRemoveMediaFile mediaFileRemover)
        {
            using (PlaylistListDbContext playlistListDbContext = _playlistListDbContextFactory.CreateDbContext())
            {
                IEnumerable<PlaylistDTO> playlistDTOs = await playlistListDbContext.Playlists.ToListAsync();
                return playlistDTOs.Select(p => new Playlist(p.Name, p.TimeCreated, p.Id, mediaFileProvider, mediaFileCreator, mediaFileRemover));
            }
        }
    }
}
