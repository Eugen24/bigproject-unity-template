using System;
using Template.Scripts.DI;
using UnityEngine;

namespace Template.Samples.DI
{
    public class PlayerMoveController : InjectedMono
    {
        [In] private InputSystem _inputSystem;
        [In] private PlayerSingle _player;

        private void Update()
        {
            if (_inputSystem.InputData.IsUp)
            {
                _player.MoveUp();
            }
            
            if (_inputSystem.InputData.IsDown)
            {
                _player.MoveDown();
            }
            
            if (_inputSystem.InputData.IsLeft)
            {
                _player.MoveLeft();
            }
            
            if (_inputSystem.InputData.IsRight)
            {
                _player.MoveRight();
            }
        }
    }
}
