using Core.Services.CameraProvider;
using UnityEngine;
using Zenject;

namespace Core.Services.ScreenBorderProvider
{
    public class BorderProvider : MonoBehaviour, IScreenBorderProvider
    {
        [Inject] private ICameraProvider _cameraProvider;
        
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Vector2 _horizontalOffsets;
        [SerializeField] private Vector2 _verticalOffsets;
        
        private ScreenBorderProviderData _screenData;
        private ScreenBorderProviderData _worldData;
        
        public ScreenBorderProviderData GetScreenBorder() => _screenData;

        public ScreenBorderProviderData GetScreenToWorldBorder() => _worldData;

        private void Awake()
        {
            _screenData = new ScreenBorderProviderData()
            {
                LeftBorder = 0 + _horizontalOffsets.x,
                RightBorder = _rectTransform.rect.width - _horizontalOffsets.y,
                TopBorder = _rectTransform.rect.height - _verticalOffsets.x,
                BottomBorder = 0 + _verticalOffsets.y
            };

            _worldData = new ScreenBorderProviderData()
            {
                LeftBorder = ConvertToWorldPoint(new Vector2(_screenData.LeftBorder,0)).x,
                RightBorder = ConvertToWorldPoint(new Vector2(_screenData.RightBorder,0)).x,
                TopBorder = ConvertToWorldPoint(new Vector2(0,_screenData.TopBorder)).y,
                BottomBorder = ConvertToWorldPoint(new Vector2(0,_screenData.BottomBorder)).y
            };
        }

        private Vector2 ConvertToWorldPoint(Vector2 screenPoint) =>
            _cameraProvider.GetCamera().ScreenToWorldPoint(screenPoint);
    }
}
