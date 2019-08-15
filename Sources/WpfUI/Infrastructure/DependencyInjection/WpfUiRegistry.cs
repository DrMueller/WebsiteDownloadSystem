using StructureMap;

namespace Mmu.Wds.WpfUI.Infrastructure.DependencyInjection
{
    public class WpfUiRegistry : Registry
    {
        public WpfUiRegistry()
        {
            Scan(scanner =>
            {
                scanner.AssemblyContainingType<WpfUiRegistry>();
                scanner.WithDefaultConventions();
            });
        }
    }
}