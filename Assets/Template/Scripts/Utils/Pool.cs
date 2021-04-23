using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Template.Scripts.Utils
{
    public static class Pool <T> where T : IPoolObject
    {
        private static readonly List<T> _pool = new List<T>();
        
        public static T GetFromPool(Func<T> onMissing)
        {
            if (_pool.Count == 0)
            {
                return onMissing.Invoke();
            }

            var r = _pool[0];
            _pool.RemoveAt(0);
            r.OnExitPool();
            return r;
        }

        public static void SetInPool(T obj)
        { 
            obj.OnEnterPool();
            if (_pool.Contains(obj) == false)
                _pool.Add(obj);
        }

        public static void ForceClearAll()
        {
            _pool.Clear();
        }
    }

    public static class PoolWithSO<Q, T> where T : IPoolObject  
                                         where Q : class
    {
        private static readonly List<PoolObj<Q, T>> _pool = new List<PoolObj<Q, T> >();
        
        public static T GetFromPool(Q id, System.Action onMissing)
        {
            if (_pool.Exists(_ => _.Id == id) == false)
            {
                onMissing.Invoke();
            }
            var p = _pool.Find(_ => _.Id == id).List;
            
            if (p.Count == 0)
            {
                onMissing.Invoke();
            }

            var r = p[0];
            p.RemoveAt(0);
            r.OnExitPool();
            return r;
        }

        public static void SetInPool(Q id, T obj)
        { 
            obj.OnEnterPool();
            var index = _pool.FindIndex(_ => _.Id == id);
            if (index == -1)
            {
                var l = new PoolObj<Q, T>();
                l.Id = id;
                l.List.Add(obj);
                _pool.Add(l);
            }
            else
            {
                if (_pool[index].List.Contains(obj) == false)
                    _pool[index].List.Add(obj);
            }
        }
        
        public static void ForceClearAll()
        {
            _pool.Clear();
        }

        public class PoolObj<Q, T> where T : IPoolObject  
                                   where Q : class
        {
            public Q Id = null;
            public List<T> List = new List<T>();
        }
    }
    

    public interface IPoolObject
    {
        void OnEnterPool();
        void OnExitPool();
    }
    
}
