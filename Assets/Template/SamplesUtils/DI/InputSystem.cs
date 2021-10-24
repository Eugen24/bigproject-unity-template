using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template.Samples.DI
{
    public class InputSystem : MonoBehaviour
    {
        public InputData InputData { get; private set; }

        private void Start()
        {
            InputData = new InputData();
        }

        private void Update()
        {
            InputData.IsUp = Input.GetKey(KeyCode.W);
            InputData.IsDown = Input.GetKey(KeyCode.S);
            InputData.IsRight = Input.GetKey(KeyCode.D);
            InputData.IsLeft = Input.GetKey(KeyCode.A);
        }
    }

    public class InputData
    {
        public bool IsUp { get; internal set; }
        public bool IsDown { get; internal set; }
        public bool IsRight { get; internal set; }
        public bool IsLeft { get; internal set; }
    }
}