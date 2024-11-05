using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound
{
    public Vector3 position;
    public GameObject gameObject;
    public float volume;
    public int priority;
    public Sound(Vector3 origin, GameObject gameObj, float soundVolume, int soundPriority)
    {
        position = origin;
        volume = soundVolume;
        priority = soundPriority;
        gameObject = gameObj;
    }
}
public class MonsterAI : MonoBehaviour
{
    public float maxViewDistance;
    public float viewAngle;
    public float maxHearingDistance;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    [HideInInspector]
    public List<Transform> visableTargets = new List<Transform>();
    public List<Sound> heardTargets = new List<Sound>();
    void Start(){
        StartCoroutine("FindTargetsWithDelay", .2f);
    }
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            FindTargets();
        }
    }
    void FindTargets()
    {
        //------Sight------
        visableTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, maxViewDistance, targetMask);
        for(int i=0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if(!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    visableTargets.Add(target);
                }
            }
        }
    }
    /// <summary>
    /// Informs monster AI of sound
    /// </summary>
    /// <param name="position">Vector3 of the sound origin</param>
    /// <param name="volume">the volume of the sound</param>
    /// <param name="priority">the priority of the sound</param>
    /// <param name="immaKillMyself">do not change this it's always true</param>
    public void IHeardThat(Vector3 position, GameObject gameObject, float volume, int priority, bool immaKillMyself = true)
    {
        Vector3 dirToTarget = (position - transform.position).normalized;
        float distToTarget = Vector3.Distance(transform.position, position);
        Ray rayism = new Ray(transform.position, dirToTarget);
        RaycastHit[] hiter = Physics.RaycastAll(rayism, distToTarget, obstacleMask);
        //float finalVolume = (volume - (20 * (float)Math.Log(distToTarget/0.1,10))) * (float)Math.Pow(0.25, hiter.Length);
        float finalVolume = Math.Clamp((volume - (6 * (float)Math.Log(distToTarget,2))) * (float)Math.Pow(0.25, hiter.Length), 0, float.MaxValue);
        if(finalVolume > 0)
        {
            heardTargets.Add(new Sound(position, gameObject, finalVolume, priority));
            Debug.LogWarning(heardTargets[0].position + "at volume " + heardTargets[0].volume + " though " + hiter.Length);
        }
    }

    // Update is called once per frame
    void Update()
    {
        heardTargets.Clear();
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