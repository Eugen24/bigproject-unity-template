using Template.Scripts.DI;
using Template.Scripts.UI.StartMenu;
using Template.Scripts.UI.WinLose;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class Example : InjectedMono
    {
        [SerializeField] private Text _text;

        public override void OnSyncStart()
        {
            _signalBus.Sub<StartMenu.OnStartLevel>(OnStartLevel);
            _signalBus.Sub<FinishLevelHandler.FinishSignal>(OnFinishLevel);
        }

        private void OnFinishLevel(FinishLevelHandler.FinishSignal obj)
        {
            if (obj.IsWin)
            {
                _text.text = "Win";
            }
            else
            {
                _text.text = "Lose";
            }
        }

        private void OnStartLevel(StartMenu.OnStartLevel obj)
        {
            _text.text = "Game Loop";
        }

        public void OnFail()
        {
            _signalBus.Fire(new FinishLevelHandler.FinishSignal
                { AmountOfPoints = 20, IsWin = false});
        }

        public void OnWin()
        {
            _signalBus.Fire(new FinishLevelHandler.FinishSignal
                { AmountOfPoints = 20, IsWin = true, ShowPoints = true});
        }
    }
}
