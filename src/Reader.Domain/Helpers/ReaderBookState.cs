namespace Reader.Domain.Helpers;

public class ReaderBookState
{
    public Subscribable<string> BookName { get; } = new("Reader");
}