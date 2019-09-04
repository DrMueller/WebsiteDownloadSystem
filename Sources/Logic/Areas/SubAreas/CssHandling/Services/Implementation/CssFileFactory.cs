using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text.RegularExpressions;
using Mmu.Mlh.LanguageExtensions.Areas.Collections;
using Mmu.Wds.Logic.Areas.SubAreas.CssHandling.Models;

namespace Mmu.Wds.Logic.Areas.SubAreas.CssHandling.Services.Implementation
{
    internal class CssFileFactory : ICssFileFactory
    {
        private readonly IFileSystem _fileSystem;

        public CssFileFactory(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public CssFile Parse(string filePath, string fileUrl)
        {
            var cssContent = _fileSystem.File.ReadAllText(filePath);
            var urls = ParseUrls(filePath, fileUrl, cssContent);
            return new CssFile(urls, cssContent, filePath);
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

        private string AlignFilePath(string cssFilePath, string cssUrlValue)
        {
            cssFilePath = cssFilePath.Replace("\\", "/", StringComparison.Ordinal);
            cssUrlValue = cssUrlValue.Replace("\\", "/", StringComparison.Ordinal);

            var newFileName = _fileSystem.Path.GetFileName(cssUrlValue);
            var oldFileName = _fileSystem.Path.GetFileName(cssFilePath);

            var newValueParthParts = cssUrlValue.Replace(newFileName, string.Empty, StringComparison.Ordinal).Split(@"/").ToList();
            var created = cssFilePath.Replace("/" + oldFileName, string.Empty, StringComparison.Ordinal).Split(@"/").Reverse().ToList();

            for (var i = 0; i < newValueParthParts.Count; i++)
            {
                var newVal = newValueParthParts.ElementAt(i);
                if (newVal == "..")
                {
                    created.RemoveAt(i);
                }
                else
                {
                    created.Insert(0, newVal);
                }
            }

            created.Reverse();
            var result = string.Join(@"/", created);
            result += newFileName;

            // CSS URls can have metadata via ?, we don't need to distinct that, as this is just for the browser
            if (result.Contains("?", StringComparison.Ordinal))
            {
                result = result.Substring(0, result.IndexOf("?", StringComparison.Ordinal));
            }

            return result;
        }

        private Uri AlignUrl(string cssFileUrl, string cssUrlValue)
        {
            var newFileName = _fileSystem.Path.GetFileName(cssUrlValue);
            var oldFileName = _fileSystem.Path.GetFileName(cssFileUrl);

            var newValueParthParts = cssUrlValue.Replace(newFileName, string.Empty, StringComparison.Ordinal).Split(@"/").ToList();
            var created = cssFileUrl.Replace("/" + oldFileName, string.Empty, StringComparison.Ordinal).Split(@"/").Reverse().ToList();

            for (var i = 0; i < newValueParthParts.Count; i++)
            {
                var newVal = newValueParthParts.ElementAt(i);
                if (newVal == "..")
                {
                    created.RemoveAt(i);
                }
                else
                {
                    created.Insert(0, newVal);
                }
            }

            created.Reverse();
            var result = string.Join(@"/", created);
            result += newFileName;

            return new Uri(result);
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
                    var uri = AlignUrl(cssFileUrl, rawUrl);
                    var filePath = AlignFilePath(cssFilePath, rawUrl);
                    var isFile = !rawUrl.Contains("data:", StringComparison.OrdinalIgnoreCase);

                    var url = new CssUrl(rawUrl, filePath, uri, isFile);
                    result.Add(url);
                });
            });

            return result;
        }
    }
}