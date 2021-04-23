using NaughtyAttributes;
using UnityEngine;

namespace Template.Scripts.EditorUtils.Editor
{
    [CreateAssetMenu(fileName = "GeekonPublishingConfig", menuName = "_Publisher/GeekonPublishingConfig", order = 2)]
    public class GeekonPublishingConfig : ScriptableObject
    {
        public string FacebookId;
        public string AdjustIdAndroid;
        public string AdjustIdiOS;
        

#if GEEKON_LIONSTUDIO
        [Button("ApplyIds")]
        public void ApplyIds()
        {
            PublisherIntegrator.SetIds();
        }
#else
        [Button("Not found SDK")]
        public void NotFoundSDK()
        {
            Debug.LogError("Not found SDK!");
        }
 #endif

    }
}
