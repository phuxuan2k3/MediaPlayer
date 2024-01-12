using MediaPlayerProject.DTOs;
using MediaPlayerProject.Models;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.MediaFileCreator
{
    public interface IMediaFileCreator
    {
        Task<MediaFileDTO> AddMediaFiletoPool(MediaFile m);
        Task<bool> AddMediaFiletoPlaylist(Playlist pl, MediaFile m);
    }
}
