using System.Collections;
using NaughtyAttributes;
using Template.Scripts.DI;
using Template.Scripts.RuntimeUtils;
using UnityEngine;
using UnityEngine.UI;

namespace Template.Scripts.UI.Tutorial
{
    public class ClickTutoralUi : InjectedMono
    {
        [SerializeField] private RectTransform _view;
        [SerializeField] private Animator _animator1;

        [In] private WaitSystem _waitSystem;

        public override void OnSyncStart()
        {
            StopAnimation();
            _signalBus.Sub<ClickTutorialSignal>(OnTutorialSignal);
            _signalBus.Sub<ClickTutorialAnchorPositionSignal>(OnTutorialPos);
        }

        private void OnTutorialPos(ClickTutorialAnchorPositionSignal obj)
        {
            _view.anchoredPosition = obj.Position;
            Show(obj.IsActive);
        }

        private void OnTutorialSignal(ClickTutorialSignal obj)
        {
            Show(obj.IsActive);
        }

#if UNITY_EDITOR
        [Button]
        public void Show_Editor()
        {
            Show(true);
        }

        [Button]
        public void Hide_Editor()
        {
            Show(false);
        }
#endif

        private void Show(bool value)
        {
            if (value)
            {
                StartAnimation();
            }
            else
            {
                StopAnimation();
            }
        }

        private Coroutine _coroutine;
        private void StartAnimation()
        {
            _view.gameObject.SetActive(true);

         
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = _waitSystem.WaitRoutine(0.5f, () =>
            {
                _animator1.gameObject.SetActive(true);
                _animator1.enabled = true;
            });
        }

        private void StopAnimation()
        {
            _view.gameObject.SetActive(false);
            _animator1.enabled = false;
            
            _animator1.gameObject.SetActive(false);
        }
        

        public struct ClickTutorialSignal
        {
            public bool IsActive;
        }

        public struct ClickTutorialAnchorPositionSignal
        {
            public bool IsActive;
            public Vector3 Position;
        }
        
    }
}
