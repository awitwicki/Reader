namespace Reader.Domain.Interfaces;

public interface IFileManager
{
    Task<Stream> OpenFile(string fileName);
}
