using System.Net;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.Orchestration.Models
{
    public class Credentials
    {
        private readonly string _password;
        private readonly string _userName;

        public Credentials(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }

        public Maybe<ICredentials> ToNetCredentials()
        {
            if (string.IsNullOrEmpty(_userName))
            {
                return Maybe.CreateNone<ICredentials>();
            }

            return new NetworkCredential(_userName, _password);
        }
    }
}