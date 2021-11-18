using MyBookAPI.Application.Common.Interfaces;
using System.IO;

namespace MyBookAPI.Infrastructure.FileStore
{
    public class DirectoryWrapper : IDirectoryWrapper
    {
        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}
