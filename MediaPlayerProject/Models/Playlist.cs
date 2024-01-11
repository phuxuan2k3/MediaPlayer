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

        public string Name { get; }
        public DateTime TimeCreated { get; }
        public Guid Id { get; }

        public List<MediaFile> Files { get; }
        public IEnumerable<MediaFile> GetItems()
        {
            return Files;
        }
    }
}
