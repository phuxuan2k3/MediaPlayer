using MediaPlayerProject.DbContexts;
using MediaPlayerProject.DTOs;
using MediaPlayerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.PlaylistCreators
{
    public class DatabasePlaylistCreator : IPlaylistCreators
    {
        private readonly PlaylistListDbContextFactory _playlistListDbContextFactory;

        public DatabasePlaylistCreator(PlaylistListDbContextFactory playlistListDbContextFactory)
        {
            _playlistListDbContextFactory = playlistListDbContextFactory;
        }

        public async Task CreatePlaylist(Playlist playlist)
        {
            using (PlaylistListDbContext dbContext = _playlistListDbContextFactory.CreateDbContext())
            {
                PlaylistDTO playlistDTO = ToPlaylistDTO(playlist);
                dbContext.Playlists.Add(playlistDTO);
                await dbContext.SaveChangesAsync();
            }
        }

        private PlaylistDTO ToPlaylistDTO(Playlist playlist)
        {
            return new PlaylistDTO()
            {
                Name = playlist.Name,
                TimeCreated = playlist.TimeCreated
            };
        }
    }
}
