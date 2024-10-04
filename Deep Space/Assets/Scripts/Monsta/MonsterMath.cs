using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMath : MonoBehaviour
{
    /// <summary>
    /// Returns Vector3 direction of given angle
    /// </summary>
    /// <param name="angle">angle in degrees (unit circle way)</param>
    /// <returns>Vector3</returns>
    Vector3 dirFromAngle(float angle)
    {
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad),0,Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}
