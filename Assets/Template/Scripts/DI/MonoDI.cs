using System;
using System.Collections.Generic;
using Template.Scripts.Signal;
using Template.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Template.Scripts.DI
{
    public class MonoDI : MonoBehaviour
    {
        private static MonoDI _instance;
        private readonly List<System.Object> _toInject = new List<System.Object>();
        private readonly GeneralInstaller _generalInstaller = new GeneralInstaller();
        private readonly List<InjectedMono> _injectOnStart = new List<InjectedMono>();
        private bool _wasStart = false;
        
        public static MonoDI Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = FindObjectOfType<MonoDI>();
                _instance.Init();
                return _instance;
            }
        }
        private void Init()
        {
            var r = gameObject.GetComponentsInChildren<MonoBehaviour>();
            foreach (var behaviour in r)
            {
                _toInject.Add(behaviour);
            }

            var assertSingle = gameObject.GetComponentInChildren<SingleMono>();
            if (assertSingle != null)
            {
                Debug.LogError("SingleMono should not be put in System.prefab, please use regular MonoBehaviour!");
            }
            
            var singles = GameObjectUtils.Find<SingleMono>();
            foreach (var single in singles)
            {
                _toInject.Add(single);
            }
            
            _toInject.Add(new SignalBus());
        }

        private void Start()
        {
            foreach (var injectedMono in _injectOnStart)
            {
                injectedMono.OnSyncStart();
            }
            
            foreach (var injectedMono in _injectOnStart)
            {
                injectedMono.OnSyncAfterStart();
            }

            _wasStart = true;
            _injectOnStart.Clear();
        }

        /// <summary>
        /// Add Injection to a specific object
        /// </summary>
        /// <param name="injectedMono"></param>
        public void FixDependencies(InjectedMono injectedMono)
        {
            _generalInstaller.Inject(injectedMono, _toInject);
            if (_wasStart)
            {
                injectedMono.OnSyncStart();
                injectedMono.OnSyncAfterStart();
            }
            else
            {
                _injectOnStart.Add(injectedMono);
            }
        }
    }
}
