using MediaPlayerProject.DbContexts;
using MediaPlayerProject.DTOs;
using MediaPlayerProject.Models;
using System;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.MediaFileCreators
{
    public class DatabaseMediaFileCreator : IMediaFileCreator
    {
        private readonly PlaylistListDbContextFactory _playlistListDbContextFactory;

        public DatabaseMediaFileCreator(PlaylistListDbContextFactory playlistListDbContextFactory)
        {
            _playlistListDbContextFactory = playlistListDbContextFactory;
        }

        public async Task createMediaFile(Playlist playlist, MediaFile m)
        {
            using (PlaylistListDbContext dbContext = _playlistListDbContextFactory.CreateDbContext())
            {
                var mediaFIleDTO = ToMediaFIleDTO(m);
                var newMediaFile = await dbContext.MediaFiles.AddAsync(mediaFIleDTO);
                var mediaFileId = newMediaFile.Entity.Id;
                await dbContext.PlaylistFiles.AddAsync(createPlaylistFilesDTO(playlist.Id, mediaFileId));
                await dbContext.SaveChangesAsync();
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
                Path = m.Path
            };
            return mediaFileDTO;
        }
    }
}
