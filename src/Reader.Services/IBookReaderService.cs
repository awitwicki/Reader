using FB2Library;
using Reader.Services.Helpers;

namespace Reader.Services;

public interface IBookReaderService
{
    Subscribable<FB2File> Book { get; set; }
    IEnumerable<string> GetBooks();
    Task<FB2File> GetBookAsync(string filePath);
}