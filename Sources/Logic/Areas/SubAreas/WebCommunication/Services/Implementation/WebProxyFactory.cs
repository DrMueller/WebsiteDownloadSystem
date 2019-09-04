using System.Net;

namespace Mmu.Wds.Logic.Areas.SubAreas.WebCommunication.Services.Implementation
{
    internal class WebProxyFactory : IWebProxyFactory
    {
        public IWebProxy Create()
        {
            var webClient = new WebClient();
            return new WebProxy(webClient);
        }
    }
}