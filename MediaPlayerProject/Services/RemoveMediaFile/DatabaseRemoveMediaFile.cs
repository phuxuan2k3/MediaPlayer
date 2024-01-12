using MediaPlayerProject.DbContexts;
using MediaPlayerProject.DTOs;
using MediaPlayerProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.RemoveMediaFile
{
    public class DatabaseRemoveMediaFile : IRemoveMediaFile
    {
        private readonly PlaylistListDbContextFactory _playlistListDbContextFactory;

        public DatabaseRemoveMediaFile(PlaylistListDbContextFactory playlistListDbContextFactory)
        {
            _playlistListDbContextFactory = playlistListDbContextFactory;
        }
        public async Task removeMediaFile(Playlist p, MediaFile m)
        {
            using (PlaylistListDbContext dbContext = _playlistListDbContextFactory.CreateDbContext())
            {
                dbContext.PlaylistFiles.Remove(toPlaylistFileDTO(p, m));
                if (await checkIfAnyPlaylistContainsMediaFile(m) == false)
                {
                    dbContext.MediaFiles.Remove(toMediaFileDTO(m));
                }
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task<bool> checkIfAnyPlaylistContainsMediaFile(MediaFile m)
        {
            using (PlaylistListDbContext dbContext = _playlistListDbContextFactory.CreateDbContext())
            {
                var numOfRecord = await dbContext.PlaylistFiles.CountAsync(r => r.FileId == m.Id);
                if (numOfRecord > 0)
                {
                    return true;
                }
            }
            return false;
        }
        public PlaylistFilesDTO toPlaylistFileDTO(Playlist p, MediaFile m)
        {
            return new PlaylistFilesDTO()
            {
                PlaylistId = p.Id,
                FileId = m.Id,
            };
        }
        public MediaFileDTO toMediaFileDTO(MediaFile m)
        {
            return new MediaFileDTO()
            {
                Path = m.Path,
                Name = m.FileName,
                Id = m.Id
            };
        }
    }
}
