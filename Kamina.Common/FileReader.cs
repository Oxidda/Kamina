using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamina.Common
{
    public class FileReader
    {
        public StreamReader GetFileReader(string fileName)
        {
            var isoStore = IsolatedStorageFile.GetUserStoreForApplication();
            var isoStream = new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, isoStore);
            return new StreamReader(isoStream);
        }
    }
}
