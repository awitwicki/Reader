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
    public Subscribable<FB2File> Book { get; set; }

    public BookReaderService(IReaderBookState readerBookState)
    {
        _readerBookState = readerBookState;
        Book = new Subscribable<FB2File>();
    }

    public IEnumerable<string> GetBooks()
    {
        return Directory.GetFiles("/");
    }
    
    public async Task<FB2File> LoadBookAsync(string filePath)
    {
        await using FileStream stream = new(filePath, FileMode.Open);
        Book.Value = await _ReadFB2FileStreamAsync(stream);
        _readerBookState.BookName.Value = Book.Value.TitleInfo.BookTitle.Text;
        
        // Map Book sections
        var bookSections = Array.Empty<BookSection>().ToList();
        for (var i = 0; i < Book.Value.MainBody.Sections.Count; i++)
        {
            bookSections.Add(new BookSection
            {
                Index = i,
                Name = Book.Value.MainBody.Sections[i].Title?.ToString()! ?? "Nameless" 
            });
        }
      
        _readerBookState.BookSections.Value = bookSections;
        
        // Load first section
        // TODO load from settings
        SelectBookSection(0);
        
        return Book.Value;
    }

    // TODO fix exception on reload
    public void SelectBookSection(int index)
    {
        var selectedSection = Book.Value!.MainBody.Sections.ElementAtOrDefault(index);

        if (selectedSection == null)
            throw new IndexOutOfRangeException($"Boos kection with index [{index}] isnt exists");
        
        var content = Array.Empty<BookSentence>().ToList();
        
        // Map section sentences
        var selectedItem = selectedSection.Content.First();
        
        // TODO exctarct to extensions
        if (selectedItem is ParagraphItem)
        {
            var ggg = (ParagraphItem)selectedSection.Content.First();
            content = ggg.ParagraphData.Select(x => x.ToString()).Select(x => x.Split(".")
                    .Select(y => new BookSentence
                    {
                        Sentence = y.ToString()!
                    })
                    .ToList()
                )
                .SelectMany(x => x)
                .ToList();
        }
        if (selectedItem is SectionItem)
        {
            var ggg = (SectionItem)selectedSection.Content.First();
            content = ggg.Content.Select(x => x.ToString()).Select(x => x.Split(".")
                    .Select(y => new BookSentence
                    {
                        Sentence = y.ToString()!
                    })
                    .ToList()
                )
                .SelectMany(x => x)
                .ToList();
        }

        _readerBookState.BookSectionName.Value = selectedSection.Title?.ToString()! ?? "Nameless" ;
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
