using Core.Services.LocalizationService.Data;

namespace Core.Services.LocalizationService
{
    public interface ILocalizationService
    {
        public string GetLine(LocalizationPhraseKey key);

        public string GetCurrentLanguage();

        public void SetCurrentLanguage(string language);
    }
}