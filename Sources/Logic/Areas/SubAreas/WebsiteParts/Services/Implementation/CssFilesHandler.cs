using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Abstractions;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Mmu.Mlh.LanguageExtensions.Areas.Collections;
using Mmu.Mlh.LanguageExtensions.Areas.Types.FunctionsResults;
using Mmu.Wds.Logic.Areas.SubAreas.Files.Services;
using Mmu.Wds.Logic.Areas.SubAreas.UrlAlignment.Services;
using Mmu.Wds.Logic.Areas.SubAreas.WebsiteParts.Models;

namespace Mmu.Wds.Logic.Areas.SubAreas.WebsiteParts.Services.Implementation
{
    internal class CssFilesHandler : PartHandlerBase
    {
        private readonly IFilePathFactory _filePathFactory;
        private readonly IFileRepository _fileRepo;
        private readonly IFileSystem _fileSystem;

        public CssFilesHandler(
            IFileSystem fileSystem,
            IFileRepository fileRepo,
            IUrlAlignmentService urlAligner,
            IFilePathFactory filePathFactory)
            : base(fileRepo, urlAligner, filePathFactory)
        {
            _fileSystem = fileSystem;
            _fileRepo = fileRepo;
            _filePathFactory = filePathFactory;
        }

        protected override IReadOnlyCollection<WebsitePart> GetParts(HtmlDocument htmlDoc)
        {
            return htmlDoc.DocumentNode
                .Descendants()
                .Where(f => f.Name == "link")
                .Where(f => f.Attributes.Single(f => f.Name == "rel").Value == "stylesheet")
                .Select(f => f.Attributes.Single(f => f.Name == "href"))
                .Select(attr => new WebsitePart(attr))
                .ToList();
        }

        protected override void PostProcessPart(WebClient webClient, WebsitePart part, string absoluteUrl, string savePath)
        {
            var urlRegex = new Regex(@"(url\()(?<urlVal>.+)(?\))");
            var cssData = _fileSystem.File.ReadAllText(savePath);
            var matches = urlRegex.Matches(cssData);

            //   src: url("../resources/fonts/slick.eot?#iefix") format("embedded-opentype"), url("../resources/fonts/slick.woff") format("woff"), url("../resources/fonts/slick.ttf") format("truetype"), url("../resources/fonts/slick.svg#slick") format("svg");


            matches.ForEach(match =>
            {
                var includeValue = match.Groups["urlVal"].Value;
                includeValue = includeValue.Replace("\"", string.Empty, StringComparison.Ordinal);

                if (includeValue.Contains("slick.eot")) Debugger.Break();

                var cssUrl = AlignUrl(absoluteUrl, includeValue);
                var cssFilePath = AlignFilePath(savePath, includeValue);

                if (!_fileSystem.File.Exists(cssFilePath))
                {
                    var downloadResult = TryDownloading(webClient, cssUrl);
                    if (downloadResult.IsSuccess)
                    {
                        _fileRepo.SaveData(cssFilePath, downloadResult.Value);
                        cssData = cssData.Replace(includeValue, cssFilePath, StringComparison.Ordinal);
                    }
                }
            });

            _fileRepo.SaveString(savePath, cssData);
        }

        private static FunctionResult<byte[]> TryDownloading(WebClient webClient, string url)
        {
            try
            {
                var data = webClient.DownloadData(url);
                return FunctionResult.CreateSuccess(data);
            }
            catch
            {
                return FunctionResult.CreateFailure<byte[]>();
            }
        }

        private string AlignFilePath(string savePath, string newValue)
        {
            savePath = savePath.Replace("\\", "/", StringComparison.Ordinal);
            newValue = newValue.Replace("\\", "/", StringComparison.Ordinal);

            var newFileName = _fileSystem.Path.GetFileName(newValue);
            var oldFileName = _fileSystem.Path.GetFileName(savePath);

            var newValuePath = savePath.Replace(oldFileName, string.Empty, StringComparison.Ordinal);

            var newValueParthParts = newValue.Replace(newFileName, string.Empty, StringComparison.Ordinal).Split(@"/").ToList();
            var created = savePath.Replace("/" + oldFileName, string.Empty, StringComparison.Ordinal).Split(@"/").Reverse().ToList();

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

            return result;
        }

        private string AlignUrl(string absoluteCssPath, string newValue)
        {
            var newFileName = _fileSystem.Path.GetFileName(newValue);
            var oldFileName = _fileSystem.Path.GetFileName(absoluteCssPath);

            var newValuePath = absoluteCssPath.Replace(oldFileName, string.Empty, StringComparison.Ordinal);

            var newValueParthParts = newValue.Replace(newFileName, string.Empty, StringComparison.Ordinal).Split(@"/").ToList();
            var created = absoluteCssPath.Replace("/" + oldFileName, string.Empty, StringComparison.Ordinal).Split(@"/").Reverse().ToList();

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

            return result;
        }
    }
}