using System;
using System.Collections;
using Template.Scripts.DI;
using UnityEngine;

namespace Template.Scripts.UI.StartMenu
{
    public class StartMenu : InjectedMono
    {
        [SerializeField] private GameObject _view;

        private IEnumerator Start()
        {
            yield return null;
            _view.SetActive(true);
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                _signalBus.Fire<OnStartLevel>(new OnStartLevel());
                //UIMenus.instance.OpenCloseMenu("Weapons");
                _view.SetActive(false);
                gameObject.SetActive(false);
            }
        }
        
        public struct OnStartLevel
        {
            
        }
    }
}
