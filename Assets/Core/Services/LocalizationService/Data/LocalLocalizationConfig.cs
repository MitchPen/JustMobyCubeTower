using UnityEngine;

namespace Core.Services.LocalizationService.Data
{
    [CreateAssetMenu(fileName = "LocalizationConfig", menuName = "Configs/LocalizationConfig")]
    public class LocalLocalizationConfig : ScriptableObject
    {
        public LocalizationData LocalizationData;
    }
}