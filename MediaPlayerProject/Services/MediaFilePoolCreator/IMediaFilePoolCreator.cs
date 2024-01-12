using MediaPlayerProject.Models;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.MediaFilePoolCreator
{
    public interface IMediaFilePoolCreator
    {
        Task addMediaFiletoPool(MediaFile m);
    }
}
