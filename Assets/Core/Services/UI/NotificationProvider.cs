using System.Collections;
using System.Collections.Generic;
using Core.Services.LocalizationService;
using Core.Services.LocalizationService.Data;
using Core.Services.UI;
using UnityEngine;
using Zenject;

public class NotificationProvider : MonoBehaviour, INotificationProvider
{
    [Inject] private ILocalizationService _localization;
    [SerializeField] private NotificationView _view = null;
    
    public void ShowNotification(LocalizationPhraseKey key)
    {
        var message = _localization.GetLine(key);
        if(string.IsNullOrEmpty(message)) return;
        _view.ShowNotification(message);
    }
}
