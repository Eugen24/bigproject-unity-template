using Template.Scripts.Levels;
using Template.Scripts.Signal;
using UnityEngine;

namespace Template.Scripts.DI
{
    public abstract class InjectedMono : MonoBehaviour
    {
        [In] protected SignalBus _signalBus;
        [In] protected SceneProgression _sceneProgression;
        private void Awake()
        {
            MonoDI.Instance.FixDependencies(this);
        }

        public virtual void OnSyncStart()
        {
            
        }

        public virtual void OnSyncAfterStart()
        {
            
        }
    }
}
