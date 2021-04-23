using UnityEngine;

namespace Template.Scripts.InputHelper
{
    public static class InputNormalized 
    {
        public static Vector2 GetMousePos()
        {
            var mouse = UnityEngine.Input.mousePosition;
            mouse.x /= Screen.width;
            mouse.y /= Screen.height;
            return mouse;
        }
    }
}
