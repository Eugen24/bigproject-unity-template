using Template.Scripts.DI;
using Template.Scripts.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Template.Scripts.UI.WinLose
{
    public class FinishView : InjectedMono
    {
        [SerializeField] private GameObject _earn;
        [SerializeField] private Text _earnText;
        [SerializeField] private GameObject _view;
        [SerializeField] private UnityEvent _onPress;

        public override void OnSyncStart()
        {
            _view.SetActive(false);
        }

        public void Show(bool showEarn, int amountEarn)
        {
            _view.SetActive(true);
            if (_earn != null)
                _earn.SetActive(showEarn);
            if (_earnText != null)
                _earnText.text = amountEarn.ToKMB();
        }
        
        public void OnCollect()
        {
            _onPress.Invoke();
        }
    }
}
