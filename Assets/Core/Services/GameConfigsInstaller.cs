using Core.Services.LocalizationService.Data;
using UnityEngine;
using Zenject;

namespace Core.Services
{
    public class GameConfigsInstaller : MonoInstaller
    {
        [SerializeField]  private LocalLocalizationConfig _localLocalizationConfig;
        public override void InstallBindings()
        {
            Container
                .BindInstance(_localLocalizationConfig)
                .AsSingle();
        }
    }
}
