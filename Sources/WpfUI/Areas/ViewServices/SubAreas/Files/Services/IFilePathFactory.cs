namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.Files.Services
{
    public interface IFilePathFactory
    {
        string CreateAbsoluteSavePath(string targetBasePath, string relativeUrlPath);
    }
}