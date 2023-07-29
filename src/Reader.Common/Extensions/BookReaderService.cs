using Microsoft.Extensions.DependencyInjection;
using Reader.Domain.Interfaces;
using Reader.Services;

namespace Reader.Common.Extensions;

public static class BookReaderServiceExtensions
{
    public static IServiceCollection AddBookReaderServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IReaderBookState, ReaderBookState>();
        serviceCollection.AddScoped<IBookReaderService, BookReaderService>();
        serviceCollection.AddTransient<ITranslateService, AzureTranslateService>();
        serviceCollection.AddTransient<IScrollInfoService, ScrollInfoService>();
        serviceCollection.AddTransient<ISettings, SettingsService>();
        return serviceCollection;
    }
}
