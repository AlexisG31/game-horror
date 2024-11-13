using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent monster;
    public float maxViewDistance;
    public float viewAngle;
    public float maxHearingDistance;
    private bool randomDestination = false;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    [HideInInspector]
    public List<Transform> visableTargets = new List<Transform>();
    public List<Sound> heardTargets = new List<Sound>();
    public List<GameObject> patrolDestination = new List<GameObject>();

    void Start()
    {
        StartCoroutine("EssentialOperationsWithDelay", .2f);
    }
    void Update()
    {
        Transform goTo = setTarget();
        if(goTo != null)
        {
            monster.SetDestination(goTo.position);
        }
        heardTargets.Clear();
    }
    Transform setTarget()
    {
        Sound soundToFollow;
        if(visableTargets.Count > 0)
        {
            randomDestination = false;
            return visableTargets[0];
        }
        else if(heardTargets.Count > 0)
        {
            soundToFollow = heardTargets[0];
            foreach (Sound sound in heardTargets)
            {
                if(sound.priority > soundToFollow.priority)
                {
                    soundToFollow = sound;
                }
                else if(sound.priority == soundToFollow.priority && sound.volume > soundToFollow.volume)
                {
                    soundToFollow = sound;
                }
            }
            randomDestination = false;
            return soundToFollow.gameObject.transform;
        }
        else
        {
            if(randomDestination == false)
            {
                int randomPoint = UnityEngine.Random.Range(0,patrolDestination.Count);
                randomDestination = true;
                return patrolDestination[randomPoint].transform;
            }
        }
        return null;
    }
    IEnumerator EssentialOperationsWithDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            SightCheck();
        }
    }
    void SightCheck()
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
    public void IHeardThat(Vector3 position, GameObject gameObject, float volume, int priority)
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