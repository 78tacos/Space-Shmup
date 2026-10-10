using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{

    /// <summary>
    /// Bezier interpolation for any number of control points.
    /// </summary>
    /// <param name="u">The amount of interpolation [0..1]</param>
    /// <param name="points">An array of Vector3s to interpolate</param>
    static public Vector3 Bezier( float u, params Vector3[] points ) {
        Vector3[,] vArr = new Vector3[points.Length, points.Length];
        // Last row is the control points; each earlier row lerps its neighbors.
        int r = points.Length - 1;
        for ( int c = 0; c < points.Length; c++) {
            vArr[r, c] = points[c];        
        }

        for ( r--; r >= 0; r--) {
            for ( int c = 0; c <= r; c++) {
                vArr[r,c] = Vector3.LerpUnclamped(vArr[r+1,c], vArr[r+1,c+1], u);
            }
        }

        // When complete, vArr[0,0] holds the final interpolated value
        return vArr[0, 0];
    }
    
}
