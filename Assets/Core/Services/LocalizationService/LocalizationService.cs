using System.Collections.Generic;
using Core.Services.LocalizationService.Data;
using UnityEngine;
using Zenject;

namespace Core.Services.LocalizationService
{
    public class LocalizationService: ILocalizationService
    {
        private ILocalizationDataProvider _localizationDataProvider;
        private LocalizationData _currentLocalizationData;
        private string _currentLanguage;
        private Dictionary<LocalizationPhraseKey, string>  _phraseDictionary;
        
        [Inject]
        public LocalizationService(ILocalizationDataProvider localizationDataProvider)
        {
            _localizationDataProvider =  localizationDataProvider;
            _currentLocalizationData = _localizationDataProvider.GetLocalizationData();
            SetCurrentLanguage(_currentLocalizationData.DefaultLanguage);
        }
        
        public string GetCurrentLanguage() => _currentLanguage;
            
        public string GetLine(LocalizationPhraseKey key)
        {
            if(string.IsNullOrEmpty(_currentLanguage) || _phraseDictionary.Count == 0) return null;
            if(_phraseDictionary.TryGetValue(key, out var line)) return line;
            Debug.LogWarning($"{typeof(LocalizationService)}: key({key}) not found");
            return null;
        }
        
        public void SetCurrentLanguage(string language)
        {
            if (_currentLocalizationData.LanguagesSetup == null ||  _currentLocalizationData.LanguagesSetup.Count == 0)
            {
                Debug.LogWarning($"{typeof(LocalizationService)}: LanguagesSetup is null or empty");
                return;
            }

            var setup = new LocalizationDataItem();
            foreach (var data in _currentLocalizationData.LanguagesSetup)
            {
                if (string.Equals(data.LanguageKey, language))
                {
                    _currentLanguage = language;
                    setup = data;
                    break;
                }
            }

            if (string.IsNullOrEmpty(setup.LanguageKey))
            {
                Debug.LogWarning($"{typeof(LocalizationService)}: LanguagesSetup not  found");
                return;
            }
            _phraseDictionary = new Dictionary<LocalizationPhraseKey, string>();
            foreach (var c in setup.PhraseContainers)
                _phraseDictionary.Add(c.Key, c.Line);
        }
    }
}