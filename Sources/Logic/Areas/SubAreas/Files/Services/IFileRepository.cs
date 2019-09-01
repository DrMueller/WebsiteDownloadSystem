namespace Mmu.Wds.Logic.Areas.SubAreas.Files.Services
{
    internal interface IFileRepository
    {
        void CleanPath(string path);

        void SaveData(string filePath, byte[] data);

        void SaveString(string filePath, string data);
    }
}