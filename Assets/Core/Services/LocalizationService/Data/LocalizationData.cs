using System;
using System.Collections.Generic;

namespace Core.Services.LocalizationService.Data
{
    [Serializable]
    public class LocalizationData
    {
        public string DefaultLanguage;
        public List<LocalizationDataItem> LanguagesSetup;
    }
}
