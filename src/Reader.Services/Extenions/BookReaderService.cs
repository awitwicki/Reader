using Microsoft.Extensions.DependencyInjection;

namespace Reader.Services.Extenions;

public static class BookReaderServiceExtensions
{
    public static IServiceCollection AddBookReaderService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IBookReaderService, BookReaderService>();
        return serviceCollection;
    }
}