using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.CssHandling.Models;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.CssHandling.Services
{
    public interface ICssFileRepository
    {
        CssFile Parse(string filePath, string fileUrl);

        void Save(CssFile file);
    }
}