using System;

namespace Mmu.Wds.Logic.Areas.Services.Servants.Implementation
{
    internal class UrlAlignmentServant : IUrlAlignmentServant
    {
        public string CreateAbsolutePath(Uri downloadUri, string path)
        {
            if (path.StartsWith("/", StringComparison.Ordinal))
            {
                return downloadUri.Scheme + Uri.SchemeDelimiter + downloadUri.Host + path;
            }

            return path;
        }
    }
}