using MediaPlayerProject.DbContexts;
using MediaPlayerProject.DTOs;
using MediaPlayerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.SaveMediaFileTimeSpan
{
    public class SaveMediaFileTimeSpan : ISaveMediaFileTimeSpan
    {
        private readonly PlaylistListDbContextFactory _playlistListDbContextFactory;

        public SaveMediaFileTimeSpan(PlaylistListDbContextFactory playlistListDbContextFactory)
        {
            _playlistListDbContextFactory = playlistListDbContextFactory;
        }
        public MediaFileDTO toMediaFileDTO(MediaFile m)
        {
            return new MediaFileDTO()
            {
                Path = m.Path,
                Name = m.FileName,
                Id = m.Id,
                StartTime = m.StartTime,
            };
        }

        public async Task Save(MediaFile m)
        {
            using (PlaylistListDbContext dbContext = _playlistListDbContextFactory.CreateDbContext())
            {
                dbContext.MediaFiles.Update(toMediaFileDTO(m));
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
