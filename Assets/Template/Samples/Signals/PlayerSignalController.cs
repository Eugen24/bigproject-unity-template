using Template.Scripts.DI;
using UnityEngine;

namespace Template.Samples.Signals
{
    public class PlayerSignalController : InjectedMono
    {
        private void Start()
        {
            _signalBus.Sub<InputSignalData>(OnInputData);
        }

        private void OnInputData(InputSignalData obj)
        {
            if (obj.IsUp)
            {
                transform.position += Vector3.up * Time.deltaTime;
            }
            
            if (obj.IsDown)
            {
                transform.position += Vector3.down * Time.deltaTime;
            }
            
            if (obj.IsLeft)
            {
                transform.position += Vector3.left * Time.deltaTime;
            }
            
            if (obj.IsRight)
            {
                transform.position += Vector3.right * Time.deltaTime;
            }
        }
    }
}
