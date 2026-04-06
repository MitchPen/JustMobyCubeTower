using UnityEngine;
using Zenject;

namespace Core.GamePlay.Level.SetupProvider
{
    public class ILevelSetupProviderInstaller : MonoInstaller
    {
        [SerializeField] private LocalLevelSetup _localLevelSetup;
        public override void InstallBindings()
        {
            Container.BindInstance(_localLevelSetup)
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<LocalLevelSetupProvider>()
                .AsSingle()
                .NonLazy();
        }
    }
}