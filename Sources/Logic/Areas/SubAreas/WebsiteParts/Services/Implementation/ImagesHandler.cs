using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Mmu.Wds.Logic.Areas.SubAreas.Files.Services;
using Mmu.Wds.Logic.Areas.SubAreas.UrlAlignment.Services;
using Mmu.Wds.Logic.Areas.SubAreas.WebsiteParts.Models;

namespace Mmu.Wds.Logic.Areas.SubAreas.WebsiteParts.Services.Implementation
{
    internal class ImagesHandler : PartHandlerBase
    {
        public ImagesHandler(
            IFileRepository filePathServant,
            IUrlAlignmentService urlAligner,
            IFilePathFactory filePathFactory)
            : base(filePathServant, urlAligner, filePathFactory)
        {
        }

        protected override IReadOnlyCollection<WebsitePart> GetParts(HtmlDocument htmlDoc)
        {
            return htmlDoc
                .DocumentNode
                .Descendants()
                .Where(f => f.Name == "img")
                .Select(f => f.Attributes.Single(f => f.Name == "src"))
                .Select(attr => new WebsitePart(attr))
                .ToList();
        }
    }
}