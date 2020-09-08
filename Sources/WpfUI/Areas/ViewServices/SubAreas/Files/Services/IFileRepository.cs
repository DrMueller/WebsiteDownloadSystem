namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.Files.Services
{
    public interface IFileRepository
    {
        void CleanPath(string path);

        void SaveData(string filePath, byte[] data);

        void SaveString(string filePath, string data);
    }
}