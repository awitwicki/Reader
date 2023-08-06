namespace Reader.Common.Helpers;

public static class SectionChapterIndexConverter
{
   public static int ConvertSectionChapterIndex(int? sectionIndex, int? chapterIndex) =>
      sectionIndex * 1000 + chapterIndex ?? -1;
}
