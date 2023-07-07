using Reader.Domain.Helpers;
using Reader.Domain.Models;

namespace Reader.Domain.Interfaces;

public interface IReaderBookState
{
    Subscribable<string> BookName { get; set; }
    Subscribable<string> BookSectionName { get; set; }
    Subscribable<IList<BookSection>> BookSections { get; set; }
    Subscribable<List<BookSentence>> BookSectionContent { get; set; }
}
