using Core.Services.CameraProvider;
using UnityEngine;
using Zenject;

namespace Core.Services.ScreenBorderProvider
{
    public class BorderProvider : MonoBehaviour, IScreenBorderProvider
    {
        [Inject] private ICameraProvider _cameraProvider;
        
        [SerializeField] private Vector2 _horizontalOffsets;
        [SerializeField] private Vector2 _verticalOffsets;
        
        private ScreenBorderProviderData _screenData;
        private ScreenBorderProviderData _worldData;
        public ScreenBorderProviderData GetScreenBorder() => _screenData;
   

        public ScreenBorderProviderData GetScreenToWorldBorder() => _worldData;

        private void Awake()
        {
            var deviceResolution = Screen.currentResolution;

            _worldData = new ScreenBorderProviderData()
            {
                LeftBorder = ConvertToWorldPoint(new Vector2(0+_horizontalOffsets.x,0)).x,
                RightBorder = ConvertToWorldPoint(new Vector2(deviceResolution.width-_horizontalOffsets.y,0)).x,
                TopBorder = ConvertToWorldPoint(new Vector2(0,deviceResolution.height-_verticalOffsets.x)).y,
                BottomBorder = ConvertToWorldPoint(new Vector2(0,0+_verticalOffsets.y)).y
            };
        }

        private Vector2 ConvertToWorldPoint(Vector2 screenPoint) =>
            _cameraProvider.GetCamera().ScreenToWorldPoint(screenPoint);
    }
}
