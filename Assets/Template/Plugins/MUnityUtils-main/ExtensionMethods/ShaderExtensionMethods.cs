using UnityEngine;

namespace MUnityUtils.ExtensionMethods
{
    public static class ShaderExtensionMethods
    {
         public enum BlendMode
        {
            Opaque = 0,
            Cutout = 1,
            Fade = 2,
            Transparent = 3
        }

         public static void ChangeRenderMode(this Material material, BlendMode blendMode)
         {
             switch (blendMode)
             {
                 case BlendMode.Opaque:
                     material.SetFloat("_Mode", 0f);
                     material.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.One);
                     material.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.Zero);
                     material.SetInt("_ZWrite", 1);
                     material.DisableKeyword("_ALPHATEST_ON");
                     material.DisableKeyword("_ALPHABLEND_ON");
                     material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                     material.renderQueue = -1;
                     break;
                 case BlendMode.Cutout:
                     material.SetFloat("_Mode", 1f);
                     material.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.One);
                     material.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.Zero);
                     material.SetInt("_ZWrite", 1);
                     material.EnableKeyword("_ALPHATEST_ON");
                     material.DisableKeyword("_ALPHABLEND_ON");
                     material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                     material.renderQueue = 2450;
                     break;
                 case BlendMode.Fade:
                     material.SetFloat("_Mode", 2f);
                     material.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
                     material.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                     material.SetInt("_ZWrite", 0);
                     material.DisableKeyword("_ALPHATEST_ON");
                     material.EnableKeyword("_ALPHABLEND_ON");
                     material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                     material.renderQueue = 3000;
                     break;
                 case BlendMode.Transparent:
                     material.SetFloat("_Mode", 3f);
                     material.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.One);
                     material.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                     material.SetInt("_ZWrite", 0);
                     material.DisableKeyword("_ALPHATEST_ON");
                     material.DisableKeyword("_ALPHABLEND_ON");
                     material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                     material.renderQueue = 3000;
                     break;
             }
         }
    }
}
