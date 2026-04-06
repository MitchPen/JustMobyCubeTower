using Core.Services.LocalizationService;
using Core.Services.SavingService;
using Zenject;

namespace Core.Services
{
    public class ServiceInstaller: Installer<ServiceInstaller>
    {
        public override void InstallBindings()
        {
            SavingServiceInstaller.Install(Container);
            LocalizationServiceInstaller.Install(Container);
        }
    }
}
