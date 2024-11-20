using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMasking : MonoBehaviour
{
    public GameObject Door;
    [SerializeField] 
    private GameObject Keycard;


    public LayerMask collisionLayerMask;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 9)
        {  
            Destroy(gameObject);
        }
        //if(Door = GameObject.Find("Door"))
        //{

        //}
    }
}
