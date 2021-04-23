using Template.Scripts.DI;
using UnityEngine;
using UnityEngine.UI;

namespace Template.Scripts.UI.Helpers
{
    public class LevelText : InjectedMono
    {
        [SerializeField] private bool _usePrefix = true;
        [Get] private Text _text;
        public override void OnSyncStart()
        {
            if (_usePrefix)
            {
                _text.text = $"LEVEL {_sceneProgression.TotalFinishedScenes}";
            }
            else
            {
                _text.text = $"{_sceneProgression.TotalFinishedScenes}";
            }
        }
    }
}
