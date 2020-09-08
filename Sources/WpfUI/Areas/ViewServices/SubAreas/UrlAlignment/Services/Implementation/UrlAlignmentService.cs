using System;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.UrlAlignment.Services.Implementation
{
    public class UrlAlignmentService : IUrlAlignmentService
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