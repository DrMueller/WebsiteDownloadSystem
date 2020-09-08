using System.IO.Abstractions;
using Lamar;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebsiteParts.Services;

namespace Mmu.Wds.WpfUI.Infrastructure.DependencyInjection
{
    public class WpfUiRegistry : ServiceRegistry
    {
        public WpfUiRegistry()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<WpfUiRegistry>();
                    scanner.AddAllTypesOf<IWebsitePartHandler>();
                    scanner.WithDefaultConventions();
                });

            For<IFileSystem>().Use<FileSystem>().Singleton();
        }
    }
}