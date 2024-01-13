using MediaPlayerProject.DbContexts;
using MediaPlayerProject.DTOs;
using MediaPlayerProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.MediaFileCreator
{
    public class DatabaseMediaFileCreator : IMediaFileCreator
    {
        private readonly PlaylistListDbContextFactory _playlistListDbContextFactory;

        public DatabaseMediaFileCreator(PlaylistListDbContextFactory playlistListDbContextFactory)
        {
            _playlistListDbContextFactory = playlistListDbContextFactory;
        }


        public MediaFileDTO toMediaFileDTO(MediaFile m)
        {
            return new MediaFileDTO()
            {
                Path = m.Path,
                Name = m.FileName,
                StartTime = m.StartTime
            };
        }
        public async Task<MediaFileDTO> AddMediaFiletoPool(MediaFile m)
        {
            using (PlaylistListDbContext dbContext = _playlistListDbContextFactory.CreateDbContext())
            {
                var res = await dbContext.MediaFiles.AddAsync(toMediaFileDTO(m));
                await dbContext.SaveChangesAsync();
                return res.Entity;
            }
        }
        public async Task<bool> AddMediaFiletoPlaylist(Playlist pl, MediaFile m)
        {
            using (PlaylistListDbContext dbContext = _playlistListDbContextFactory.CreateDbContext())
            {
                var checkM = await dbContext.MediaFiles.AnyAsync(b => b.Id == m.Id);
                if (checkM)
                {
                    var checkMPl = await dbContext.PlaylistFiles.AnyAsync(b => b.FileId == m.Id && b.PlaylistId == pl.Id);
                    if (checkMPl)
                    {
                        return false;
                    }
                    else
                    {
                        var res = await dbContext.PlaylistFiles.AddAsync(new PlaylistFilesDTO() { FileId = m.Id, PlaylistId = pl.Id });
                        await dbContext.SaveChangesAsync();
                        return true;
                    }
                }
                else
                {
                    throw new Exception();
                }

            }
        }
    }
}
