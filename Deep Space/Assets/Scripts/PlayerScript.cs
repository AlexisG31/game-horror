using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public InventoryObject Inventory;

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if(item)
        {
            Inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Inventory.Load();
        }
    }
    private void OnApplicationQuit()
    {
        Inventory.Container.Items = new InventorySlot[24];
    }
}
