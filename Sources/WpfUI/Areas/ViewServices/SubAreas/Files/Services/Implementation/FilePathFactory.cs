﻿using System;
using System.IO.Abstractions;
using System.Linq;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.Files.Services.Implementation
{
    public class FilePathFactory : IFilePathFactory
    {
        private readonly IFileSystem _fileSystem;

        public FilePathFactory(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public string CreateAbsoluteSavePath(string targetBasePath, string relativeUrlPath)
        {
            if (relativeUrlPath.StartsWith("/.", StringComparison.Ordinal))
            {
                relativeUrlPath = relativeUrlPath.Substring(2);
            }

            if (!relativeUrlPath.StartsWith("/", StringComparison.Ordinal))
            {
                relativeUrlPath = "/" + relativeUrlPath;
            }

            var invalidPathChars = _fileSystem.Path.GetInvalidPathChars();
            var otherInvalidChars = new[]
            {
                ':'
            };

            var allInvalidChars = invalidPathChars.Concat(otherInvalidChars).ToArray();
            var validFileName = string.Join("_", relativeUrlPath.Split(allInvalidChars));
            var absolutePath = targetBasePath + validFileName;
            return absolutePath;
        }
    }
}