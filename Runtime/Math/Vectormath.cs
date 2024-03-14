using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGFabi_Utils.Math
{
    public class Vectormath
    {
        /// <summary>
        /// Returns the IntersectionPoint of an Ray with the XZ-Plane
        /// </summary>
        /// <param name="rayOrigin"></param>
        /// <param name="rayDirection"></param>
        /// <param name="offset">Distance between the XZ-Plane and the intersecting Plane</param>
        /// <returns></returns>
        public static Vector3 CalculateRayIntersectionWithXZPlane(Vector3 rayOrigin, Vector3 rayDirection, float offset = 0)
        {
            // Ensure the ray direction is normalized
            rayDirection.Normalize();

            // The XZ plane is defined as y = 0, so its normal is (0, 1, 0)
            Vector3 planeNormal = Vector3.up; // Equivalent to new Vector3(0, 1, 0)

            // Check if the ray is parallel to the plane (no intersection)
            float denominator = Vector3.Dot(planeNormal, rayDirection);
            if (Mathf.Approximately(denominator, 0))
            {
                // Ray is parallel to the XZ plane, no intersection
                Debug.LogWarning("Ray is parallel to the XZ plane and does not intersect");
                return Vector3.zero; // Return a default value or handle this case as needed
            }

            // Calculate the distance from the ray origin to the intersection point on the XZ plane
            float distance = -Vector3.Dot(planeNormal, rayOrigin) / denominator;

            // Calculate the intersection point using the distance along the ray's direction
            Vector3 intersectionPoint = rayOrigin + new Vector3(0, offset, 0) + rayDirection * distance;

            return intersectionPoint;
        }

        /// <summary>
        /// Returns the IntersectionPoint of an Ray with the XY-Plane
        /// </summary>
        /// <param name="rayOrigin"></param>
        /// <param name="rayDirection"></param>
        /// <param name="offset">Distance between the XY-Plane and the intersecting Plane</param>
        /// <returns></returns>
        public static Vector3 CalculateRayIntersectionWithXYPlane(Vector3 rayOrigin, Vector3 rayDirection, float offset = 0)
        {
            // Ensure the ray direction is normalized
            rayDirection.Normalize();

            // The XY plane is defined as z = 0, so its normal is (0, 0, 1)
            Vector3 planeNormal = Vector3.forward; 

            // Check if the ray is parallel to the plane (no intersection)
            float denominator = Vector3.Dot(planeNormal, rayDirection);
            if (Mathf.Approximately(denominator, 0))
            {
                // Ray is parallel to the XZ plane, no intersection
                Debug.LogWarning("Ray is parallel to the XZ plane and does not intersect");
                return Vector3.zero; // Return a default value or handle this case as needed
            }

            // Calculate the distance from the ray origin to the intersection point on the XZ plane
            float distance = -Vector3.Dot(planeNormal, rayOrigin) / denominator;

            // Calculate the intersection point using the distance along the ray's direction
            Vector3 intersectionPoint = rayOrigin + new Vector3(0, 0, offset)+ rayDirection * distance;

            return intersectionPoint;
        }

        /// <summary>
        /// Returns the IntersectionPoint of an Ray with the YZ-Plane
        /// </summary>
        /// <param name="rayOrigin"></param>
        /// <param name="rayDirection"></param>
        /// <param name="offset">Distance between the YZ-Plane and the intersecting Plane</param>
        /// <returns></returns>
        public static Vector3 CalculateRayIntersectionWithYZPlane(Vector3 rayOrigin, Vector3 rayDirection, float offset = 0)
        {
            // Ensure the ray direction is normalized
            rayDirection.Normalize();

            // The YZ plane is defined as x = 0, so its normal is (1, 0, 0)
            Vector3 planeNormal = Vector3.right; // Equivalent to new Vector3(1, 0, 0)

            // Check if the ray is parallel to the plane (no intersection)
            float denominator = Vector3.Dot(planeNormal, rayDirection);
            if (Mathf.Approximately(denominator, 0))
            {
                // Ray is parallel to the XZ plane, no intersection
                Debug.LogWarning("Ray is parallel to the XZ plane and does not intersect");
                return Vector3.zero; // Return a default value or handle this case as needed
            }

            // Calculate the distance from the ray origin to the intersection point on the XZ plane
            float distance = -Vector3.Dot(planeNormal, rayOrigin) / denominator;

            // Calculate the intersection point using the distance along the ray's direction
            Vector3 intersectionPoint = rayOrigin + new Vector3(offset, 0, 0) + rayDirection * distance;

            return intersectionPoint;
        }


        public static Vector3 FloorVector(Vector3 a)
        {
            return new Vector3(
                Mathf.Floor(a.x),
                Mathf.Floor(a.y),
                Mathf.Floor(a.z)
                );
        }
        public static Vector3Int FloorVectorToInt(Vector3 a)
        {
            return new Vector3Int(
                Mathf.FloorToInt(a.x),
                Mathf.FloorToInt(a.y),
                Mathf.FloorToInt(a.z)
                );
        }
        public static Vector2 FloorVector(Vector2 a)
        {
            return new Vector2(
                Mathf.Floor(a.x),
                Mathf.Floor(a.y)
                );
        }
        public static Vector2Int FloorVectorToInt(Vector2 a)
        {
            return new Vector2Int(
                Mathf.FloorToInt(a.x),
                Mathf.FloorToInt(a.y)
                );
        }

    }
}
