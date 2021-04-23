using System;
using System.Collections;
using Template.Scripts.Utils;
using UnityEngine;

namespace Template.Samples.Pool
{
    public class PoolController : MonoBehaviour
    {
        public PoolObjectMono Prefab;

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                var obj = Pool<PoolObjectMono>.GetFromPool(() => Instantiate(Prefab));
                StartCoroutine(DestroyObjectAfterTime(obj));
            }
        }

        private IEnumerator DestroyObjectAfterTime(PoolObjectMono obj)
        {
            yield return new WaitForSeconds(2f);
            Pool<PoolObjectMono>.SetInPool(obj);
        }

        private void OnDestroy()
        {
            Pool<PoolObjectMono>.ForceClearAll();
        }
    }
}
