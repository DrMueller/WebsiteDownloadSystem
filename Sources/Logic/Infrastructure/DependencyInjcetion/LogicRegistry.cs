using System.IO.Abstractions;
using Mmu.Wds.Logic.Areas.Orchestration.Services;
using Mmu.Wds.Logic.Areas.Orchestration.Services.Implementation;
using Mmu.Wds.Logic.Areas.Orchestration.Services.Servants;
using Mmu.Wds.Logic.Areas.Orchestration.Services.Servants.Implementation;
using Mmu.Wds.Logic.Areas.SubAreas.CssHandling.Services;
using Mmu.Wds.Logic.Areas.SubAreas.CssHandling.Services.Implementation;
using Mmu.Wds.Logic.Areas.SubAreas.Files.Services;
using Mmu.Wds.Logic.Areas.SubAreas.Files.Services.Implementation;
using Mmu.Wds.Logic.Areas.SubAreas.UrlAlignment.Services;
using Mmu.Wds.Logic.Areas.SubAreas.UrlAlignment.Services.Implementation;
using Mmu.Wds.Logic.Areas.SubAreas.WebCommunication.Services;
using Mmu.Wds.Logic.Areas.SubAreas.WebCommunication.Services.Implementation;
using Mmu.Wds.Logic.Areas.SubAreas.WebsiteParts.Services;
using Mmu.Wds.Logic.Areas.SubAreas.WebsiteParts.Services.Implementation;
using StructureMap;

namespace Mmu.Wds.Logic.Infrastructure.DependencyInjcetion
{
    public class LogicRegistry : Registry
    {
        public LogicRegistry()
        {
            // Common
            For<IFileSystem>().Use<FileSystem>().Singleton();

            // Orchestration
            For<IDownloadService>().Use<DownloadService>().Singleton();
            For<IHtmlDocumentServant>().Use<HtmlDocumentServant>().Singleton();

            // CssHandling
            For<ICssFileFactory>().Use<CssFileFactory>().Singleton();

            // Files
            For<IFileRepository>().Use<FileRepository>().Singleton();
            For<IFilePathFactory>().Use<FilePathFactory>().Singleton();

            // UrlAlignment
            For<IUrlAlignmentService>().Use<UrlAlignmentService>().Singleton();

            // WebCommunication
            For<IWebProxyFactory>().Use<WebProxyFactory>().Singleton();

            // WebsitePArts
            For<IWebsitePartHandler>().Use<ScriptsHandler>().Singleton();
            For<IWebsitePartHandler>().Use<LinksHandler>().Singleton();
            For<IWebsitePartHandler>().Use<ImagesHandler>().Singleton();
            For<IWebsitePartHandler>().Use<CssFilesHandler>().Singleton();
        }
    }
}