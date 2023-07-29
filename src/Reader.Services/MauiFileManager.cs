using Microsoft.Maui.Storage;
using Reader.Domain.Interfaces;

namespace Reader.Services;

public class MauiFileManager : IFileManager
{
    public Task<Stream> OpenFile(string fileName)
    {
        return FileSystem.OpenAppPackageFileAsync(fileName);
    }
}
