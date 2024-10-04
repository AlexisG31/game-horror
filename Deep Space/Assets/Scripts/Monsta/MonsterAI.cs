using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float maxViewDistance;
    public float viewAngle;
    public float peripheralVisionDistance;
    public float peripheralVisionAngle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Returns Vector3 direction of given angle
    /// </summary>
    /// <param name="angle">angle in degrees (unit circle way)</param>
    /// <param name="global">is the angle global?</param>
    /// <returns>Vector3</returns>
    public Vector3 dirFromAngle(float angle, bool global)
    {
        if(!global)
        {
            angle += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad),0,Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}
