namespace Mmu.Wds.WpfUI.Areas.ViewData
{
    public class LinkHandlingOptions
    {
        public bool DoDownloadLocally { get; }

        public LinkHandlingOptions(bool doDownloadLocally)
        {
            DoDownloadLocally = doDownloadLocally;
        }
    }
}