using HtmlAgilityPack;

namespace Mmu.Wds.Logic.Areas.Services.Models
{
    internal class WebsitePart
    {
        private readonly HtmlAttribute _attribute;

        public string Value => _attribute.Value;

        public WebsitePart(HtmlAttribute attribute)
        {
            _attribute = attribute;
        }

        public void WriteValue(string value)
        {
            _attribute.Value = value;
        }
    }
}