using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace Kaltura.Request
{
    public class Files : SortedList<string, FileData>
    {
        public void Add(Files files)
        {
            foreach (KeyValuePair<string, FileData> item in files)
            {
                this.Add(item.Key, item.Value);
            }
        }
    }

    public class FileData
    {
        public Stream FileStream { get; set; }
        public string FileName { get; set; }

        public FileData(Stream stream, string fileName)
        {
            FileStream = stream;
            var fileStream = stream as FileStream;
            if (fileStream != null) { FileName = Path.GetFileName(fileStream.Name); }

            // Override filename if property was set regardless of stream type
            if (fileName != null) { FileName = fileName; }
        }
    }
}