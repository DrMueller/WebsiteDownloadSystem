using System;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.CssHandling.Models
{
    public class CssUrl
    {
        public string FilePath { get; set; }
        public string OriginalRawUrl { get; set; }
        public Uri Uri { get; set; }

        public CssUrl(string originalRawUrl, string filePath, Uri uri)
        {
            Guard.StringNotNullOrEmpty(() => originalRawUrl);
            Guard.StringNotNullOrEmpty(() => filePath);
            Guard.ObjectNotNull(() => uri);

            OriginalRawUrl = originalRawUrl;
            FilePath = filePath;
            Uri = uri;
        }
    }
}