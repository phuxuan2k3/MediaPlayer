﻿using MediaPlayerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Services.HistoryFileProvider
{
    public interface IHistoryFileProvider
    {
        IEnumerable<MediaFile> GetHistoryFile();
    }
}
