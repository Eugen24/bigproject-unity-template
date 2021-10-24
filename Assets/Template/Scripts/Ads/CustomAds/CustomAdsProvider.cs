using System;
using System.Collections;
using Template.Scripts.DI;
using UnityEngine;

namespace Template.Scripts.Ads
{
    public class AdsSystem : MonoBehaviour
    {
        private static IAdsProvider _adsProvider;
        private void Awake()
        {
            if (_adsProvider == null)
            {
                Init();
            }
        }

        protected virtual void Init()
        {
            //here you need to add new provider
            _adsProvider = new FakeAdsProvider();
            _adsProvider.Init();
        }

        public bool IsAvailable()
        {
            return _adsProvider.IsAvailable();
        }

        private Coroutine _current;

        public void ShowAds(System.Action<bool> onFinish)
        {
            StopCoroutine(_current);
            _current = StartCoroutine(ShowAdsRoutine(onFinish));
        }

        private IEnumerator ShowAdsRoutine(System.Action<bool> onFinish)
        {
            if (IsAvailable() == false)
            {
                onFinish.Invoke(false);
                yield break;
            }
            
            _adsProvider.ShowAds(success =>
            {
                //it is used to avoid any crash from potential google-shity-like plugins that makes
                //crash because of multithreading....
                onFinish?.Invoke(success);
            });
        }
    }

    public class FakeAdsProvider : IAdsProvider
    {
        public void Init()
        {
            
        }

        public bool IsAvailable()
        {
            return true;
        }

        public void ShowAds(Action<bool> onFinish)
        {
            onFinish.Invoke(true);
        }
    }


    public interface IAdsProvider
    {
        void Init();
        bool IsAvailable();
        void ShowAds(System.Action<bool> onFinish);
    }
}
