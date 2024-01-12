using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MediaPlayerProject.DbContexts
{
    public class PlaylistListDesignTimeDbContextFactory : IDesignTimeDbContextFactory<PlaylistListDbContext>
    {
        public PlaylistListDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=PlaylistList.db").Options;
            return new PlaylistListDbContext(options);
        }
    }
}
