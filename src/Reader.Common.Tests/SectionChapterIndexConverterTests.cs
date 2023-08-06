using Reader.Common.Helpers;

namespace Reader.Common.Tests;

public class SectionChapterIndexConverterTests
{
    [Theory]
    [InlineData (1, 1, 1001)]
    [InlineData (3, 100, 3100)]
    [InlineData (5, 43, 5043)]
    public void CallSectionChapterIndexConverter_WithProperData_ShouldReturnExpectedValue(int sectionIndex,
        int chapterIndex, int expectedResult)
    {
        // Arrange
        
        // Act
        var result = SectionChapterIndexConverter.ConvertSectionChapterIndex(sectionIndex, chapterIndex);
        
        // Assert
        Assert.Equal(expectedResult, result);
    }
    
    [Theory]
    [InlineData (null, null)]
    [InlineData (null, 1)]
    [InlineData (1, null)]
    public void CallSectionChapterIndexConverter_WithNullValueAsInput_ShouldReturnExpectedValue(int? sectionIndex,
        int? chapterIndex)
    {
        // Arrange
        const int expectedResult = -1;
        
        // Act
        var result = SectionChapterIndexConverter.ConvertSectionChapterIndex(sectionIndex, chapterIndex);
        
        // Assert
        Assert.Equal(expectedResult, result);
    }
}
