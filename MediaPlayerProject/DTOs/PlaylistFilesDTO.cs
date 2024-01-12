﻿using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaPlayerProject.DTOs
{
    [PrimaryKey(nameof(PlaylistId), nameof(FileId))]
    public class PlaylistFilesDTO
    {

        [ForeignKey("Playlist")]
        public Guid PlaylistId { get; set; }
        public PlaylistDTO? Playlist { get; set; }

        [ForeignKey("MediaFile")]
        public Guid FileId { get; set; }
        public MediaFileDTO? MediaFile { get; set; }
    }
}
