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
}
