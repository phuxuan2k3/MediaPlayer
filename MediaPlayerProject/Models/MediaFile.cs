using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Models
{
    public class MediaFile
    {
        public MediaFile() { }

        public MediaFile(string fileName, string path)
        {
            FileName = fileName;
            Path = path;
        }

        public string FileName;
        public string Path;

    }
}
