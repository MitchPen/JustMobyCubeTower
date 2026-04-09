using Core.Services.LocalizationService.Data;
using Zenject;

namespace Core.Services.LocalizationService
{
    public class LocalizationDataProvider : ILocalizationDataProvider
    {
        private readonly LocalizationData _currentLocalizationData;

        [Inject]
        public LocalizationDataProvider(LocalLocalizationConfig localConfig)
        {
            _currentLocalizationData = localConfig.LocalizationData;
        }

        public LocalizationData GetLocalizationData()
        {
            return _currentLocalizationData;
        }
    }
}