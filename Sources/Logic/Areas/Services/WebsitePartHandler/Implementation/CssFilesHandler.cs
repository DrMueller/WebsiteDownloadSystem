using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Mmu.Mlh.LanguageExtensions.Areas.Collections;
using Mmu.Wds.Logic.Areas.Services.Models;
using Mmu.Wds.Logic.Areas.Services.Servants;

namespace Mmu.Wds.Logic.Areas.Services.WebsitePartHandler.Implementation
{
    internal class CssFilesHandler : PartHandlerBase
    {
        private readonly IFileSystem _fileSystem;
        private readonly IFilePathFactory _filePathFactory;

        public CssFilesHandler(
            IFileSystem fileSystem,
            IFilePathServant filePathServant,
            IUrlAlignmentServant urlAligner,
            IFilePathFactory filePathFactory)
            : base(filePathServant, urlAligner, filePathFactory)
        {
            _fileSystem = fileSystem;
            _filePathFactory = filePathFactory;
        }

        protected override IReadOnlyCollection<WebsitePart> GetParts(HtmlDocument htmlDoc)
        {
            return htmlDoc.DocumentNode
                .Descendants()
                .Where(f => f.Name == "link")
                .Where(f => (f.Attributes.Single(f => f.Name == "rel").Value == "stylesheet"))
                .Select(f => f.Attributes.Single(f => f.Name == "href"))
                .Select(attr => new WebsitePart(attr))
                .ToList();
        }

        protected override void PostProcessPart(WebClient webClient, WebsitePart part, string absoluteUrl, string savePath)
        {
            var urlRegex = new Regex(@"(url\()(?<urlVal>.+)(\))");
            var cssData = _fileSystem.File.ReadAllText(savePath);
            var matches = urlRegex.Matches(cssData);

            matches.ForEach(match =>
            {
                var refMatch = match.Groups["urlVal"];
                var includeValue = refMatch.Value;

                var cssValueUrl = _filePathFactory.CreateAbsoluteSavePa
            });
        }
    }
}
