using System.IO;

namespace Kamina.Common.Core
{
    public class FileReader
    {
        public StreamReader GetFileReader(string fileName)
        {
            //IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();
           // IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, isoStore);
            return new StreamReader(fileName);
        }
    }
}
