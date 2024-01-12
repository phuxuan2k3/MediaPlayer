using MediaPlayerProject.Services.MediaFileCreators;
using MediaPlayerProject.Services.MediaFileProviders;
using MediaPlayerProject.Services.RemoveMediaFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Models
{
    public class Playlist
    {
        public Playlist()
        {
            Files = new List<MediaFile>();
        }
        public Playlist(string name, DateTime timeCreated)
        {
            Name = name;
            TimeCreated = timeCreated;
            this.Files = new List<MediaFile>();
        }
        public Playlist(string name, DateTime timeCreated, Guid id)
        {
            Name = name;
            TimeCreated = timeCreated;
            this.Files = new List<MediaFile>();
            this.Id = id;
        }
        public Playlist(string name, DateTime timeCreated, Guid id, IMediaFileProvider mediaFileProvider, IMediaFileCreator mediaFileCreator, IRemoveMediaFile mediaFileRemover)
        {
            Name = name;
            TimeCreated = timeCreated;
            this.Files = new List<MediaFile>();
            this.Id = id;
            this.mediaFileProvider = mediaFileProvider;
            this.mediaFileCreator = mediaFileCreator;
            this.mediaFileRemover = mediaFileRemover;
        }

        public string Name { get; }
        public DateTime TimeCreated { get; }
        public Guid Id { get; }

        public List<MediaFile> Files { get; }
        private readonly IMediaFileProvider mediaFileProvider;
        private readonly IMediaFileCreator mediaFileCreator;
        private readonly IRemoveMediaFile mediaFileRemover;
        public async Task<IEnumerable<MediaFile>> GetItems()
        {
            return await mediaFileProvider.GetMediaFiles(this);
        }
        public async Task CreateMediaFile(MediaFile mediaFile)
        {
            await mediaFileCreator.createMediaFile(this, mediaFile);
        }
        public async Task RemoveMediaFile(MediaFile mediaFile)
        {
            await mediaFileRemover.removeMediaFile(this, mediaFile);
        }
    }
}
