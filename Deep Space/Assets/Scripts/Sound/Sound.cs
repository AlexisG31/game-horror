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
