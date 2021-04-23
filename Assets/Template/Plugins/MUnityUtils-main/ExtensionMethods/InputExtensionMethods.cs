using UnityEngine;

namespace Template.Plugins.MUnityUtils.ExtensionMethods
{
    public static class InputExtensionMethods
    {
        /// <summary>
        /// Detect drag direction
        /// </summary>
        /// <param name="clickPoint"></param>
        /// <returns></returns>
        public static Vector2 GetDragDirection(this Vector2 clickPoint)
        {
            var currentPosition = Input.mousePosition;
            var direction = Vector2.zero;
            if (clickPoint.x > currentPosition.x)
                direction.x = -1;
            else if (clickPoint.x < currentPosition.x)
                direction.x = 1;
            if (clickPoint.y > currentPosition.y)
                direction.y = -1;
            else if (clickPoint.y < currentPosition.y)
                direction.y = 1;
            return direction;
        }
    }
}
