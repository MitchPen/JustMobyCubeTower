using UnityEngine;
using Zenject;

namespace Core.GamePlay.UI
{
    public class InventoryViewInstaller : MonoInstaller
    {
        [SerializeField] private InventoryFactoryConfig _config;
        [SerializeField] private InventoryView _view;

        public override void InstallBindings()
        {
            Container
                .BindInstance(_config)
                .AsSingle()
                .NonLazy();
            Container
                .Bind<InventoryItemFactory>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInstance(_view)
                .AsSingle()
                .NonLazy();
        }
    }
}
