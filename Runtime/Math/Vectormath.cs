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
            return CalculateRayIntersectionWithPlane(rayOrigin, rayDirection, Vector3.up, offset);
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
            return CalculateRayIntersectionWithPlane(rayOrigin, rayDirection, Vector3.forward, offset);
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
            return CalculateRayIntersectionWithPlane(rayOrigin, rayDirection, Vector3.right, offset);
        }

        /// <summary>
        /// Returns the IntersectionPoint of an Ray with the givven Plane
        /// </summary>
        /// <param name="rayOrigin"></param>
        /// <param name="rayDirection"></param>
        /// <param name="planeNormal">Normal of the Plane in Worldspace</param>
        /// <param name="offset">Distance between the Plane (at origin) and the intersecting Plane</param>
        /// <returns></returns>
        public static Vector3 CalculateRayIntersectionWithPlane(Vector3 rayOrigin, Vector3 rayDirection, Vector3 planeNormal, float offset)
        {
            // Ensure the ray direction is normalized
            rayDirection.Normalize();
            
            // Calculate the distance from the plane to the origin of the ray
            float distanceToPlane = Vector3.Dot(planeNormal, rayOrigin);
            
            // Adjust the distance by the offset
            distanceToPlane += offset;
            
            // Check if the ray is parallel to the plane (no intersection)
            float denominator = Vector3.Dot(planeNormal, rayDirection);
            if (Mathf.Approximately(denominator, 0))
            {
                // Ray is parallel to the plane, no intersection
                Debug.LogWarning("Ray is parallel to the plane and does not intersect");
                return Vector3.zero; // Return a default value or handle this case as needed
            }
            
            // Calculate the distance from the ray origin to the intersection point on the plane
            float distance = (distanceToPlane - Vector3.Dot(planeNormal, rayOrigin)) / denominator;
            
            // Calculate the intersection point using the distance along the ray's direction
            Vector3 intersectionPoint = rayOrigin + rayDirection * distance;
            
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
