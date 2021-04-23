using UnityEngine;
using UnityEngine.UI;

namespace Template.Scripts.UI.Progress
{
    public class UiProgressViewBar : MonoBehaviour
    {
        [SerializeField] private Text _currentLevelText;
        [SerializeField] private Text _nextLevelText;
        [SerializeField] private RectTransform _bar;
        private float _totalBarLength;

        private void Awake()
        {
            var rect = _bar.rect;
            _totalBarLength = rect.max.x - rect.min.x;
        }
        
        public void UpdateData(UiProgressData data)
        {
            var x = _totalBarLength * data.ProgressProc;
            _bar.sizeDelta = new Vector2(x, _bar.sizeDelta.y);
            _currentLevelText.text = data.CurrentLevel.ToString();
            _nextLevelText.text = data.NextLevel.ToString();
        }
        
    }
}
