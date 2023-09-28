using System.Net;
using Mmu.Wds.WpfUI.Areas.ViewServices.Orchestration.Models;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebCommunication.Services.Implementation
{
    public class WebProxyFactory : IWebProxyFactory
    {
        public IWebProxy Create(Credentials credentials)
        {
            var webClient = new WebClient();
            credentials
                .ToNetCredentials()
                .Reduce(() => throw new System.Exception("Tra"));

            return new WebProxy(webClient);
        }
    }
}