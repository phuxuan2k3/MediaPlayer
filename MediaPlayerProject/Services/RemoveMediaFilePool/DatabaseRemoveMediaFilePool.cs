using MediaPlayerProject.DbContexts;
using MediaPlayerProject.DTOs;
using MediaPlayerProject.Models;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.RemoveMediaFilePool
{
    public class DatabaseRemoveMediaFilePool : IRemoveMediaFilePool
    {
        private readonly PlaylistListDbContextFactory _playlistListDbContextFactory;

        public DatabaseRemoveMediaFilePool(PlaylistListDbContextFactory playlistListDbContextFactory)
        {
            _playlistListDbContextFactory = playlistListDbContextFactory;
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

        public async Task removeMediaFile(MediaFile m)
        {
            using (PlaylistListDbContext dbContext = _playlistListDbContextFactory.CreateDbContext())
            {
                dbContext.MediaFiles.Remove(toMediaFileDTO(m));
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
