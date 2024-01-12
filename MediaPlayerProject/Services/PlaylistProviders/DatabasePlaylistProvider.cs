﻿using MediaPlayerProject.DbContexts;
using MediaPlayerProject.DTOs;
using MediaPlayerProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.PlaylistProviders
{
    public class DatabasePlaylistProvider : IPlaylistProvider
    {
        private readonly PlaylistListDbContextFactory _playlistListDbContextFactory;

        public DatabasePlaylistProvider(PlaylistListDbContextFactory playlistListDbContextFactory)
        {
            _playlistListDbContextFactory = playlistListDbContextFactory;
        }

        public async Task<IEnumerable<Playlist>> GetAllPlaylist(MediaFileProviders.IMediaFileProvider mediaFileProvider)
        {
            using (PlaylistListDbContext playlistListDbContext = _playlistListDbContextFactory.CreateDbContext())
            {
                IEnumerable<PlaylistDTO> playlistDTOs = await playlistListDbContext.Playlists.ToListAsync();
                return playlistDTOs.Select(p => new Playlist(p.Name, p.TimeCreated, p.Id, mediaFileProvider));
            }
        }
    }
}
