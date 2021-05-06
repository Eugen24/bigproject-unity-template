using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MUnityUtils.ExtensionMethods
{
    public static class ListExtensionMethods
    {
        /// <summary>
        /// Sort gameobject list by distance
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="mesureFrom"></param>
        /// <returns></returns>
        public static List<GameObject> SortByDistance(this List<GameObject> objects, Vector3 mesureFrom)
        {
            return objects.OrderBy(x => Vector3.Distance(x.transform.position, mesureFrom)).ToList();
        }
        /// <summary>
        /// Get random item from list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">List</param>
        /// <returns>Random item</returns>
        public static T GetRandomItem<T>(this IList<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
        /// <summary>
        /// Move an item from one index to another
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">List</param>
        /// <param name="oldIndex">Old index</param>
        /// <param name="newIndex"> New index</param>
        public static void Move<T>(this List<T> list, int oldIndex, int newIndex)
        {
            T item = list[oldIndex];
            list.RemoveAt(oldIndex);
            if (newIndex > oldIndex)
                newIndex--;
            list.Insert(newIndex, item);
        }
        /// <summary>
        /// Move an item from one index to another
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">List</param>
        /// <param name="item">Item</param>
        /// <param name="newIndex"> New index</param>
        public static void Move<T>(this List<T> list, T item, int newIndex)
        {
            var oldIndex = list.IndexOf(item);
            list.RemoveAt(oldIndex);
            if (newIndex > oldIndex)
                newIndex--;
            list.Insert(newIndex, item);
        }
        /// <summary>
        /// Remove and return random item from list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">List</param>
        /// <returns>Removed item</returns>
        public static T RemoveRandomItem<T>(this IList<T> list)
        {
            var index = UnityEngine.Random.Range(0, list.Count);
            var item = list[index];
            list.RemoveAt(index);
            return item;
        }
        /// <summary>
        /// Sort Vector3 List on x
        /// </summary>
        /// <param name="data">List.</param>
        /// <returns>Sorted list</returns>
        public static List<Vector3> SortListV3X(this List<Vector3> data)
        {
            return data.Count < 2 ? data : data.OrderBy(v => v.x).ToArray<Vector3>().ToList();
        }
        /// <summary>
        /// Sort Vector3 List on y
        /// </summary>
        /// <param name="data">List.</param>
        /// <returns>Sorted list</returns>
        public static List<Vector3> SortListV3Y(this List<Vector3> data)
        {
            return data.Count < 2 ? data : data.OrderBy(v => v.y).ToArray<Vector3>().ToList();
        }
        /// <summary>
        /// Sort Vector3 List on Z
        /// </summary>
        /// <param name="data">List.</param>
        /// <returns>Sorted list</returns>
        public static List<Vector3> SortListV3Z(this List<Vector3> data)
        {
            return data.Count < 2 ? data : data.OrderBy(v => v.z).ToArray<Vector3>().ToList();
        }
        /// <summary>
        /// Shuffles a list
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        public static void ShuffleList<T>(this List<T> list)
        {
            var rng = new System.Random();
            var count = list.Count;
            while (count > 1)
            {
                count--;
                var k = rng.Next(count + 1);
                var value = list[k];
                list[k] = list[count];
                list[count] = value;
            }
        }
    }
}