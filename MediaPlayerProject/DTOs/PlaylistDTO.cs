using System;
using System.ComponentModel.DataAnnotations;

namespace MediaPlayerProject.DTOs
{
    public class PlaylistDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeCreated { get; set; }

    }
}
