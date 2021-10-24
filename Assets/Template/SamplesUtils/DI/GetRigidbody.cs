using Template.Scripts.DI;
using UnityEngine;

namespace Template.Samples.DI
{
    public class GetRigidbody : InjectedMono
    {
        [Get] private Rigidbody _rigidbody;

        private void Start()
        {
            Debug.Log(_rigidbody.name);
        }
    }
}
