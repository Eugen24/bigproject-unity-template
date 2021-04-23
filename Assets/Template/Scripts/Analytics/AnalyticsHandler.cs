using Template.Scripts.DI;
using UnityEngine;

namespace Template.Scripts.Analytics
{
    public class AnalyticsHandler : MonoBehaviour
    {
        private IAnalytics _provider;

        protected void Awake()
        {
            _provider = 
#if GEEKON_LIONSTUDIO
                new LionStudioProvider();
#else 
                new GameAnalyticsProvider();
#endif
        }

        public void LevelFinish(int index)
        {
            _provider.LevelFinish(index);
        }
        public void LevelStart(int index)
        {
            _provider.LevelStart(index);
        }
        
        public void CustomEvent(string id)
        {
            _provider.CustomEvent(id);
        }
    }


    public class GameAnalyticsProvider : IAnalytics 
    {
        public GameAnalyticsProvider()
        {
            //GameAnalytics.Initialize();
        }

        public void CustomEvent(string id)
        {
            //GameAnalytics.NewDesignEvent(id);
        }

        public void LevelStart(int index)
        {
            //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, index.ToString("0000"));
        }

        public void LevelFinish(int index)
        {
            //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, index.ToString("0000"));
        }
    }
    
#if GEEKON_LIONSTUDIO

    public class LionStudioProvider : IAnalytics 
    {
        public LionStudioProvider()
        {
        }

        public void CustomEvent(string id)
        {
            //GameAnalytics.NewDesignEvent(id);
        }

        public void LevelStart(int level)
        {
            LionStudios.Analytics.Events.LevelStarted(level);
        }

        public void LevelFinish(int level)
        {
            ShowAd();
            LionStudios.Analytics.Events.LevelComplete(level);
        }

        private void ShowAd()
        {
            //uncomment for ads
            
            //var request = new LionStudios.Ads.ShowAdRequest();
            //request.SetLevel(level);
            //LionStudios.Ads.Interstitial.Show(request);
        }

        public void FailLevel(int level)
        {
            LionStudios.Analytics.Events.LevelFailed(level);
        }
        
        public void Restart(int level)
        {
            LionStudios.Analytics.Events.LevelRestart(level);
        }
    }
#endif


    public interface IAnalytics
    {
        void CustomEvent(string id);
        void LevelStart(int index);
        void LevelFinish(int index);
    }
}
