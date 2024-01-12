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

        public MediaFile(string fileName, string path, Guid id)
        {
            FileName = fileName;
            Path = path;
            Id = id;
        }

        public string FileName { get; set; }
        public string Path { get; set; }

        public Guid Id;
    }
}
