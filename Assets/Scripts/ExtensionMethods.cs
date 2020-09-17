using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods {
    /// <summary>
    /// Checks if two vectors are within treshold distance of each other.
    /// </summary>
    public static bool WithinDistance(this Vector3 position1, Vector3 position2, float treshold) {
        return (position1 - position2).sqrMagnitude < treshold * treshold;
    }
    
    /// <summary>
    /// Checks if two vectors are within treshold distance of each other.
    /// </summary>
    public static bool WithinDistance(this Vector2 position1, Vector2 position2, float treshold) {
        return (position1 - position2).sqrMagnitude < treshold * treshold;
    }

    /// <summary>
    /// Rotates Vector by an angle on a certain axis
    /// </summary>

    public static Vector2 RotateWithAngle(this Vector2 original, float angle, Vector3 axis, bool radians = false)
    {
        if (radians)
        {
            angle *= Mathf.Rad2Deg;
        }
        
        Quaternion rotation = Quaternion.AngleAxis(angle, axis);
        return rotation * original;
    }

    public static Vector3 NoZValue(this Vector3 vector)
    {
        return new Vector3(vector.x, vector.y, 0f);
    }

    /// <summary>
    /// Get a rotation where transform.up points towards target
    /// </summary>

    public static Quaternion LookAtTargetTopDown(this Vector3 currentPosition, Vector3 target)
    {
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, target - currentPosition);
        return rotation;
    }
}
