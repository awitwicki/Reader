using System.Diagnostics;
using System.IO.Compression;
using System.Xml;
using FB2Library;

namespace Reader.Services;

public class BookReaderService
{
    public async Task<FB2File> GetBook()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("1984.fb2");
        
        return await ReadFB2FileStreamAsync(stream);
    }
    
    private async Task<FB2File> ReadFB2FileStreamAsync(Stream stream)
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