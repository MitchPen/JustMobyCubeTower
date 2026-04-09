using UnityEngine;
using Zenject;

namespace Core.Services.ScreenBorderProvider
{
    public class IBorderProviderInstaller : MonoInstaller
    {
        [SerializeField] private BorderProvider _borderProvider;

        public override void InstallBindings()
        {
            Container
                .Bind<IScreenBorderProvider>()
                .FromInstance(_borderProvider)
                .AsSingle()
                .NonLazy();
        }
    }
}