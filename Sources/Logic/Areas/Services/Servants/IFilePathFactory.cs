namespace Mmu.Wds.Logic.Areas.Services.Servants
{
    internal interface IFilePathFactory
    {
        string CreateAbsoluteSavePath(string targetBasePath, string relativeUrlPath);
    }
}