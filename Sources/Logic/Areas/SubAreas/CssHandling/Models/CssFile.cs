using System;
using System.Collections.Generic;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Wds.Logic.Areas.SubAreas.CssHandling.Models
{
    internal class CssFile
    {
        private readonly string _filePath;
        public string Content { get; private set; }
        public IReadOnlyCollection<CssUrl> Urls { get; }

        public CssFile(IReadOnlyCollection<CssUrl> urls, string content, string filePath)
        {
            Guard.ObjectNotNull(() => urls);
            Guard.StringNotNullOrEmpty(() => content);
            Guard.StringNotNullOrEmpty(() => filePath);

            Urls = urls;
            Content = content;
            _filePath = filePath;
        }

        internal void AlignUrlFilePath(CssUrl url)
        {
            Content = Content.Replace(url.OriginalRawUrl, url.FilePath, StringComparison.Ordinal);
        }
    }
}