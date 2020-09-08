using System.IO.Abstractions;
using Mmu.Mlh.LanguageExtensions.Areas.Collections;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.Files.Services.Implementation
{
    public class FileRepository : IFileRepository
    {
        private readonly IFileSystem _fileSystem;

        public FileRepository(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void CleanPath(string path)
        {
            var directoryInfo = _fileSystem.DirectoryInfo.FromDirectoryName(path);
            RecursiveDelete(directoryInfo);
        }

        public void SaveData(string filePath, byte[] data)
        {
            AssureDirectoryExists(filePath);
            _fileSystem.File.WriteAllBytes(filePath, data);
        }

        public void SaveString(string filePath, string data)
        {
            AssureDirectoryExists(filePath);
            _fileSystem.File.WriteAllText(filePath, data);
        }

        private static void RecursiveDelete(IDirectoryInfo directoryInfo)
        {
            if (!directoryInfo.Exists)
            {
                return;
            }

            directoryInfo.EnumerateDirectories().ForEach(dir => RecursiveDelete(dir));
            directoryInfo.Delete(true);
        }

        private void AssureDirectoryExists(string filePath)
        {
            var dirPath = _fileSystem.Path.GetDirectoryName(filePath);
            if (!_fileSystem.Directory.Exists(dirPath))
            {
                _fileSystem.Directory.CreateDirectory(dirPath);
            }
        }
    }
}