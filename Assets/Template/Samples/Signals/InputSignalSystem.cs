using Template.Scripts.DI;
using UnityEngine;

namespace Template.Samples.Signals
{
    public class InputSignalSystem : InjectedMono
    {
        private void Update()
        {
            if (Input.anyKey)
            {
                var signalData = new InputSignalData();
                signalData.IsUp = Input.GetKey(KeyCode.W);
                signalData.IsDown = Input.GetKey(KeyCode.S);
                signalData.IsLeft = Input.GetKey(KeyCode.A);
                signalData.IsRight = Input.GetKey(KeyCode.D);
                _signalBus.Fire<InputSignalData>(signalData);
            }
        }
    }
    
    public struct InputSignalData
    {
        public bool IsUp { get; internal set; }
        public bool IsDown { get; internal set; }
        public bool IsRight { get; internal set; }
        public bool IsLeft { get; internal set; }
    }
}
