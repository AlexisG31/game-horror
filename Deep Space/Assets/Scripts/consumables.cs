using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New consumables Object" , menuName = "Inventory System/Items/consumables")]
public class consumables : ItemObject
{
    public void Awake()
    {
        type = ItemType.consumables;
    }
}
