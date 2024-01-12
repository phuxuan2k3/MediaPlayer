using MediaPlayerProject.Models;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.RemoveMediaFile
{
    public interface IRemoveMediaFile
    {
        Task removeMediaFile(Playlist p, MediaFile m);
    }
}
