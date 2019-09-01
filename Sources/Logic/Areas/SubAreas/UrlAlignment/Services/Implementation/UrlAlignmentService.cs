using System;

namespace Mmu.Wds.Logic.Areas.SubAreas.UrlAlignment.Services.Implementation
{
    internal class UrlAlignmentService : IUrlAlignmentService
    {
        public string CreateAbsoluteUrl(Uri downloadUri, string path)
        {
            if (path.StartsWith("/", StringComparison.Ordinal))
            {
                return downloadUri.Scheme + Uri.SchemeDelimiter + downloadUri.Host + path;
            }

            return path;
        }
    }
}