using System;
using System.IO;
using System.IO.Abstractions;
using System.Net;
using System.Text.RegularExpressions;
using Mmu.Mlh.LanguageExtensions.Areas.Collections;

namespace Mmu.Wds.Logic.Areas.Services.Servants.Implementation
{
    internal class CssAlignmentServant : ICssAlignmentServant
    {
        private readonly IFileSystem _fileSystem;

        public CssAlignmentServant(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void AlignCssFiles(WebClient client, Uri downloadUri, string targetPath)
        {
            var cssFilePaths = _fileSystem.Directory.GetFiles(targetPath, "*.css", SearchOption.AllDirectories);

           
        }
    }
}