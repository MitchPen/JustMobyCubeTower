using Core.Services.LocalizationService.Data;

namespace Core.Services.LocalizationService
{
    public interface ILocalizationDataProvider
    {
        public LocalizationData GetLocalizationData();
    }
}