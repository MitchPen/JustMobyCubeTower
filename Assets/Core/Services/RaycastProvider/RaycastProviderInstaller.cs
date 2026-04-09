using UnityEngine;
using Zenject;

namespace Core.Services.RaycastProvider
{
    public class RaycastProviderInstaller : MonoInstaller
    {
        [SerializeField] private Raycaster _raycaster;

        public override void InstallBindings()
        {
            Container
                .Bind<IRaycastProvider>()
                .FromInstance(_raycaster)
                .AsSingle();
        }
    }
}