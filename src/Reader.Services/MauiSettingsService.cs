using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using Reader.Domain.Interfaces;
using Reader.Domain.Models;

namespace Reader.Services;

public class MauiSettingsService : ISettings
{
    private static BookSettings? BookSettings { get; set; }
    
    public Task<BookSettings> GetSettings()
    {
        if (BookSettings != null)
        {
            return Task.FromResult(BookSettings);
        }
        var bookSettingsJson = Preferences.Default.Get("book_settings", string.Empty);
        
        if (string.IsNullOrWhiteSpace(bookSettingsJson))
        {
            BookSettings = new BookSettings();
        }
        else
        {
            BookSettings = JsonConvert.DeserializeObject<BookSettings>(bookSettingsJson);
        }
      
        return Task.FromResult(BookSettings);
    }

    public Task UpdateSettings(BookSettings newSettings)
    {
        BookSettings = newSettings;
        
        var newSettingsStr = JsonConvert.SerializeObject(newSettings);
        
        
        // Set a string value:
        Preferences.Default.Set("book_settings", newSettingsStr);
        
        return Task.CompletedTask;
    }
}
