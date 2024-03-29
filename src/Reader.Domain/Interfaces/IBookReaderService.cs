using FB2Library;

namespace Reader.Domain.Interfaces;

public interface IBookReaderService
{
    IEnumerable<string> GetBooks();
    Task<FB2File> LoadBookAsync(string filePath);
    void SelectBookSectionChapter(int sectionIndex, int chapterIndex);
}
