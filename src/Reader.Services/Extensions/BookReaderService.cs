using Microsoft.Extensions.DependencyInjection;

namespace Reader.Services.Extensions;

public static class BookReaderServiceExtensions
{
    public static IServiceCollection AddBookReaderService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IBookReaderService, BookReaderService>();
        return serviceCollection;
    }
}