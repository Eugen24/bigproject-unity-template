using UnityEngine;

namespace Template.Plugins.MUnityUtils.ExtensionMethods
{
    public static class ColorExtensionMethods
    {
        /// <summary>
        /// Generate random color
        /// </summary>
        /// <param name="color">Color</param>
        /// <param name="minClamp">Min color value</param>
        /// <returns>New random color</returns>
        public static Color MakeRandomColor(this Color color, float minClamp = 0f)
        {
            var randCol = UnityEngine.Random.onUnitSphere * 3;
            randCol.x = Mathf.Clamp(randCol.x, minClamp, 1f);
            randCol.y = Mathf.Clamp(randCol.y, minClamp, 1f);
            randCol.z = Mathf.Clamp(randCol.z, minClamp, 1f);
            return new Color(randCol.x, randCol.y, randCol.z, 1f);
        }
    }
}

