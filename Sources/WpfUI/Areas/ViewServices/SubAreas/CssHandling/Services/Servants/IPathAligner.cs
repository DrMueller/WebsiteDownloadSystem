using System;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.CssHandling.Services.Servants
{
    public interface IPathAligner
    {
        string AlignFilePath(string cssFilePath, string cssUrlValue);

        Uri AlignUrl(string cssFileUrl, string cssUrlValue);
    }
}