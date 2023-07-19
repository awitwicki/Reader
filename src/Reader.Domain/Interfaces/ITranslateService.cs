using Reader.Domain.Enums;

namespace Reader.Domain.Interfaces;

public interface ITranslateService
{
    Task<string> Translate(string input, Language language);
}
