using System;
using System.IO.Abstractions;
using System.Net;
using System.Text.RegularExpressions;

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
            var cssFiles = _fileSystem.Directory.GetFiles(targetPath, "*.css");

            foreach (var cssFile in cssFiles)
            {
                var cssData = _fileSystem.File.ReadAllText(cssFile);

                var regex = new Regex("(url()(\()");
                var tra = regex.Match(stringBlock);
                if (tra.Success)
                {
                    var refMatch = tra.Groups["include"];
                    var includeValue = refMatch.Value;
                }

            }
        }
    }
}