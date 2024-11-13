using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceOfAudio : MonoBehaviour
{
    private LayerMask monsterMask;
    [SerializeField]
    float duration;
    public float volume;
    [SerializeField]
    int priority;
    float maxDist;
    float timeToDestroy;

    

    // Start is called before the first frame update
    void Start()
    {
        timeToDestroy = Time.time + duration;
        monsterMask = (1 << 8);
        maxDist = Math.Clamp((float)Math.Pow(2,volume/6), 0, float.MaxValue) * 0.1f;
        //StartCoroutine("Darion", .5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Darion(float delay)
    {
        while(true)
        {
            DarionIsRacist();
            if(Time.time >= timeToDestroy)
            {
                Debug.LogError("Destroyed sound");
                Destroy(this);
            }
            yield return new WaitForSeconds(delay);
        }
    }
    void DarionIsRacist()
    {
        Collider[] hearersInHearingRange = Physics.OverlapSphere(transform.position, maxDist, monsterMask);
        for(int i=0; i < hearersInHearingRange.Length; i++)
        {
            Transform target = hearersInHearingRange[i].transform;
            MonsterAI monster = target.transform.gameObject.GetComponent<MonsterAI>();
            if (monster != null)
            {
                monster.IHeardThat(gameObject.transform.position, this.gameObject, volume, priority);
            }
        }
        
    }
}
