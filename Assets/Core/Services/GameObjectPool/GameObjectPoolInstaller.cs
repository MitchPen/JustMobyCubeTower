using Zenject;

namespace Core.Services.GameObjectPool
{
    public class GameObjectPoolInstaller : Installer<GameObjectPoolInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<GameObjectPool>()
                .AsSingle()
                .NonLazy();
        }
    }
}