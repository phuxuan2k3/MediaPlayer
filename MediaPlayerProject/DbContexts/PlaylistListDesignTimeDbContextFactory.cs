using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
