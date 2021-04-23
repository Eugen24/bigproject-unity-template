using UnityEngine;

namespace MUnityUtils.ExtensionMethods
{
    public static class RendererExtensionMethods
    {
        /// <summary>
        /// Set alpha to SpriteRenderer
        /// </summary>
        /// <param name="spriteRenderer">Sprite renderer</param>
        /// <param name="alpha">Alpha value.</param>
        public static void SetAlpha(this SpriteRenderer spriteRenderer, float alpha)
        {
            var color = spriteRenderer.color;
            spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
        }
        /// <summary>
        /// Checks if Renderer can be seen by camera
        /// </summary>
        /// <param name="renderer">Renderrer</param>
        /// <param name="camera">Camera</param>
        /// <returns>If camera can be seen by camera</returns>
        public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
        {
            var planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
        }
    }
}