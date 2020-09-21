using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Mmu.Mlh.WpfCoreExtensions.Areas.ViewExtensions.Grids.InformationGrids.ViewData;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.Files.Services;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.UrlAlignment.Services;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebsiteParts.Models;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebsiteParts.Services.Implementation
{
    public class ImagesHandler : PartHandlerBase
    {
        public ImagesHandler(
            IFileRepository filePathServant,
            IUrlAlignmentService urlAligner,
            IFilePathFactory filePathFactory)
            : base(filePathServant, urlAligner, filePathFactory)
        {
        }

        protected override IReadOnlyCollection<WebsitePart> GetParts(HtmlDocument htmlDoc, Action<InformationGridEntryViewData> onNewInfo)
        {
            onNewInfo(new InformationGridEntryViewData("Fetching images.."));

            var imgNodes = htmlDoc
                .DocumentNode
                .Descendants()
                .Where(f => f.Name == "img")
                .Select(f => f.Attributes.Single(f => f.Name == "src"))
                .Select(attr => new WebsitePart(attr))
                .ToList();

            var srcNodeAttributes = htmlDoc
                .DocumentNode
                .Descendants()
                .Where(f => f.Name == "source")
                .Select(f => f.Attributes.Single(f => f.Name == "srcset"))
                .ToList();

            var srcNodes = srcNodeAttributes.Select(
                attr =>
                {
                    var val = attr.Value;
                    var commaIndex = val.IndexOf(',');
                    if (commaIndex > -1)
                    {
                        val = val.Substring(0, commaIndex);
                        attr.Value = val;
                    }

                    return new WebsitePart(attr);
                });

            var result = imgNodes.Concat(srcNodes).ToList();

            return result;
        }
    }
}