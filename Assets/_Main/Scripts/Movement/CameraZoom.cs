using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Main.Scripts.Movement
{
    public class CameraZoom : MonoBehaviour
    {
        // make camera zoon in and out with mouse scroll wheel
        [SerializeField] private int zoomSpeed = 10;
        [SerializeField] private int minZoom = 50;
        [SerializeField] private int maxZoom = 150;

        private PixelPerfectCamera _camera;
        
        private void Start()
        {
            _camera = GetComponent<PixelPerfectCamera>();
        }

        private void Update()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (!Mathf.Approximately(scroll, 0f))
            {
                float delta = scroll * zoomSpeed;
                float target = _camera.assetsPPU + delta;
                int newSize = Mathf.Clamp(Mathf.RoundToInt(target), minZoom, maxZoom);
                _camera.assetsPPU = newSize;
            }
        }
    }
}