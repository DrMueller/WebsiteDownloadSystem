using Mmu.Wds.Logic.Areas.SubAreas.CssHandling.Models;

namespace Mmu.Wds.Logic.Areas.SubAreas.CssHandling.Services
{
    internal interface ICssFileRepository
    {
        CssFile Parse(string filePath, string fileUrl);

        void Save(CssFile file);
    }
}