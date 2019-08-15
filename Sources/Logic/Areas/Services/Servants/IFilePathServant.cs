namespace Mmu.Wds.Logic.Areas.Services.Servants
{
    internal interface IFilePathServant
    {
        void CleanPath(string path);

        void SaveData(string filePath, byte[] data);
    }
}