using MediaPlayerProject.DbContexts;
using MediaPlayerProject.DTOs;
using MediaPlayerProject.Models;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.PlaylistDelete
{
    public class DatabasePlaylistDelete : IPlaylistDelete
    {
        private readonly PlaylistListDbContextFactory _playlistListDbContextFactory;

        public DatabasePlaylistDelete(PlaylistListDbContextFactory playlistListDbContextFactory)
        {
            _playlistListDbContextFactory = playlistListDbContextFactory;
        }

        public async Task DeletePlaylist(Playlist playlist)
        {
            using (PlaylistListDbContext dbContext = _playlistListDbContextFactory.CreateDbContext())
            {
                PlaylistDTO playlistDTO = ToPlaylistDTO(playlist);
                dbContext.Playlists.Remove(playlistDTO);
                await dbContext.SaveChangesAsync();
            }
        }

        private PlaylistDTO ToPlaylistDTO(Playlist playlist)
        {
            return new PlaylistDTO()
            {
                Id = playlist.Id,
                Name = playlist.Name,
                TimeCreated = playlist.TimeCreated
            };
        }
    }
}