using System;
using System.IO.Abstractions;
using System.Linq;

namespace Mmu.Wds.Logic.Areas.SubAreas.CssHandling.Services.Servants.Implementation
{
    internal class PathAligner : IPathAligner
    {
        private readonly IFileSystem _fileSystem;

        public PathAligner(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public string AlignFilePath(string filePath, string urlValue)
        {
            filePath = filePath.Replace("\\", "/", StringComparison.Ordinal);
            urlValue = urlValue.Replace("\\", "/", StringComparison.Ordinal);

            var absoluteUrl = CreateAbsolutePath(filePath, urlValue);

            // CSS URls can have metadata via ?, we don't need to distinct that, as this is just for the browser
            if (absoluteUrl.Contains("?", StringComparison.Ordinal))
            {
                absoluteUrl = absoluteUrl.Substring(0, absoluteUrl.IndexOf("?", StringComparison.Ordinal));
            }

            return absoluteUrl;
        }

        public Uri AlignUrl(string fileUrl, string urlValue)
        {
            var absoluteUrl = CreateAbsolutePath(fileUrl, urlValue);
            return new Uri(absoluteUrl);
        }

        private string CreateAbsolutePath(string absoluteCssPath, string cssUrlValue)
        {
            // 1. Get and remove the file names
            // 2. Create an absolute path out of the existing Path
            // 3. For each relative part, insert or remove the part, depending on the value
            // 4. Reverse it, to get a compleet path
            // 5. Recreate the path
            var newFileName = _fileSystem.Path.GetFileName(cssUrlValue);
            var oldFileName = _fileSystem.Path.GetFileName(absoluteCssPath);

            var newValueParthParts = cssUrlValue.Replace(newFileName, string.Empty, StringComparison.Ordinal).Split(@"/").ToList();
            var absolutePath = absoluteCssPath.Replace("/" + oldFileName, string.Empty, StringComparison.Ordinal).Split(@"/").Reverse().ToList();

            for (var i = 0; i < newValueParthParts.Count; i++)
            {
                var newVal = newValueParthParts.ElementAt(i);
                if (newVal == "..")
                {
                    absolutePath.RemoveAt(i);
                }
                else
                {
                    absolutePath.Insert(0, newVal);
                }
            }

            absolutePath.Reverse();
            var result = string.Join(@"/", absolutePath);
            result += newFileName;

            return result;
        }
    }
}