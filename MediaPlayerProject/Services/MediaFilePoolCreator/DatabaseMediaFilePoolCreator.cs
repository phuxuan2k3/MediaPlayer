using MediaPlayerProject.DbContexts;
using MediaPlayerProject.DTOs;
using MediaPlayerProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.MediaFilePoolCreator
{
    public class DatabaseMediaFilePoolCreator : IMediaFilePoolCreator
    {
        private readonly PlaylistListDbContextFactory _playlistListDbContextFactory;

        public DatabaseMediaFilePoolCreator(PlaylistListDbContextFactory playlistListDbContextFactory)
        {
            _playlistListDbContextFactory = playlistListDbContextFactory;
        }


        public MediaFileDTO toMediaFileDTO(MediaFile m)
        {
            return new MediaFileDTO()
            {
                Path = m.Path,
                Name = m.FileName
            };
        }
        public async Task addMediaFiletoPool(MediaFile m)
        {
            using (PlaylistListDbContext dbContext = _playlistListDbContextFactory.CreateDbContext())
            {
                var res = await dbContext.MediaFiles.AddAsync(toMediaFileDTO(m));
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
