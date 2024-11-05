using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

[CustomEditor(typeof(MonsterAI))]
public class MonsterFOVEditor : Editor
{
    void OnSceneGUI(){
        MonsterAI Monster = (MonsterAI)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(Monster.transform.position, Vector3.up, Vector3.forward, 360, Monster.maxViewDistance);
        Vector3 viewAngleA = Monster.dirFromAngle (-Monster.viewAngle / 2, false);
        Vector3 viewAngleB = Monster.dirFromAngle (Monster.viewAngle / 2, false);
        Handles.DrawLine(Monster.transform.position, Monster.transform.position + viewAngleA * Monster.maxViewDistance);
        Handles.DrawLine(Monster.transform.position, Monster.transform.position + viewAngleB * Monster.maxViewDistance);
        Handles.color = Color.yellow;
        foreach (Transform visableTarget in Monster.visableTargets)
        {
            Handles.DrawLine(Monster.transform.position, visableTarget.position);
        }
    }
}
