using Mmu.Wds.Logic.Areas.SubAreas.CssHandling.Models;

namespace Mmu.Wds.Logic.Areas.SubAreas.CssHandling.Services
{
    internal interface ICssFileFactory
    {
        CssFile Parse(string filePath, string fileUrl);
    }
}