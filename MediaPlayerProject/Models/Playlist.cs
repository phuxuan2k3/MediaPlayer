using MediaPlayerProject.Services.MediaFileProviders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaPlayerProject.Models
{
    public class Playlist
    {
        public Playlist(string name, DateTime timeCreated)
        {
            Name = name;
            TimeCreated = timeCreated;
        }
        public Playlist(string name, DateTime timeCreated, Guid id)
        {
            Name = name;
            TimeCreated = timeCreated;
            this.Id = id;
        }

        public string Name { get; }
        public DateTime TimeCreated { get; }
        public Guid Id { get; }

        public async Task<IEnumerable<MediaFile>> getFiles()
        {
            var sv = App.GetService<IMediaFileProvider>();
            var mediaFiles = await sv.GetMediaFiles(this);
            return mediaFiles;
        }
    }
}
