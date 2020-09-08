using System;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.UrlAlignment.Services
{
    public interface IUrlAlignmentService
    {
        string CreateAbsoluteUrl(Uri downloadUri, string path);
    }
}