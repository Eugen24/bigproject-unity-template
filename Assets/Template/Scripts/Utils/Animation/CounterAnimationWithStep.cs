using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Template.Scripts.Utils.Animation
{
    public class CounterAnimationWithStep
    {
        private readonly int _start;
        private readonly int _end;
        private readonly int _time;
        private readonly StringBuilder _builder;
        private readonly Text _text;
        
        public CounterAnimationWithStep(int start, int end, int step, Text text)
        {
            _start = start;
            _end = end;
            _time = step;
            _text = text;
            _builder = new StringBuilder();
        }

        public void UpdateStep()
        {
        }
    }
}
