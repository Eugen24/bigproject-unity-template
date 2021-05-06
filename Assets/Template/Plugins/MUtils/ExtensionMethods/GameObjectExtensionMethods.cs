using UnityEngine;
namespace MUnityUtils.ExtensionMethods
{
    public static class GameObjectExtensionMethods
    {
        /// <summary>
        /// Gets a component attached to the given game object.
        /// If one isn't found, a new one is attached and returned.
        /// </summary>
        /// <param name="gameObject">Game object.</param>
        /// <returns>Previously or newly attached component.</returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
        }

        /// <summary>
        /// Checks whether a game object has a component of type T attached.
        /// </summary>
        /// <param name="gameObject">Game object.</param>
        /// <returns>True when component is attached.</returns>
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>() != null;
        }
        /// <summary>
        /// Destroys all children recursively of a GameObject
        /// </summary>
        /// <param name="parent">Parent</param>
        public static void DestroyChildrens(this GameObject parent)
        {
            var children = new Transform[parent.transform.childCount];
            for (var i = 0; i < parent.transform.childCount; i++)
                children[i] = parent.transform.GetChild(i);
            foreach (var t in children)
                GameObject.Destroy(t.gameObject);
        }
        /// <summary>
        /// Children of one GameObject gets transferred to another GameObject
        /// </summary>
        /// <param name="from">From gameobject</param>
        /// <param name="to">Target gameobject</param>
        public static void MoveChildrens(this GameObject from, GameObject to)
        {
            var children = new Transform[from.transform.childCount];
            for (var i = 0; i < from.transform.childCount; i++)
                children[i] = from.transform.GetChild(i);
            for (var i = 0; i < children.Length; i++)
                children[i].SetParent(to.transform);
        }
    }
}
