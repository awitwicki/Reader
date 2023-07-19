using Reader.Domain.Enums;

namespace Reader.Services.Tests;

public class TranslateServiceTests
{
    [Theory]
    [InlineData("dog", "пес")]
    public async Task Translate_WithInputText_ShouldReturnExpectedValue(string input, string expectedValue)
    {
        // Arrange
        var translateService = new TranslateService();
        
        // Act
        var result = await translateService.Translate(input, Language.Ukrainian);
        
        // Assert
        Assert.Equal(expectedValue, result);
    }
}
