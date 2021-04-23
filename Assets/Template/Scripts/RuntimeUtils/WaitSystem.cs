using System.Collections;
using UnityEngine;

namespace Template.Scripts.RuntimeUtils
{
    public class WaitSystem : MonoBehaviour
    {
        public Coroutine WaitRoutine(float seconds, System.Action callBack)
        {
            return StartCoroutine(WaitRoutine_internal(seconds, callBack));
        }
        
        public Coroutine WaitFrameRoutine(System.Action callBack)
        {
            return StartCoroutine(WaitFrameRoutine_internal(callBack));
        }

        private IEnumerator WaitRoutine_internal(float seconds, System.Action callBack)
        {
            yield return new WaitForSeconds(seconds);
            ValidateAndInvokeCallBack(callBack);
        }
        
        private IEnumerator WaitFrameRoutine_internal(System.Action callBack)
        {
            yield return null;
            ValidateAndInvokeCallBack(callBack);
        }


        private void ValidateAndInvokeCallBack(System.Action callBack)
        {
            try
            {
                if (callBack.Target != null)
                {
                    callBack?.Invoke();
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.StackTrace);
            }
        }
    }
}
