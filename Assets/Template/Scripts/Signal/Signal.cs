using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Template.Scripts.Signal
{
    public sealed class SignalBus : IDisposable
    {
        private readonly Dictionary<Type, object> _dictionary = new Dictionary<Type, object>(); 
        public SignalBus Sub<T>(Action<T> callBack) where T : struct
        {
            GetList<T>().Add(callBack);
            return this;
        }

        public SignalBus Fire<T>(T data) where T : struct
        {
            var l = GetList<T>();
            foreach (var obj in l)
            {
#if UNITY_EDITOR == false
                try
                {
#endif
                    if (obj != null && obj.Target != null)
                    {

                        obj.Invoke(data);
                    }
#if UNITY_EDITOR == false
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e.StackTrace);
                }
#endif
            }
            return this;
        }

        public SignalBus UnSub<T>(Action<T> callBack) where T : struct
        {
            GetList<T>().Remove(callBack);
            return this;
        }

        private List<Action<T>> GetList<T>()
        {
            if (_dictionary.ContainsKey(typeof(T)) == false)
            {
                var l = new List<Action<T>>();
                _dictionary.Add(new TypeDelegator(typeof(T)), l);
                return l;
            }

            return (List<Action<T>>) _dictionary[typeof(T)];
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public void Dispose()
        {
            Clear();
        }
    }
}
