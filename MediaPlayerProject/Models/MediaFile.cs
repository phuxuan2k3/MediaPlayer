using System;

namespace MediaPlayerProject.Models
{
    public class MediaFile
    {
        public MediaFile() { }

        public MediaFile(string fileName, string path, TimeSpan startTime)
        {
            FileName = fileName;
            Path = path;
            StartTime = startTime;
        }

        public MediaFile(string fileName, string path, Guid id, TimeSpan startTime)
        {
            FileName = fileName;
            Path = path;
            Id = id;
            StartTime = startTime;
        }

        public string FileName { get; set; }
        public string Path { get; set; }

        public Guid Id;
        public TimeSpan StartTime { get; set; }
    }
}
