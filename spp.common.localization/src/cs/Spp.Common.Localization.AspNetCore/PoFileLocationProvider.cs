using Microsoft.Extensions.FileProviders;
using OrchardCore.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Spp.Common.Localization.AspNetCore;

internal class PoFileLocationProvider(string contentDirectoryRelativePath) : ILocalizationFileLocationProvider
{
    private readonly string _contentDirectoryPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        contentDirectoryRelativePath);

    public IEnumerable<IFileInfo> GetLocations(string cultureName)
    {
        return Directory.GetFiles(_contentDirectoryPath, $"*.{cultureName}.po", SearchOption.AllDirectories)
            .Where(x => Path.GetFileName(Path.GetDirectoryName(x)) == "Localization")
            .Select(x => new FileInfoAdapter(new FileInfo(x)))
            .ToList();
    }

    private class FileInfoAdapter(FileInfo inner) : IFileInfo
    {
        public bool Exists => inner.Exists;

        public bool IsDirectory => false;

        public DateTimeOffset LastModified => inner.LastWriteTimeUtc;

        public long Length => inner.Length;

        public string Name => inner.Name;

        public string? PhysicalPath => inner.FullName;

        public Stream CreateReadStream()
        {
            return inner.OpenRead();
        }
    }
}
