using System.Diagnostics;
using System.Xml;
using FB2Library;
using FB2Library.Elements;
using Reader.Domain.Helpers;
using Reader.Domain.Interfaces;
using Reader.Domain.Models;

namespace Reader.Services;

public class BookReaderService : IBookReaderService
{
    private readonly IReaderBookState _readerBookState;
    private readonly ISettings _settings;
    public Subscribable<FB2File> Book { get; set; }

    public BookReaderService(IReaderBookState readerBookState, ISettings settings)
    {
        _readerBookState = readerBookState;
        _settings = settings;
        Book = new Subscribable<FB2File>();
    }

    public IEnumerable<string> GetBooks() =>
        Directory.GetFiles("/");

    public async Task<FB2File> LoadBookAsync(string filePath)
    {
        await using FileStream stream = new(filePath, FileMode.Open);
        Book.Value = await _ReadFB2FileStreamAsync(stream);
        _readerBookState.BookName.Value = Book.Value.TitleInfo.BookTitle.Text;
        
        // Map Book sections
        var bookSections = Book.Value.MainBody.Sections
            .Select((x, sectionIndex) => new BookSection
            {
                Index = sectionIndex,
                Name = x.Title?.ToString()! ?? "Nameless",
                Chapters = x.Content
                    .Where(y => y is SectionItem)
                    .Select((y, chapterIndex) =>
                    new BookChapter
                    {
                        Index = chapterIndex,
                        Name = $"{chapterIndex}.{((SectionItem)y).Title}"
                    }).ToList()
            })
            .ToList();
      
        _readerBookState.BookSections.Value = bookSections;
        
        // Update settings
        var bookSettings = await _settings.GetSettings();
        bookSettings.BookPath = filePath;
        await _settings.UpdateSettings(bookSettings);
        
        // Load section
        SelectBookSectionChapter(bookSettings.LastBookSectionIndex, bookSettings.LastBookChapterIndex);
        
        return Book.Value;
    }

    // TODO fix exception on reload
    public void SelectBookSectionChapter(int sectionIndex, int chapterIndex)
    {
        var selectedSection = Book.Value!.MainBody.Sections.ElementAtOrDefault(sectionIndex);

        if (selectedSection == null)
            throw new IndexOutOfRangeException($"Boos kection with index [{sectionIndex}] isnt exists");
        
        _readerBookState.BookSectionName.Value = selectedSection.Title?.ToString()! ?? "Nameless";
        
        var selectedChapter = selectedSection.Content.ElementAtOrDefault(chapterIndex);
        
        if (selectedChapter == null)
            throw new IndexOutOfRangeException($"Boos chapter with index [{chapterIndex}] in section [{sectionIndex}: {selectedSection.Title}] isnt exists");

        var content = Array.Empty<BookSentence>().ToList();

        try
        {
            if (selectedChapter is SectionItem)
            {
                _readerBookState.BookChapterName.Value =
                    ((SectionItem)selectedChapter).Title?.ToString()! ?? "Nameless";

                foreach (var item in ((SectionItem)selectedChapter).Content)
                {
                    // TODO extract to extensions
                    if (item is ParagraphItem)
                    {
                        var words = ((ParagraphItem)item).ToString().Split(".")
                            .Select(y => new BookSentence
                            {
                                Sentence = y.ToString()!
                            })
                            .ToList();

                        content.AddRange(words);
                    }
                }
            }
            else
            {
                // Parsing not done yet
                content.Add(new BookSentence
                {
                    Sentence = "[Unsupported format]"
                });
            }
        }
        catch (Exception e)
        {
            content.Add(new BookSentence
            {
                Sentence = "[Unsupported format]"
            });
            
            content.Add(new BookSentence
            {
                Sentence = e.ToString()
            });
        }

        _readerBookState.BookSectionContent.Value = content;
    }

    private async Task<FB2File> _ReadFB2FileStreamAsync(Stream stream)
    {
        // Setup
        var readerSettings = new XmlReaderSettings
        {
            DtdProcessing = DtdProcessing.Ignore
        };
        var loadSettings = new XmlLoadSettings(readerSettings);

        try
        {
            // Reading
            var file = await new FB2Reader().ReadAsync(stream, loadSettings);
            return file;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading file : {ex.Message}");
        }

        return null!;
    }
}
