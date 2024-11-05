using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

[CustomEditor(typeof(SourceOfAudio))]
public class SoundVisualization : Editor
{
    void OnSceneGUI(){
        SourceOfAudio sound = (SourceOfAudio)target;
        Handles.color = Color.yellow;
        Handles.DrawWireArc(sound.transform.position, Vector3.up, Vector3.forward, 360, (float)Math.Pow(10,sound.volume/20) * 0.1f);
    }
}
