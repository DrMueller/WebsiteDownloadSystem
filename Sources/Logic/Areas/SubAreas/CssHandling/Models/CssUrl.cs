using System;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Wds.Logic.Areas.SubAreas.CssHandling.Models
{
    internal class CssUrl
    {
        public string FilePath { get; set; }
        public string OriginalRawUrl { get; set; }
        public bool TargetsFile { get; set; }
        public Uri Uri { get; set; }

        public CssUrl(string originalRawUrl, string filePath, Uri uri, bool targetsFile)
        {
            Guard.StringNotNullOrEmpty(() => originalRawUrl);
            Guard.StringNotNullOrEmpty(() => filePath);
            Guard.ObjectNotNull(() => uri);

            OriginalRawUrl = originalRawUrl;
            FilePath = filePath;
            Uri = uri;
            TargetsFile = targetsFile;
        }
    }
}