using Reader.Domain.Models;

namespace Reader.Domain.Interfaces;

public interface ISettings
{
    Task<BookSettings> GetSettings();
    Task UpdateSettings(BookSettings newSettings);
}
