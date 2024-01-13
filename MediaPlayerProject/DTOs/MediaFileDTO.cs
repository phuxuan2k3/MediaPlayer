using System;
using System.ComponentModel.DataAnnotations;

namespace MediaPlayerProject.DTOs
{
    public class MediaFileDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public TimeSpan StartTime { get; set; }
    }
}