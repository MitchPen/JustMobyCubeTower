using UnityEngine;
using Zenject;

namespace Core.Services.CameraProvider
{
    public class ICameraProviderInstaller: MonoInstaller
    {
        [SerializeField] private CameraProvider _cameraProvider;

        public override void InstallBindings()
        {
            Container.Bind<ICameraProvider>()
                .FromInstance(_cameraProvider)
                .AsSingle()
                .NonLazy();
        }
    }
}