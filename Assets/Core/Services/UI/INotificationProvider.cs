using Core.Services.LocalizationService.Data;

namespace Core.Services.UI
{
    public interface INotificationProvider
    {
        public void ShowNotification(LocalizationPhraseKey key);
    }
}