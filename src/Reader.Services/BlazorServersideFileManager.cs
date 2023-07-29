using Reader.Domain.Interfaces;

namespace Reader.Services;

public class BlazorServersideFileManager : IFileManager
{
    public Task<Stream> OpenFile(string fileName)
    {
        FileStream stream = new(fileName, FileMode.Open);
        return Task.FromResult((Stream)stream);
    }
}
