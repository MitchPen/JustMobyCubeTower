using System;
using System.Collections.Generic;

namespace Core.Services.LocalizationService.Data
{
    [Serializable]
    public struct LocalizationDataItem
    {
        public string LanguageKey;
        public List<PhraseContainer>  PhraseContainers;
    }
}