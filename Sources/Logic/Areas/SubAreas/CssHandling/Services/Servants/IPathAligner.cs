using System;

namespace Mmu.Wds.Logic.Areas.SubAreas.CssHandling.Services.Servants
{
    internal interface IPathAligner
    {
        string AlignFilePath(string cssFilePath, string cssUrlValue);

        Uri AlignUrl(string cssFileUrl, string cssUrlValue);
    }
}