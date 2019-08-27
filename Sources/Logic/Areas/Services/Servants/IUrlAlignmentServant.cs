using System;

namespace Mmu.Wds.Logic.Areas.Services.Servants
{
    internal interface IUrlAlignmentServant
    {
        string CreateAbsolutePath(Uri downloadUri, string path);
    }
}