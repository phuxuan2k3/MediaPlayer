using MediaPlayerProject.DbContexts;
using MediaPlayerProject.DTOs;
using MediaPlayerProject.Helpers;
using MediaPlayerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.HistoryFileProvider
{
    public class HistoryFileProvider : IHistoryFileProvider
    {
        private readonly PlaylistListDbContextFactory _playlistListDbContextFactory;

        public HistoryFileProvider(PlaylistListDbContextFactory playlistListDbContextFactory)
        {
            _playlistListDbContextFactory = playlistListDbContextFactory;
        }


        public MediaFileDTO toMediaFileDTO(MediaFile m)
        {
            return new MediaFileDTO()
            {
                Path = m.Path,
                Name = m.FileName,
                StartTime = m.StartTime
            };
        }

        public IEnumerable<MediaFile> GetHistoryFile()
        {
            var hh = new HistoryHelper();
            var listId = hh.ReadGuidFromTextFile();
            var listFile = new List<MediaFile>();

            using (PlaylistListDbContext dbContext = _playlistListDbContextFactory.CreateDbContext())
            {
                foreach (var item in listId)
                {
                    var fileDTO = dbContext.MediaFiles.SingleOrDefault(f => f.Id == item);
                    listFile.Add(new MediaFile(fileDTO!.Name, fileDTO.Path, fileDTO.StartTime));
                }
            }
            return listFile;
        }
    }
}
