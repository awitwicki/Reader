using FB2Library;
using Reader.Domain.Helpers;

namespace Reader.Domain.Interfaces;

public interface IBookReaderService
{
    Subscribable<FB2File> Book { get; set; }
    IEnumerable<string> GetBooks();
    Task<FB2File> GetBookAsync(string filePath);
    void SelectBookSection(string sectionId);
}