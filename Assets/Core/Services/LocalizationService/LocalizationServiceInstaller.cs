using Zenject;

namespace Core.Services.LocalizationService
{
    public class LocalizationServiceInstaller : Installer<LocalizationServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<LocalizationDataProvider>()
                .AsSingle()
                .NonLazy();
            Container.BindInterfacesAndSelfTo<LocalizationService>()
                .AsSingle()
                .NonLazy();
        }
    }
}