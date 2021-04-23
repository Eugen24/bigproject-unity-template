using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Template.Scripts.Utils
{
    public class UiHelper 
    {
        private static List<RaycastResult> results = new List<RaycastResult>();
        public static List<RaycastResult> RaycastMouse()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                pointerId = -1,
            };

            pointerData.position = Input.mousePosition;
            results.Clear();
            EventSystem.current.RaycastAll(pointerData, results);
            return results;
        }

        public static bool IsOnUi()
        {
            var r = RaycastMouse();
            foreach (var result in r)
            {
                if (result.isValid)
                    return true;
            }
            return false;
        }
    }
}
