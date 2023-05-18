using UnityEngine;

namespace EasyInputPointer
{
    public class InputPointer : MonoBehaviour
    {
        [SerializeField] private Transform _pointer;
        [SerializeField] private SpriteRenderer _pointerRenderer;
        [SerializeField] private float _scale = 0f;
        private Camera _camera;

        void Awake()
        {
            _camera = Camera.main;
            _pointerRenderer.transform.localScale = new Vector3(_scale, _scale, 1f);
            _pointerRenderer.transform.localPosition = new Vector3(_scale * 0.75f, _scale * -1.5f, 1);
            _pointerRenderer.enabled = false;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                var inputPosition = Input.mousePosition;
                inputPosition.z = 10;
                var pointerPosition = _camera.ScreenToWorldPoint(inputPosition);
                _pointer.position = pointerPosition;
                _pointer.forward = _camera.transform.forward;
                _pointerRenderer.enabled = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _pointerRenderer.enabled = false;
            }
        }
    }
}
