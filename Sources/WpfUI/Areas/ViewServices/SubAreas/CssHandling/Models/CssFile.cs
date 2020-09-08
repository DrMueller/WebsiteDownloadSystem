using System;
using System.Collections.Generic;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.CssHandling.Models
{
    public class CssFile
    {
        public string Content { get; private set; }
        public string FilePath { get; }
        public IReadOnlyCollection<CssUrl> Urls { get; }

        public CssFile(string filePath, string content, IReadOnlyCollection<CssUrl> urls)
        {
            Guard.StringNotNullOrEmpty(() => filePath);
            Guard.StringNotNullOrEmpty(() => content);
            Guard.ObjectNotNull(() => urls);

            Urls = urls;
            FilePath = filePath;
            Content = content;
        }

        internal void AlignUrlFilePath(CssUrl url)
        {
            Content = Content.Replace(url.OriginalRawUrl, url.FilePath, StringComparison.Ordinal);
        }
    }
}