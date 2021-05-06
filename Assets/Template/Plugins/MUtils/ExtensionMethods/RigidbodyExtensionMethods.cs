using UnityEngine;

namespace MUnityUtils.ExtensionMethods
{
    public static class RigidbodyExtensionMethods
    {
        /// <summary>
        /// Changes the direction of a rigidbody without changing its speed.
        /// </summary>
        /// <param name="rigidbody">Rigidbody.</param>
        /// <param name="direction">New direction.</param>
        public static void ChangeDirection(this Rigidbody rigidbody, Vector3 direction)
        {
            rigidbody.velocity = direction * rigidbody.velocity.magnitude;
        }
        /// <summary>
        /// Add force rigidbody up
        /// </summary>
        /// <param name="rigidbody"></param>
        /// <param name="jumpForce">Jump speed</param>
        public static void Jump(this Rigidbody rigidbody, float jumpForce = 5f)
        {
            rigidbody.AddForce(rigidbody.transform.up * jumpForce, ForceMode.VelocityChange);
        }
        /// <summary>
        /// Freeze position
        /// </summary>
        /// <param name="rigidbody"></param>
        public static void FreezePosition(this Rigidbody rigidbody)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        }
        /// <summary>
        /// Freeze rotation
        /// </summary>
        /// <param name="rigidbody"></param>
        public static void FreezeRotation(this Rigidbody rigidbody)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }
        /// <summary>
        /// Freeze all
        /// </summary>
        /// <param name="rigidbody"></param>
        public static void FreezeAll(this Rigidbody rigidbody)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        /// <summary>
        /// Freeze position
        /// </summary>
        /// <param name="rigidbody"></param>
        public static void UnfreezeAll(this Rigidbody rigidbody)
        {
            rigidbody.constraints = RigidbodyConstraints.None;
        }
        /// <summary>
        /// Ratate rigidbody
        /// </summary>
        /// <param name="rigidbody"></param>
        /// <param name="xAngle"></param>
        /// <param name="yAngle"></param>
        /// <param name="zAngle"></param>
        /// <param name="relativeTo"></param>
        public static void Rotate(this Rigidbody rigidbody, float xAngle, float yAngle, float zAngle, Space relativeTo = Space.Self)
        {
            rigidbody.Rotate(new Vector3(xAngle, yAngle, zAngle), relativeTo);
        }
        /// <summary>
        /// Ratate rigidbody
        /// </summary>
        /// <param name="rigidbody"></param>
        /// <param name="xAngle"></param>
        /// <param name="yAngle"></param>
        /// <param name="zAngle"></param>
        /// <param name="relativeTo"></param>
        public static void Rotate(this Rigidbody rigidbody, Vector3 eulerAngles, Space relativeTo = Space.Self)
        {
            if (relativeTo == Space.Self)
            {
                eulerAngles = rigidbody.transform.TransformDirection(eulerAngles);
            }
            rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(eulerAngles));
        }
        /// <summary>
        /// Ratate rigidbody
        /// </summary>
        /// <param name="rigidbody"></param>
        /// <param name="xAngle"></param>
        /// <param name="yAngle"></param>
        /// <param name="zAngle"></param>
        /// <param name="relativeTo"></param>
        public static void Rotate(this Rigidbody rigidbody, Vector3 axis, float angle, Space relativeTo = Space.Self)
        {
            if (relativeTo == Space.Self)
            {
                axis = rigidbody.transform.TransformDirection(axis);
            }
            rigidbody.MoveRotation(rigidbody.rotation * Quaternion.AngleAxis(angle, axis));
        }
    }
}
