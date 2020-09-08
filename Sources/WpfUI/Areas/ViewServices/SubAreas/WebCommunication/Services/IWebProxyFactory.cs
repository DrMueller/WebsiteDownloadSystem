using Mmu.Wds.WpfUI.Areas.ViewServices.Orchestration.Models;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebCommunication.Services
{
    public interface IWebProxyFactory
    {
        IWebProxy Create(Credentials credentials);
    }
}