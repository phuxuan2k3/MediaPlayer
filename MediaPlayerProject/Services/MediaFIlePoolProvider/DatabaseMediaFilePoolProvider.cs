﻿using MediaPlayerProject.DbContexts;
using MediaPlayerProject.DTOs;
using MediaPlayerProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.MediaFIlePoolProvider
{
    public class DatabaseMediaFilePoolProvider : IMediaFIlePoolProvider
    {
        private readonly PlaylistListDbContextFactory _playlistListDbContextFactory;

        public DatabaseMediaFilePoolProvider(PlaylistListDbContextFactory playlistListDbContextFactory)
        {
            _playlistListDbContextFactory = playlistListDbContextFactory;
        }

        public async Task<IEnumerable<MediaFile>> getMediaFIlePool()
        {
            using (PlaylistListDbContext dbContext = _playlistListDbContextFactory.CreateDbContext())
            {
                return (await dbContext.MediaFiles.ToListAsync()).Select(m => new MediaFile(m.Name, m.Path, m.Id, m.StartTime));
            }
        }

        PlaylistFilesDTO createPlaylistFilesDTO(Guid playlistId, Guid fileId)
        {
            return new PlaylistFilesDTO()
            {
                PlaylistId = playlistId,
                FileId = fileId
            };
        }
        MediaFileDTO ToMediaFIleDTO(MediaFile m)
        {
            MediaFileDTO mediaFileDTO = new MediaFileDTO()
            {
                Name = m.FileName,
                Path = m.Path,
                StartTime = m.StartTime
            };
            return mediaFileDTO;
        }
    }
}
