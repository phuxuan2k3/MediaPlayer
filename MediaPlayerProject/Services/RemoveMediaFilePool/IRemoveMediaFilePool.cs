using MediaPlayerProject.Models;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.RemoveMediaFilePool
{
    public interface IRemoveMediaFilePool
    {
        Task removeMediaFile(MediaFile m);
    }
}
