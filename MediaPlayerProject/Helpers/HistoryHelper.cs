using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Helpers
{
    public class HistoryHelper
    {
        public HistoryHelper() { }
        // Hàm viết/đọc guid
        public void WriteGuidToTextFile(Guid guid)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = System.IO.Path.Combine(filePath, "History.txt");
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine(guid.ToString());
            }
        }

        public List<Guid> ReadGuidFromTextFile()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = System.IO.Path.Combine(filePath, "History.txt");
            string[]? lines = File.ReadAllLines(fileName);
            List<Guid> guidList = new List<Guid>();

            for (int i = lines.Length - 1; i >= 0; i--)
            {
                guidList.Add(Guid.Parse(lines[i]));
            }

            return guidList;
        }
    }
}
