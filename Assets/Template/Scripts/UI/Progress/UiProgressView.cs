using Template.Scripts.Signal;
using UnityEngine;

namespace Template.Scripts.UI.Progress
{
    public class UiProgressView : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private UiProgressViewBar _viewBar;

        private void Awake()
        {
            //Signal<UiProgressData>.Event += SignalOnEvent;
        }
        private void OnDestroy()
        {
            //Signal<UiProgressData>.Event -= SignalOnEvent;
        }

        private void SignalOnEvent(UiProgressData data)
        {
            _panel.SetActive(data.IsActiveView);
            _viewBar.UpdateData(data);
        }
    }


    [SerializeField]
    public struct UiProgressData
    {
        public bool IsActiveView;
        public float ProgressProc;
        public int CurrentLevel;
        public int NextLevel;
    }
}
