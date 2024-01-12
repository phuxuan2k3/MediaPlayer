using MediaPlayerProject.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediaPlayerProject.DbContexts
{
    public class PlaylistListDbContext : DbContext
    {
        public PlaylistListDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PlaylistDTO> Playlists { get; set; }
        public DbSet<MediaFileDTO> MediaFiles { get; set; }
        public DbSet<PlaylistFilesDTO> PlaylistFiles { get; set; }
    }
}
