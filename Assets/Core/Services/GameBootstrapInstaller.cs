using Zenject;

namespace Core.Services
{
    public class GameBootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<GameBootstrap>()
                .AsSingle()
                .NonLazy();
        }
    }
}
