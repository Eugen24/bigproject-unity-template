using System.Collections.Generic;

namespace Template.Plugins.MUnityUtils.ExtensionMethods
{
    public static class QueueExtensionMethods
    {
        /// <summary>
        /// Convert list to queue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queue">Queue</param>
        /// <param name="list">List to convert</param>
        public static void ToQueue<T>(this Queue<T> queue, IList<T> list)
        {
            foreach (var obj in list)
                queue.Enqueue(obj);
        }
    }
}