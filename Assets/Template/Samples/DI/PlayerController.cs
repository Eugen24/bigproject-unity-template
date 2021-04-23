using Template.Scripts.DI;
using UnityEngine;

namespace Template.Samples.DI
{
    public class PlayerController : InjectedMono
    {
        [In] private InputSystem _inputSystem;
        void Update()
        {
            if (_inputSystem.InputData.IsUp)
            {
                transform.position += Vector3.up * Time.deltaTime;
            }
            
            if (_inputSystem.InputData.IsDown)
            {
                transform.position += Vector3.down * Time.deltaTime;
            }
            
            if (_inputSystem.InputData.IsLeft)
            {
                transform.position += Vector3.left * Time.deltaTime;
            }
            
            if (_inputSystem.InputData.IsRight)
            {
                transform.position += Vector3.right * Time.deltaTime;
            }
        }
    }
}
