namespace Reader.Services.Helpers;

public class ReaderBookState
{
    public Subscribable<string> BookName { get; } = new("Reader");
}