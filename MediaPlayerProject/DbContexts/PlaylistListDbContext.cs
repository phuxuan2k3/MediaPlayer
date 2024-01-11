using MediaPlayerProject.DTOs;
using MediaPlayerProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.DbContexts
{
    public  class PlaylistListDbContext : DbContext
    {
        public PlaylistListDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PlaylistDTO> Playlists { get; set; }
    }
}
