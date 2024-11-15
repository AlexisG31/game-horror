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
        GameObject manager = GameObject.FindWithTag("soundMGR");
        SoundManager soundManager = manager.GetComponent<SoundManager>();
        soundManager.OnSoundLaunchTime += SoundManager_OnSoundLaunchTime;
        //StartCoroutine("Darion", .5f);
    }
    void SoundManager_OnSoundLaunchTime(object sender, EventArgs e)
    {
        StartCoroutine("DelayedDarion");
        if(Time.time >= timeToDestroy)
        {
            Debug.LogError("Destroyed sound");
            Destroy(this);
        }
    }
    IEnumerator DelayedDarion()
    {
        yield return 0;
        DarionIsRacist();
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
