using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Text.RegularExpressions;
using Mmu.Mlh.LanguageExtensions.Areas.Collections;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.CssHandling.Models;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.CssHandling.Services.Servants;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.Files.Services;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.CssHandling.Services.Implementation
{
    public class CssFileRepository : ICssFileRepository
    {
        private readonly IFileRepository _fileRepo;
        private readonly IFileSystem _fileSystem;
        private readonly IPathAligner _pathAligner;

        public CssFileRepository(
            IFileRepository fileRepo,
            IFileSystem fileSystem,
            IPathAligner pathAligner)
        {
            _fileRepo = fileRepo;
            _fileSystem = fileSystem;
            _pathAligner = pathAligner;
        }

        public CssFile Parse(string filePath, string fileUrl)
        {
            var cssContent = _fileSystem.File.ReadAllText(filePath);
            var urls = ParseUrls(filePath, fileUrl, cssContent);
            return new CssFile(filePath, cssContent, urls);
        }

        public void Save(CssFile file)
        {
            _fileRepo.SaveString(file.FilePath, file.Content);
        }

        private IReadOnlyCollection<CssUrl> ParseUrls(string localCssFilePath, string cssFileWebUrl, string cssContent)
        {
            // We expcet URLs in the format url(/.resources/xxx/webresources/resources/slick.eot?#iefix)
            // We also remove URLs with 'data', as they are not real files
            var urlRegex = new Regex(@"(url\()(?<urlVal>.+?)(\))");
            var matches = urlRegex.Matches(cssContent);
            var result = new List<CssUrl>();

            matches.ForEach(
                match =>
                {
                    var relativeWebUrl = match.Groups["urlVal"].Value;
                    if (relativeWebUrl.Contains("data:", StringComparison.OrdinalIgnoreCase))
                    {
                        return;
                    }

                    var absoluteWebUrl = _pathAligner.AlignUrl(cssFileWebUrl, relativeWebUrl);
                    var localFullFileName = _pathAligner.AlignFilePath(localCssFilePath, relativeWebUrl);
                    var url = new CssUrl(relativeWebUrl, localFullFileName, absoluteWebUrl);
                    result.Add(url);
                });

            return result;
        }
    }
}