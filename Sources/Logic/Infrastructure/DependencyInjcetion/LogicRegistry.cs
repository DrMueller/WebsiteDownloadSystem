using System.IO.Abstractions;
using Mmu.Wds.Logic.Areas.Orchestration.Services;
using Mmu.Wds.Logic.Areas.Orchestration.Services.Implementation;
using Mmu.Wds.Logic.Areas.SubAreas.Files.Services;
using Mmu.Wds.Logic.Areas.SubAreas.Files.Services.Implementation;
using Mmu.Wds.Logic.Areas.SubAreas.UrlAlignment.Services;
using Mmu.Wds.Logic.Areas.SubAreas.UrlAlignment.Services.Implementation;
using Mmu.Wds.Logic.Areas.SubAreas.WebsiteParts.Services;
using Mmu.Wds.Logic.Areas.SubAreas.WebsiteParts.Services.Implementation;
using StructureMap;

namespace Mmu.Wds.Logic.Infrastructure.DependencyInjcetion
{
    public class LogicRegistry : Registry
    {
        public LogicRegistry()
        {
            For<IWebsitePartHandler>().Use<ScriptsHandler>().Singleton();
            For<IWebsitePartHandler>().Use<LinksHandler>().Singleton();
            For<IWebsitePartHandler>().Use<ImagesHandler>().Singleton();
            For<IWebsitePartHandler>().Use<CssFilesHandler>().Singleton();

            For<IFilePathFactory>().Use<FilePathFactory>().Singleton();
            For<IFileRepository>().Use<FileRepository>().Singleton();
            For<IUrlAlignmentService>().Use<UrlAlignmentService>().Singleton();

            For<IFileSystem>().Use<FileSystem>().Singleton();
            For<IDownloadService>().Use<DownloadService>().Singleton();
            For<IFileRepository>().Use<FileRepository>().Singleton();
        }
    }
}