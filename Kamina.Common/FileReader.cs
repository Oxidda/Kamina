using System.IO;
using System.IO.IsolatedStorage;

namespace Kamina.Common
{
    public class FileReader
    {
        public StreamReader GetFileReader(string fileName)
        {
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, isoStore);
            return new StreamReader(isoStream);
        }
    }
}
