using UnityEngine;

namespace Template.Plugins.MUnityUtils.ExtensionMethods
{
    public static class ColliderExtensionMethods
    {
        /// <summary>
        /// Check if capsule collider is grounded
        /// </summary>
        /// <param name="collider">Capsule collider</param>
        /// <param name="slopeLimit">Max slope limit</param>
        /// <returns>True if collider is grounded</returns>
        public static bool IsGrounded(this CapsuleCollider collider, float slopeLimit = 45f)
        {
            var isGrounded = false;
            var colliderTransform = collider.transform;
            var capsuleHeight = Mathf.Max(collider.radius * 2f, collider.height);
            var capsuleBottom = colliderTransform.TransformPoint(collider.center - Vector3.up * capsuleHeight / 2f);
            var radius = colliderTransform.TransformVector(collider.radius, 0f, 0f).magnitude;
            var up = colliderTransform.up;
            var ray = new Ray(capsuleBottom + up * .01f, -up);
            if (Physics.Raycast(ray, out var hit, radius * 5f))
            {
                var normalAngle = Vector3.Angle(hit.normal, colliderTransform.up);
                if (normalAngle < slopeLimit)
                {
                    var maxDist = radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - radius + .02f;
                    if (hit.distance < maxDist)
                       isGrounded = true;
                }
            }
            return isGrounded;
        }
        /// <summary>
        /// Check if sphere collider is grounded
        /// </summary>
        /// <param name="collider">Sphere collider</param>
        /// <param name="slopeLimit">Max slope limit</param>
        /// <returns>True if collider is grounded</returns>
        public static bool IsGrounded(this SphereCollider collider, float slopeLimit = 45f)
        {
            var isGrounded = false;
            var colliderTransform = collider.transform;
            var sphereBottom = colliderTransform.TransformPoint(collider.center - Vector3.up * collider.radius);
            var radius = colliderTransform.TransformVector(collider.radius, 0f, 0f).magnitude;
            var up = colliderTransform.up;
            var ray = new Ray(sphereBottom + up * .01f, -up);
            if (Physics.Raycast(ray, out var hit, radius * 5f))
            {
                var normalAngle = Vector3.Angle(hit.normal, colliderTransform.up);
                if (normalAngle < slopeLimit)
                {
                    var maxDist = radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - radius + .02f;
                    if (hit.distance < maxDist)
                        isGrounded = true;
                }
            }
            return isGrounded;
        }
    }
}
