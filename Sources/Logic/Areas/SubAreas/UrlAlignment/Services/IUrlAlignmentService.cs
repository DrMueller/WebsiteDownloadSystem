using System;

namespace Mmu.Wds.Logic.Areas.SubAreas.UrlAlignment.Services
{
    internal interface IUrlAlignmentService
    {
        string CreateAbsoluteUrl(Uri downloadUri, string path);
    }
}