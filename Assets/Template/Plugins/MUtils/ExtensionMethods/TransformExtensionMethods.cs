using System;
using System.Collections;
using UnityEngine;

namespace MUnityUtils.ExtensionMethods
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Get distance to transform
        /// </summary>
        /// <param name="transform">This transform.</param>
        /// <param name="toTransform">Target transform.</param>
        public static float GetDistanceTo(this Transform transform, Transform toTransform)
        {
            return Vector3.Distance(transform.position, toTransform.position);
        }
        /// <summary>
        /// Get direction to position
        /// </summary>
        /// <param name="transform">This transform.</param>
        /// <param name="toVector">Target transform.</param>
        public static Vector3 GetDirectionTo(this Transform transform, Vector3 target)
        {
            var heading = target - transform.position;
            var direction = heading / heading.magnitude;
            return direction;
        }
        /// <summary>
        /// Get direction to another transform
        /// </summary>
        /// <param name="transform">This transform.</param>
        /// <param name="toTransform">Target transform.</param>
        public static Vector3 GetDirectionTo(this Transform transform, Transform toTransform)
        {
            var heading = toTransform.position - transform.position;
            var direction = heading / heading.magnitude;
            return direction;
        }
        /// <summary>
        /// Makes the given game objects children of the transform.
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="children">Game objects to make children.</param>
        public static void AddChildren(this Transform transform, GameObject[] children)
        {
            Array.ForEach(children, child => child.transform.parent = transform);
        }
        /// <summary>
        /// Makes the game objects of given components children of the transform.
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="children">Components of game objects to make children.</param>
        public static void AddChildren(this Transform transform, Component[] children)
        {
            Array.ForEach(children, child => child.transform.parent = transform);
        }
        /// <summary>
        /// Sets the position of a transform's children to zero.
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="recursive">Also reset ancestor positions?</param>
        public static void ResetChildPositions(this Transform transform, bool recursive = false)
        {
            foreach (Transform child in transform)
            {
                child.position = Vector3.zero;

                if (recursive)
                {
                    child.ResetChildPositions(recursive);
                }
            }
        }
        /// <summary>
        /// Sets the layer of the transform's children.
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="layerName">Name of layer.</param>
        /// <param name="recursive">Also set ancestor layers?</param>
        public static void SetChildLayers(this Transform transform, string layerName, bool recursive = false)
        {
            var layer = LayerMask.NameToLayer(layerName);
            SetChildLayersHelper(transform, layer, recursive);
        }
        /// <summary>
        /// Add to x component of the transform's position.
        /// </summary>
        /// <param name="x">Value of x.</param>
        public static void AddX(this Transform transform, float x)
        {
            transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
        }
        /// <summary>
        /// Add to y component of the transform's position.
        /// </summary>
        /// <param name="y">Value of y.</param>
        public static void AddY(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + y, transform.position.z);
        }
        /// <summary>
        /// Add to z component of the transform's position.
        /// </summary>
        /// <param name="z">Value of z.</param>
        public static void AddZ(this Transform transform, float z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + z);
        }
        /// <summary>
        /// Sets the x component of the transform's position.
        /// </summary>
        /// <param name="x">Value of x.</param>
        public static void SetPositionX(this Transform transform, float x)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        /// <summary>
        /// Sets the y component of the transform's position.
        /// </summary>
        /// <param name="y">Value of y.</param>
        public static void SetPositionY(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        /// <summary>
        /// Sets the z component of the transform's position.
        /// </summary>
        /// <param name="z">Value of z.</param>
        public static void SetPositionZ(this Transform transform, float z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }
        /// <summary>
        /// Sets the x component of the transform's scale.
        /// </summary>
        /// <param name="x">Value of x.</param>
        public static void SetScaleX(this Transform trans, float value)
        {
            trans.localScale = new Vector3(value, trans.localScale.y, trans.localScale.z);
        }
        /// <summary>
        /// Sets the y component of the transform's scale.
        /// </summary>
        /// <param name="y">Value of y.</param>
        public static void SetScaleY(this Transform trans, float value)
        {
            trans.localScale = new Vector3(trans.localScale.x, value, trans.localScale.z);
        }
        /// <summary>
        /// Sets the z component of the transform's scale.
        /// </summary>
        /// <param name="z">Value of z.</param>
        public static void SetScaleZ(this Transform trans, float value)
        {
            trans.localScale = new Vector3(trans.localScale.x, trans.localScale.y, value);
        }
        /// <summary>
        /// Sets the x component of the transform's local position.
        /// </summary>
        /// <param name="x">Value of x.</param>
        public static void SetLocalPositionX(this Transform trans, float value)
        {
            trans.localPosition = new Vector3(value, trans.localPosition.y, trans.localPosition.z);
        }
        /// <summary>
        /// Sets the y component of the transform's local position.
        /// </summary>
        /// <param name="y">Value of y.</param>
        public static void SetLocalPositionY(this Transform trans, float value)
        {
            trans.localPosition = new Vector3(trans.localPosition.x, value, trans.localPosition.z);
        }
        /// <summary>
        /// Sets the z component of the transform's local position.
        /// </summary>
        /// <param name="z">Value of z.</param>
        public static void SetLocalPositionZ(this Transform trans, float value)
        {
            trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, value);
        }
        /// <summary>
        /// Set transform euler angles X
        /// </summary>
        /// <param name="trans">Transform</param>
        /// <param name="value">X value</param>
        public static void SetEulerAnglesX(this Transform trans, float value)
        {
            trans.eulerAngles = new Vector3(value, trans.eulerAngles.y, trans.eulerAngles.z);
        }
        /// <summary>
        /// Set transform euler angles Y
        /// </summary>
        /// <param name="trans">Transform</param>
        /// <param name="value">Y value</param>
        public static void SetEulerAnglesY(this Transform trans, float value)
        {
            trans.eulerAngles = new Vector3(trans.eulerAngles.x, value, trans.eulerAngles.z);
        }
        /// <summary>
        /// Set transform euler angles Z
        /// </summary>
        /// <param name="trans">Transform</param>
        /// <param name="value">Z value</param>
        public static void SetEulerAnglesZ(this Transform trans, float value)
        {
            trans.eulerAngles = new Vector3(trans.eulerAngles.x, trans.eulerAngles.y, value);
        }
        /// <summary>
        /// Resets the position of this transform
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static Transform ResetPos(this Transform transform)
        {
            transform.position = Vector3.zero;
            return transform;
        }

        /// <summary>
        /// Resets the rotation of this transform
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static Transform ResetRot(this Transform transform)
        {
            transform.rotation = Quaternion.identity;
            return transform;
        }

        /// <summary>
        /// Resets the local scale of this transform
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static Transform ResetScale(this Transform transform)
        {
            transform.localScale = Vector3.one;
            return transform;
        }

        /// <summary>
        /// Resets the position, rotation and scale of this transform
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static Transform Reset(this Transform transform)
        {
            transform.ResetPos();
            transform.ResetRot();
            transform.ResetScale();
            return transform;
        }

        /// <summary>
        /// Resets the position and rotation of this transform
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static Transform ResetPosRot(this Transform transform)
        {
            transform.ResetPos();
            transform.ResetRot();
            return transform;
        }

        private static void SetChildLayersHelper(IEnumerable transform, int layer, bool recursive)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.layer = layer;

                if (recursive)
                {
                    SetChildLayersHelper(child, layer, recursive);
                }
            }
        }
    }
}