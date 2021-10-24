using Template.Scripts.Utils;
using UnityEngine;

namespace Template.Samples.Pool
{
    public class PoolObjectMono : MonoBehaviour, IPoolObject
    {
        public void OnEnterPool()
        {
            //fake destroy
            gameObject.SetActive(false);
        }

        public void OnExitPool()
        {
            //fake init
            gameObject.SetActive(true);
        }
    }
}
