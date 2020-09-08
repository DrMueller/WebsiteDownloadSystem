using System.Net;
using Mmu.Wds.WpfUI.Areas.ViewServices.Orchestration.Models;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebCommunication.Services.Implementation
{
    public class WebProxyFactory : IWebProxyFactory
    {
        public IWebProxy Create(Credentials credentials)
        {
            var webClient = new WebClient();
            credentials
                .ToNetCredentials()
                .Evaluate(cred => webClient.Credentials = cred);

            return new WebProxy(webClient);
        }
    }
}