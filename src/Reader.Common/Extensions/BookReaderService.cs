using Microsoft.Extensions.DependencyInjection;
using Reader.Domain.Interfaces;
using Reader.Services;

namespace Reader.Common.Extensions;

public static class BookReaderServiceExtensions
{
    public static IServiceCollection AddBookReaderService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IBookReaderService, BookReaderService>();
        return serviceCollection;
    }
}