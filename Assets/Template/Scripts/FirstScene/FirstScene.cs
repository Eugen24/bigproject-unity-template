using System.Collections;
using Template.Scripts.DI;
using Template.Scripts.Levels;
using UnityEngine;

namespace Template.Scripts.FirstScene
{
    public class FirstScene : InjectedMono
    {
        public IEnumerator Start()
        {
            //just in case of some 3rd party plugins
            yield return null;
            /*
            if (!FB.IsInitialized) {
                // Initialize the Facebook SDK
                FB.Init(InitCallback, OnHideUnity);
            } else {
                // Already initialized, signal an app activation App Event
                FB.ActivateApp();
            }
*/
            //for facebook to give one frame
            yield return null;
            Application.targetFrameRate = 60;
            _sceneProgression.LoadNextSceneFromFirstScene();
        }
        /*
        private void InitCallback ()
        {
            if (FB.IsInitialized) {
                // Signal an app activation App Event
                FB.ActivateApp();
                // Continue with Facebook SDK
                // ...
            } else {
                Debug.Log("Failed to Initialize the Facebook SDK");
            }
        }

        private void OnHideUnity (bool isGameShown)
        {
            if (!isGameShown) {
                // Pause the game - we will need to hide
                Time.timeScale = 0;
            } else {
                // Resume the game - we're getting focus again
                Time.timeScale = 1;
            }
        }
        */
    }
}
