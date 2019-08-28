using System.IO.Abstractions;
using Mmu.Wds.Logic.Areas.Services;
using Mmu.Wds.Logic.Areas.Services.Implementation;
using Mmu.Wds.Logic.Areas.Services.Servants;
using Mmu.Wds.Logic.Areas.Services.Servants.Implementation;
using Mmu.Wds.Logic.Areas.Services.WebsitePartHandler;
using Mmu.Wds.Logic.Areas.Services.WebsitePartHandler.Implementation;
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
            For<IFilePathServant>().Use<FilePathServant>().Singleton();
            For<IUrlAlignmentServant>().Use<UrlAlignmentServant>().Singleton();

            For<ICssAlignmentServant>().Use<CssAlignmentServant>().Singleton();

            For<IFileSystem>().Use<FileSystem>().Singleton();
            For<IDownloadService>().Use<DownloadService>().Singleton();
            For<IFilePathServant>().Use<FilePathServant>().Singleton();
        }
    }
}