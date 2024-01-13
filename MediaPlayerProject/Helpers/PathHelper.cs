using MediaPlayerProject.Models;
using System;

namespace MediaPlayerProject.Helpers
{
    public class FileName
    {
        public string Path { get; set; }
        public string Name { get; set; }
    }

    public class PathHelper
    {
        public static FileName parseFileName(string name)
        {
            var lastIndexOfSlashBack = name.LastIndexOf('\\');
            var nameOfFile = name.Substring(lastIndexOfSlashBack + 1);
            var pathOfFile = name.Substring(0, lastIndexOfSlashBack);
            FileName fileName = new FileName()
            {
                Path = pathOfFile,
                Name = nameOfFile
            };
            return fileName;
        }

        public static Uri fileToUri(MediaFile m)
        {
            return new Uri(m.Path + "\\" + m.FileName);
        }
    }

}
