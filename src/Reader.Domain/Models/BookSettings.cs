namespace Reader.Domain.Models;

public class BookSettings
{
    public string BookPath { get; set; } = null!;
    public int LastBookSectionIndex { get; set; }
    public int LastBookChapterIndex { get; set; }
    public float LastBookSectionProgress { get; set; }
    public decimal FontSize { get; set; }
    public bool IsDarkMode { get; set; }
}
