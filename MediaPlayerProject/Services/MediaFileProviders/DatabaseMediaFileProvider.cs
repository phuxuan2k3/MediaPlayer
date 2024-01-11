using MediaPlayerProject.DbContexts;
using MediaPlayerProject.DTOs;
using MediaPlayerProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.MediaFileProviders
{
    public class DatabaseMediaFileProvider : IMediaFileProvider
    {
        private readonly PlaylistListDbContextFactory _playlistListDbContextFactory;

        public DatabaseMediaFileProvider(PlaylistListDbContextFactory playlistListDbContextFactory)
        {
            _playlistListDbContextFactory = playlistListDbContextFactory;
        }

        public async Task<IEnumerable<MediaFile>> GetMediaFiles(Playlist playlist)
        {
            using (PlaylistListDbContext playlistListDbContext = _playlistListDbContextFactory.CreateDbContext())
            {
                IEnumerable<MediaFileDTO> mediaFileDTOs = await playlistListDbContext.MediaFiles.Where(m => m.Id == playlist.Id).ToListAsync();
                return mediaFileDTOs.Select(p => new MediaFile(p.Name, p.Path, p.Id));
            }
        }
    }
}
