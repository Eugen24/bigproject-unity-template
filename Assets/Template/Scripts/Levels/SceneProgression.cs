using NaughtyAttributes;
using Template.Scripts.Analytics;
using Template.Scripts.DI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Template.Scripts.Levels
{
    public class SceneProgression : InjectedMono
    {
        [In] private AnalyticsHandler _analytics;
        public int CurrentScene
        {
            get => PlayerPrefs.GetInt("CurrentScene", 1);
            private set => PlayerPrefs.SetInt("CurrentScene", value);
        }
        
        public int TotalFinishedScenes
        {
            get => PlayerPrefs.GetInt("TotalFinishedScenes", 1);
            private set => PlayerPrefs.SetInt("TotalFinishedScenes", value);
        }

        public void ReloadScene()
        {
            if (CurrentScene >= SceneManager.sceneCountInBuildSettings)
            {
                CurrentScene = SceneManager.sceneCountInBuildSettings - 1;
            }
            
            SceneManager.LoadScene(CurrentScene);
        }

        public override void OnSyncAfterStart()
        {
            if (SceneManager.GetActiveScene().buildIndex > 0)
                _analytics.LevelStart(CurrentScene);
        }

        public void LoadNextSceneFromFirstScene()
        {
            if (CurrentScene >= SceneManager.sceneCountInBuildSettings)
            {
                CurrentScene = SceneManager.sceneCountInBuildSettings - 1;
            }

            if (CurrentScene < 1)
            {
                CurrentScene = 1;
            }
            
            SceneManager.LoadScene(CurrentScene);
        }
        
        public void LoadNextScene(bool isFinalRandom = false)
        {
            TotalFinishedScenes += 1;
            _analytics.LevelFinish(CurrentScene);
            var nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            if (CurrentScene < nextLevel)
            {
                CurrentScene = nextLevel;
            }
            
            if (CurrentScene >= SceneManager.sceneCountInBuildSettings)
            {
                if (isFinalRandom)
                {
                    int r = Random.Range(1, SceneManager.sceneCountInBuildSettings);
                    SceneManager.LoadScene(r);
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
                }
            }
            else
            {
                SceneManager.LoadScene(CurrentScene);
            }
        }

#if UNITY_EDITOR

        [Button]
        public void ReloadScene_Editor()
        {
            ReloadScene();
        }
        
        [Button]
        public void LoadNextLevel_Editor()
        {
            LoadNextScene(false);
        }
        
        [Button]
        public void LoadLastLevel_Editor()
        {
            CurrentScene = SceneManager.sceneCountInBuildSettings - 1;
            SceneManager.LoadScene(CurrentScene);
        }

        [Button]
        public void LoadFirstAndClearSaves_Editor()
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(1);
        }
#endif

    }
}
