using MediaPlayerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.SaveMediaFileTimeSpan
{
    public interface ISaveMediaFileTimeSpan
    {
        Task Save(MediaFile m);
    }
}
