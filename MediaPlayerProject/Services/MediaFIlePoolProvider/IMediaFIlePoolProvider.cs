using MediaPlayerProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.MediaFIlePoolProvider
{
    public interface IMediaFIlePoolProvider
    {
        Task<IEnumerable<MediaFile>> getMediaFIlePool();
    }
}
