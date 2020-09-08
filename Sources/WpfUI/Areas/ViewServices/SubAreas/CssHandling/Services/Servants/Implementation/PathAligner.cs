using System;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.CssHandling.Services.Servants.Implementation
{
    public class PathAligner : IPathAligner
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
            var newFileName = _fileSystem.Path.GetFileName(cssUrlValue);
            var oldFileName = _fileSystem.Path.GetFileName(absoluteCssPath);

            var newValueParthParts = cssUrlValue.Replace(newFileName, string.Empty, StringComparison.Ordinal).Split(@"/").ToList();
            var absoluteCssPathParts = absoluteCssPath.Replace("/" + oldFileName, string.Empty, StringComparison.Ordinal).Split(@"/").ToList();

            // We navigate down the relative path until we find a part in the absolute path
            // From there, we add the relative parts to the rest of the absolute path
            for (var i = 0; i < newValueParthParts.Count; i++)
            {
                var newVal = newValueParthParts.ElementAt(i);

                if (string.IsNullOrEmpty(newVal) || !absoluteCssPathParts.Contains(newVal))
                {
                    continue;
                }

                var absPathIndex = absoluteCssPathParts.IndexOf(newVal);
                var absoluteBasePathParts = absoluteCssPathParts.Take(absPathIndex);
                var absoluteBasePath = string.Join(@"/", absoluteBasePathParts);
                var relativePartsPath = string.Join(@"/", newValueParthParts);
                var completePath = Path.Join(absoluteBasePath, relativePartsPath);
                var filePath = Path.Combine(completePath, newFileName);
                return filePath;
            }

            throw new Exception("No idea");
        }
    }
}