using System.Diagnostics;
using System.Xml;
using FB2Library;
using Reader.Domain.Helpers;
using Reader.Domain.Interfaces;

namespace Reader.Services;

public class BookReaderService : IBookReaderService
{
    private readonly ReaderBookState _readerBookState;
    public Subscribable<FB2File> Book { get; set; }

    public BookReaderService(ReaderBookState readerBookState)
    {
        _readerBookState = readerBookState;
        Book = new Subscribable<FB2File>();
    }

    public IEnumerable<string> GetBooks()
    {
        return Directory.GetFiles("/");
    }
    
    public async Task<FB2File> GetBookAsync(string filePath)
    {
        await using FileStream stream = new("1984.fb2", FileMode.Open);
        Book.Value = await _ReadFB2FileStreamAsync(stream);
        _readerBookState.BookName.Value = Book.Value.TitleInfo.BookTitle.Text;
        return Book.Value;
    }

    public void SelectBookSection(string sectionId)
    {
        throw new NotImplementedException();
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