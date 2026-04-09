using Zenject;

namespace Core.Services.SavingService
{
    public class SavingServiceInstaller : Installer<SavingServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerPrefsSavingService>()
                .AsSingle()
                .NonLazy();
        }
    }
}