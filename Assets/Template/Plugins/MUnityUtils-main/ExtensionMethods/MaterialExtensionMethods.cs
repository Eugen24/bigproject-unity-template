using UnityEngine;

namespace MUnityUtils.ExtensionMethods
{
    public static class MaterialExtensionMethods
    {
        /// <summary>
        /// Set material color alpha
        /// </summary>
        /// <param name="material"></param>
        /// <param name="alpha"></param>
        public static void SetAlpha(this Material material, float alpha)
        {
            var color = material.color;
            if (alpha < 0) { alpha = 0; }
            color.a = alpha;
            material.color = color;
        }
    }
}
