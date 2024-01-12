using MediaPlayerProject.Models;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.MediaFileCreators
{
    public interface IMediaFileCreator
    {
        Task createMediaFile(Playlist playlist, MediaFile mediaFile);
    }
}
