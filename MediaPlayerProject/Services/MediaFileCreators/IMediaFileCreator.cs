using MediaPlayerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.MediaFileCreators
{
    public interface IMediaFileCreator
    {
        Task createMediaFile(Playlist playlist, MediaFile mediaFile);
    }
}
