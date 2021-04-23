using UnityEngine;

namespace Template.Scripts.Utils
{
    public class BasicCameraController : MonoBehaviour
    {
        public float Speed = 1f;
        private Transform _tr;

        private Vector2 _previousPos;
        private void Start()
        {
            _previousPos = Input.mousePosition;
            _tr = GetComponent<Transform>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _previousPos = GetMousePos();
            }

            if (Input.GetMouseButton(0))
            {
                var current = GetMousePos();
                var dif = current - _previousPos;
                var p = _tr.right * -dif.x;
                p += _tr.up * -dif.y;
                p.y = 0f;
                p.Normalize();
                transform.position += p * (Time.deltaTime * Speed);
                _previousPos = current;
            }
        }

        private Vector2 GetMousePos()
        {
            var v = Input.mousePosition;
            v.x /= Screen.width;
            v.y /= Screen.height;

            return v;
        }
    }
}
