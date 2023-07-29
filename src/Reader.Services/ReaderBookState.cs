using Reader.Domain.Helpers;
using Reader.Domain.Interfaces;
using Reader.Domain.Models;

namespace Reader.Services;

public class ReaderBookState : IReaderBookState
{
    public Subscribable<bool> IsDarkMode { get; set; } = new(true);
    public Subscribable<decimal> FontSize { get; set; } = new(1.25m);
    public Subscribable<string> BookName { get; set; } = new("Reader");
    public Subscribable<string> BookSectionName { get; set; } = new();
    public Subscribable<string> BookChapterName { get; set; } = new();
    public Subscribable<IList<BookSection>> BookSections { get; set; } = new();
    public Subscribable<List<BookSentence>> BookSectionContent { get; set; } = new();
}
