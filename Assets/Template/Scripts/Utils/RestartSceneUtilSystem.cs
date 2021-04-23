using UnityEngine;
using UnityEngine.SceneManagement;

namespace Template.Scripts.Utils
{
    public class RestartSceneUtilSystem : MonoBehaviour
    {

#if UNITY_EDITOR
        void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }
        }
#endif
    }
}
