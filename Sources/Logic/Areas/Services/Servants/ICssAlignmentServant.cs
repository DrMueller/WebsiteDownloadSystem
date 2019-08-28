using System;
using System.Net;

namespace Mmu.Wds.Logic.Areas.Services.Servants
{
    internal interface ICssAlignmentServant
    {
        void AlignCssFiles(WebClient webClient, Uri downloadUri, string targetPath);
    }
}