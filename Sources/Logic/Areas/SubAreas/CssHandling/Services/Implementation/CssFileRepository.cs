using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Text.RegularExpressions;
using Mmu.Mlh.LanguageExtensions.Areas.Collections;
using Mmu.Wds.Logic.Areas.SubAreas.CssHandling.Models;
using Mmu.Wds.Logic.Areas.SubAreas.CssHandling.Services.Servants;
using Mmu.Wds.Logic.Areas.SubAreas.Files.Services;

namespace Mmu.Wds.Logic.Areas.SubAreas.CssHandling.Services.Implementation
{
    internal class CssFileRepository : ICssFileRepository
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

        private static IReadOnlyCollection<string> GetRawUrls(string parsedUrlValue)
        {
            var splittedUrls = parsedUrlValue.Split(',');
            var result = new List<string>();

            foreach (var url in splittedUrls)
            {
                var fixedUrl = url;

                // If there is some meta data, we cut it
                var urlRegex = new Regex(@"(url\()(.+)(\) )");
                var match = urlRegex.Match(url);
                if (match.Success)
                {
                    fixedUrl = match.Value;
                }

                fixedUrl = fixedUrl
                    .Replace(")", string.Empty, StringComparison.Ordinal)
                    .Replace("url(", string.Empty, StringComparison.OrdinalIgnoreCase)
                    .Replace("\"", string.Empty, StringComparison.OrdinalIgnoreCase)
                    .Trim();

                result.Add(fixedUrl);
            }

            return result;
        }

        private IReadOnlyCollection<CssUrl> ParseUrls(string cssFilePath, string cssFileUrl, string cssContent)
        {
            var urlRegex = new Regex(@"(url\()(.+)(\))");
            var matches = urlRegex.Matches(cssContent);
            var result = new List<CssUrl>();

            matches.ForEach(match =>
            {
                var rawUrls = GetRawUrls(match.Value);

                rawUrls.ForEach(rawUrl =>
                {
                    var uri = _pathAligner.AlignUrl(cssFileUrl, rawUrl);
                    var filePath = _pathAligner.AlignFilePath(cssFilePath, rawUrl);
                    var isFile = !rawUrl.Contains("data:", StringComparison.OrdinalIgnoreCase);
                    var url = new CssUrl(rawUrl, filePath, uri, isFile);
                    result.Add(url);
                });
            });

            return result;
        }
    }
}