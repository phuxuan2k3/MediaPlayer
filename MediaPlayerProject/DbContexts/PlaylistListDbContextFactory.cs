using Microsoft.EntityFrameworkCore;

namespace MediaPlayerProject.DbContexts
{
    public class PlaylistListDbContextFactory
    {
        private readonly string? _connectionString;

        public PlaylistListDbContextFactory(string? connectionString)
        {
            _connectionString = connectionString;
        }

        public PlaylistListDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;
            return new PlaylistListDbContext(options);
        }
    }
}
