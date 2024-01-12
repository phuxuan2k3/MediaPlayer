using MediaPlayerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.RemoveMediaFile
{
    public interface IRemoveMediaFile
    {
        Task removeMediaFile(Playlist p, MediaFile m);
    }
}
