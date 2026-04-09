using UnityEngine;
using Zenject;

namespace Core.Services.UI
{
    public class NotificationProviderInstaller : MonoInstaller
    {
        [SerializeField] private NotificationProvider notificationProvider;

        public override void InstallBindings()
        {
            Container
                .Bind<INotificationProvider>()
                .FromInstance(notificationProvider)
                .AsSingle()
                .NonLazy();
        }
    }
}