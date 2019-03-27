using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour {

    public bool isAvailable = true; // Can a tower be placed here
    public Transform pivotPoint; // Where to place the tower on this object

    // Use this for initialization
    
    /// <summary>
    /// Returns the point point attached to the tile (if any)
    /// </summary>
    /// <returns>Returns placeable position if no pivot is made</returns>
    public Vector3 GetPivotPoint()
    {
        // If there is no pivot point on the placeable object
        if (pivotPoint == null)
        {
            // Then simply return the transform position of said placeable object
            return transform.position;
        }
        
        // Otherwise, there is a pivot point on it, so return the position of the pivot point
        return pivotPoint.position;
    }

}
