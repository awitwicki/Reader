using Microsoft.JSInterop;
using Newtonsoft.Json;
using Reader.Domain.Interfaces;
using Reader.Domain.Models;

namespace Reader.Services;

public class SettingsService : ISettings
{
    private readonly IJSRuntime _jsRuntime;
    private const string SettingsKey = "bookSettings";

    private static BookSettings? BookSettings { get; set; }
    
    public SettingsService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<BookSettings> GetSettings()
    {
        if (BookSettings != null)
        {
            return BookSettings;
        }

        var bookSettingsJson = await _jsRuntime.InvokeAsync<string>("readLocalStorage", SettingsKey);

        if (string.IsNullOrWhiteSpace(bookSettingsJson))
        {
            BookSettings = new BookSettings();
        }
        else
        {
            BookSettings = JsonConvert.DeserializeObject<BookSettings>(bookSettingsJson);
        }

        return BookSettings;
    }

    public async Task UpdateSettings(BookSettings newSettings)
    {
        BookSettings = newSettings;
        
        var newSettingsStr = JsonConvert.SerializeObject(newSettings);
        await _jsRuntime.InvokeVoidAsync("addToLocalStorage", SettingsKey, newSettingsStr);
    }
}
