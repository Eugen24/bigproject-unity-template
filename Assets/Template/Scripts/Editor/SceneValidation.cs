using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Template.Scripts.Editor
{
    public class SceneValidation
    {
        // A Test behaves as an ordinary method
       // [Test]
        public IEnumerator SimpleLoadOfScene()
        {
            var scenes = EditorBuildSettings.scenes;
            foreach (var scene in scenes)
            {
                yield return null;
                //EditorSceneManager.LoadScene(scene.path);
            }
            yield return null;
        }

    }
}
