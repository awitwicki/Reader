namespace Reader.Domain.Models;

public class BookSection
{
    public int Index { get; set; }
    public string Name { get; set; }
    public List<BookChapter> Chapters { get; set; }
}
