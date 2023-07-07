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
        await using FileStream stream = new("1984.fb2", FileMode.Open);
        Book.Value = await _ReadFB2FileStreamAsync(stream);
        _readerBookState.BookName.Value = Book.Value.TitleInfo.BookTitle.Text;
        
        // Map Book sections
        var bookSections = Book.Value.MainBody.Sections.Select(x => 
            new BookSection
            {
                Id = x.ID,
                Name = x.Title.ToString()!
            })
            .ToList();

        _readerBookState.BookSections.Value = bookSections;
        
        // Load first section
        SelectBookSection(_readerBookState.BookSections.Value[0].Id);
        
        return Book.Value;
    }

    public void SelectBookSection(string sectionId)
    {
        var selectedSection = Book.Value!.MainBody.Sections.First(x => x.ID == sectionId);
        
       

        // Map section sentences
        var ggg = (SectionItem)selectedSection.Content.First();
        var content = ggg.Content.Select(x => x.ToString()).Select(x => x.Split(".")
                .Select(y => new BookSentence
                {
                    Sentence = y.ToString()!
                })
                .ToList()
            )
            .SelectMany(x => x)
            .ToList();

        _readerBookState.BookSectionName.Value = selectedSection.Title.ToString();
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
