namespace Reader.Domain.Models;

public class BookSettings
{
    public string BookPath { get; set; } = null!;
    public int LastBookSectionIndex { get; set; }
    public int LastBookChapterIndex { get; set; }
    public float LastBookSectionProgress { get; set; }
    public decimal FontSize { get; set; }
    public bool IsDarkMode { get; set; }
    
    public static BookSettings Default =>
        new BookSettings
        {
            BookPath = null!,
            LastBookSectionIndex = 0,
            LastBookChapterIndex = 0,
            LastBookSectionProgress = 0,
            FontSize = 1.25m,
            IsDarkMode = false
        };
}
