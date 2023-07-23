using Reader.Domain.Helpers;
using Reader.Domain.Interfaces;
using Reader.Domain.Models;

namespace Reader.Services;

public class ReaderBookState : IReaderBookState
{
    public Subscribable<string> BookName { get; set; } = new("Reader");
    public Subscribable<string> BookSectionName { get; set; } = new();
    public Subscribable<string> BookChapterName { get; set; } = new();
    public Subscribable<IList<BookSection>> BookSections { get; set; } = new();
    public Subscribable<List<BookSentence>> BookSectionContent { get; set; } = new();
}
