using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public EventHandler OnSoundLaunchTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("myHomeAddress", .5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator myHomeAddress(float delay)
    {
        while(true)
        {
            mySSN();
            yield return new WaitForSeconds(delay);
        }
    }
    void mySSN()
    {
        OnSoundLaunchTime?.Invoke(this, EventArgs.Empty);
    }
}
