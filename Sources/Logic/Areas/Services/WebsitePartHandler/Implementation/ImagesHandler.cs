using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Mmu.Wds.Logic.Areas.Services.Models;
using Mmu.Wds.Logic.Areas.Services.Servants;

namespace Mmu.Wds.Logic.Areas.Services.WebsitePartHandler.Implementation
{
    internal class ImagesHandler : PartHandlerBase
    {
        public ImagesHandler(
            IFilePathServant filePathServant,
            IUrlAlignmentServant urlAligner,
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