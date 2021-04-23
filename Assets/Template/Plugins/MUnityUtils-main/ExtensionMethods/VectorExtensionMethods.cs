using System.Collections.Generic;
using UnityEngine;

namespace MUnityUtils.ExtensionMethods
{
    public static class VectorExtensionMethods
    {
        /// <summary>
        /// Finds the position closest to the given one.
        /// </summary>
        /// <param name="position">World position.</param>
        /// <param name="otherPositions">Other world positions.</param>
        /// <returns>Closest position.</returns>
        public static Vector3 GetClosest(this Vector3 position, IEnumerable<Vector3> otherPositions)
        {
            var closest = Vector3.zero;
            var shortestDistance = Mathf.Infinity;

            foreach (var otherPosition in otherPositions)
            {
                var distance = (position - otherPosition).sqrMagnitude;

                if (distance < shortestDistance)
                {
                    closest = otherPosition;
                    shortestDistance = distance;
                }
            }

            return closest; 
        }
        /// <summary>
        /// Get mid point between two vectors
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Vector3 GetMidPoint(this Vector3 first, Vector3 second)
        {
            return new Vector3((first.x + second.x) * 0.5f, (first.y + second.y) * 0.5f, (first.z + second.z) * 0.5f);
        }
        /// <summary>
        /// Opposite of up
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector3 down(this Transform t)
        {
            return -t.up;
        }

        /// <summary>
        /// Opposite of right
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector3 left(this Transform t)
        {
            return -t.right;
        }

        /// <summary>
        /// Opposite of forward
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector3 backward(this Transform t)
        {
            return -t.forward;
        }
        /// <summary>
        /// Get distance to second Vector3
        /// </summary>
        /// <param name="first">First vector.</param>
        /// <param name="second">Second vector.</param>
        public static float GetDistanceTo(this Vector3 first, Vector3 second)
        {
            return Vector3.Distance(first, second);
        }
        /// <summary>
        /// Convert Vector3 to Vector2
        /// </summary>
        /// <param name="v">Vector3</param>
        /// <returns>Vector2</returns>
        public static Vector2 ToVector2(this Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }
        /// <summary>
        /// Convert Vector2 to Vector3
        /// </summary>
        /// <param name="v">Vector2</param>
        /// <param name="z">Z value</param>
        /// <returns>Vector3</returns>
        public static Vector3 ToVector3(this Vector2 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }
        /// <summary>
        /// Set Vector3 X value
        /// </summary>
        /// <param name="v">Vector3</param>
        /// <param name="x">New X value</param>
        /// <returns>New Vector3 with modified X</returns>
        public static Vector3 SetX(this Vector3 v, float x)
        {
            return new Vector3(x, v.y, v.z);
        }
        /// <summary>
        /// Set Vector3 Y value
        /// </summary>
        /// <param name="v">Vector3</param>
        /// <param name="Y">New Y value</param>
        /// <returns>New Vector3 with modified Y</returns>
        public static Vector3 SetY(this Vector3 v, float y)
        {
            return new Vector3(v.x, y, v.z);
        }
        /// <summary>
        /// Set Vector3 Z value
        /// </summary>
        /// <param name="v">Vector3</param>
        /// <param name="Z">New Z value</param>
        /// <returns>New Vector3 with modified Z</returns>
        public static Vector3 SetZ(this Vector3 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }
        /// <summary>
        /// Set Vector2 X value
        /// </summary>
        /// <param name="v">Vector2</param>
        /// <param name="X">New X value</param>
        /// <returns>New Vector2 with modified X</returns>
        public static Vector2 SetX(this Vector2 v, float x)
        {
            return new Vector2(x, v.y);
        }
        /// <summary>
        /// Set Vector2 Y value
        /// </summary>
        /// <param name="v">Vector2</param>
        /// <param name="Y">New Y value</param>
        /// <returns>New Vector2 with modified Y</returns>
        public static Vector2 SetY(this Vector2 v, float y)
        {
            return new Vector2(v.x, y);
        }
        /// <summary>
        /// Set Vector2 Z value
        /// </summary>
        /// <param name="v">Vector2</param>
        /// <param name="Z">New Z value</param>
        /// <returns>New Vector2 with modified Z</returns>
        public static Vector3 SetZ(this Vector2 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }
        /// <summary>
        /// Add to Vector3 X
        /// </summary>
        /// <param name="v">Vector3</param>
        /// <param name="X">Add value</param>
        /// <returns>New Vector3 with modified X</returns>
        public static Vector3 AddX(this Vector3 v, float x)
        {
            return new Vector3(v.x + x, v.y, v.z);
        }
        /// <summary>
        /// Add to Vector3 Y
        /// </summary>
        /// <param name="v">Vector3</param>
        /// <param name="Y">Add value</param>
        /// <returns>New Vector3 with modified Y</returns>
        public static Vector3 AddY(this Vector3 v, float y)
        {
            return new Vector3(v.x, v.y + y, v.z);
        }
        /// <summary>
        /// Add to Vector3 Z
        /// </summary>
        /// <param name="v">Vector3</param>
        /// <param name="Z">Add value</param>
        /// <returns>New Vector3 with modified Z</returns>
        public static Vector3 AddZ(this Vector3 v, float z)
        {
            return new Vector3(v.x, v.y, v.z + z);
        }
        /// <summary>
        /// Add to Vector2 X
        /// </summary>
        /// <param name="v">Vector3</param>
        /// <param name="X">Add value</param>
        /// <returns>New Vector2 with modified X</returns>
        public static Vector2 AddX(this Vector2 v, float x)
        {
            return new Vector2(v.x + x, v.y);
        }
        /// <summary>
        /// Add to Vector2 Y
        /// </summary>
        /// <param name="v">Vector3</param>
        /// <param name="Y">Add value</param>
        /// <returns>New Vector2 with modified Y</returns>
        public static Vector2 AddY(this Vector2 v, float y)
        {
            return new Vector2(v.x, v.y + y);
        }
    }
}