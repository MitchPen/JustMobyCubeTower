using Core.Services.CameraProvider;
using UnityEngine;
using Zenject;

namespace Core.Services.ScreenBorderProvider
{
    public class BorderProvider : MonoBehaviour, IScreenBorderProvider
    {
        [Inject] private ICameraProvider _cameraProvider;
        
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _offset;
        
        private ScreenBorderProviderData _screenData;
        private ScreenBorderProviderData _worldData;
        
        public ScreenBorderProviderData GetScreenBorder() => _screenData;

        public ScreenBorderProviderData GetScreenToWorldBorder() => _worldData;

        private void Awake()
        {
            _screenData = new ScreenBorderProviderData()
            {
                LeftBorder = 0 + _offset,
                RightBorder = _rectTransform.rect.width - _offset,
                TopBorder = _rectTransform.rect.height - _offset,
                BottomBorder = _rectTransform.rect.height + _offset
            };

            _worldData = new ScreenBorderProviderData()
            {
                LeftBorder = ConvertToWorldPoint(new Vector2(_screenData.LeftBorder,0)).x,
                RightBorder = ConvertToWorldPoint(new Vector2(_screenData.RightBorder,0)).x,
                TopBorder = ConvertToWorldPoint(new Vector2(_screenData.TopBorder,0)).y,
                BottomBorder = ConvertToWorldPoint(new Vector2(_screenData.BottomBorder,0)).y
            };
        }

        private Vector2 ConvertToWorldPoint(Vector2 screenPoint) =>
            _cameraProvider.GetCamera().ScreenToWorldPoint(screenPoint);
    }
}
