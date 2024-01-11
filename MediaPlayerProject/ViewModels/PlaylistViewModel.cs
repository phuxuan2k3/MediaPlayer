using MediaPlayerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.ViewModels
{

    public class PlaylistViewModel : ViewModelBase
    {
        private readonly Playlist _playlist;
        public string Name => _playlist.Name;
        public Guid Id => _playlist.Id;
        //public string TimeCreated => _playlist.TimeCreated.ToString("g");
        public DateTime TimeCreated => _playlist.TimeCreated;
        public PlaylistViewModel(Playlist playlist)
        {
            _playlist = playlist;
        }
    }
}
