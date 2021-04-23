using UnityEngine;

namespace Template.Scripts.Utils
{
    [ExecuteInEditMode]
    public class ClearPlayerPrefsSystem : MonoBehaviour
    {
#if UNITY_EDITOR
        void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Delete))
            {
                PlayerPrefs.DeleteAll();
            }
        }
#endif

    }
}
