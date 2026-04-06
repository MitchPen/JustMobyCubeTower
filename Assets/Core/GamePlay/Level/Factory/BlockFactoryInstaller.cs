using UnityEngine;
using Zenject;

namespace Core.GamePlay.Level.Factory
{
    public class BlockFactoryInstaller : MonoInstaller
    {
        [SerializeField] private BlockFactoryConfig _factoryConfig;
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(_factoryConfig)
                .AsSingle();
            Container
                .BindInterfacesTo<BlockFactory>()
                .AsSingle();
        }
    }
}