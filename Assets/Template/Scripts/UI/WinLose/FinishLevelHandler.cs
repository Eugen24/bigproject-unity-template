using NaughtyAttributes;
using Template.Scripts.DI;
using Template.Scripts.Monetization;
using UnityEngine;

namespace Template.Scripts.UI.WinLose
{
    public class FinishLevelHandler : InjectedMono
    {
        [In] private MoneySystem _money;
        [SerializeField] private FinishView _success;
        [SerializeField] private FinishView _fail;

        public override void OnSyncStart()
        {
            _signalBus.Sub<FinishSignal>(OnFinishLevel);
        }

        private void OnFinishLevel(FinishSignal obj)
        {
            if (obj.IsWin)
            {
                _money.Add(obj.AmountOfPoints);
                _success.Show(obj.ShowPoints, obj.AmountOfPoints);
            }
            else
            {
                _fail.Show(false, obj.AmountOfPoints);
            }
        }

#if UNITY_EDITOR
        [Button]
        private void OnSuccess()
        {
            _signalBus.Fire<FinishSignal>(new FinishSignal {AmountOfPoints = 20, IsWin = true, ShowPoints = true});
        }
        
        [Button]
        private void OnFail()
        {
            _signalBus.Fire<FinishSignal>(new FinishSignal {AmountOfPoints = 20, IsWin = false, ShowPoints = true});
        }
#endif

        public void OnPressReplay()
        {
            _sceneProgression.ReloadScene();
        }

        public void OnPressNext()
        {
            _sceneProgression.LoadNextScene(true);
        }

        public struct FinishSignal
        {
            public bool IsWin;
            public int AmountOfPoints;
            public bool ShowPoints;
        }
    }
}
