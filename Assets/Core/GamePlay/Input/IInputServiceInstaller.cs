using UnityEngine;
using Zenject;

namespace Core.GamePlay.Input
{
    public class IInputServiceInstaller : MonoInstaller
    {
        [SerializeField] private InputService inputService;

        public override void InstallBindings()
        {
            Container
                .Bind<IInputService>()
                .FromInstance(inputService)
                .AsSingle();
        }
    }
}