using Core.Services.CameraProvider;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.GamePlay.Input
{
    public class InputService: MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IInputService
    {
        [SerializeField] private RectTransform _pointer;
        [Inject] private ICameraProvider _cameraProvider;
        private Camera _camera;
        
        private bool _isPointerDown;
        private Vector3 _pointerPos;

        private void Start()
        {
            _camera =  _cameraProvider.GetCamera();
            _pointer.gameObject.SetActive(false);
            _pointerPos = Vector3.zero;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if(_isPointerDown) return;
            
           _isPointerDown = true;
           CalcPosition(eventData);
           _pointer.gameObject.SetActive(_isPointerDown);
           _pointer.position = _pointerPos;
           Debug.Log(_pointerPos);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if(!_isPointerDown) return;
            
            _isPointerDown = false;
            CalcPosition(eventData);
            _pointer.gameObject.SetActive(_isPointerDown);
            _pointer.position = _pointerPos;
           
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(!_isPointerDown) return;
            
            CalcPosition(eventData);
            _pointer.position = _pointerPos;
        }

        private void CalcPosition(PointerEventData eventData)
        {
            _pointerPos = _camera.ScreenToWorldPoint(eventData.position);
            _pointerPos.z = 0;
            Debug.Log(_pointerPos);
        }

        public Vector2 GetPointerPosition() => _pointerPos;
    }
}