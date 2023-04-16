using System.Diagnostics;
using System.Xml;
using FB2Library;
using Reader.Services.Helpers;

namespace Reader.Services;

public class BookReaderService : IBookReaderService
{
    private readonly ReaderBookState _ReaderBookState;
    public Subscribable<FB2File> Book { get; set; }

    public BookReaderService(ReaderBookState readerBookState)
    {
        _ReaderBookState = readerBookState;
        Book = new();
    }

    public IEnumerable<string> GetBooks()
    {
        return Directory.GetFiles("/");
    }
    
    public async Task<FB2File> GetBookAsync(string filePath)
    {
        await using FileStream stream = new("1984.fb2", FileMode.Open);
        Book.Value = await _ReadFB2FileStreamAsync(stream);
        _ReaderBookState.BookName.Value = Book.Value.TitleInfo.BookTitle.Text;
        return Book.Value;
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
            FB2File file = await new FB2Reader().ReadAsync(stream, loadSettings);
            return file;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(string.Format("Error loading file : {0}", ex.Message));
        }

        return null;
    }
}