using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace MediaPlayerProject.DTOs
{
    public class MediaFileDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

    }
}