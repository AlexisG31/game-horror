using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMasking : MonoBehaviour
{
    public float speed = 3;
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
        if(Door = GameObject.Find("Door"))
        {
            float timeElapsed = 0;
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            if(timeElapsed > 5)
            {
                transform.Translate(Vector3.up * stop * Time.deltaTime);
            }
        }
    }
}
