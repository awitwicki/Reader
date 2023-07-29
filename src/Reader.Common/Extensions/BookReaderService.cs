using Microsoft.Extensions.DependencyInjection;
using Reader.Domain.Interfaces;
using Reader.Services;

namespace Reader.Common.Extensions;

public static class BookReaderServiceExtensions
{
    private static IServiceCollection AddBaseReaderServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IReaderBookState, ReaderBookState>();
        serviceCollection.AddScoped<IBookReaderService, BookReaderService>();
        serviceCollection.AddTransient<ITranslateService, AzureTranslateService>();
        serviceCollection.AddTransient<IScrollInfoService, ScrollInfoService>();
        return serviceCollection;
    }

    public static IServiceCollection AddBookReaderServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IFileManager, BlazorServersideFileManager>();
        serviceCollection.AddBaseReaderServices();
        serviceCollection.AddTransient<ISettings, ApplicationStorageSettingsService>();
        return serviceCollection;
    }

    public static IServiceCollection AddBookReaderMauiServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IFileManager, MauiFileManager>();
        serviceCollection.AddBaseReaderServices();
        serviceCollection.AddTransient<ISettings, MauiSettingsService>();
        return serviceCollection;
    }
}
