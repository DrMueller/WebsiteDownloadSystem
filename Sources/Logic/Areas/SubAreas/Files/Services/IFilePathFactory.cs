namespace Mmu.Wds.Logic.Areas.SubAreas.Files.Services
{
    internal interface IFilePathFactory
    {
        string CreateAbsoluteSavePath(string targetBasePath, string relativeUrlPath);
    }
}