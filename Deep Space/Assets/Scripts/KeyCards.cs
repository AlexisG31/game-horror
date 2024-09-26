using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New cards Object" , menuName = "Inventory System/Items/cards")]
public class KeyCards : ItemObject
{
    public void Awake()
    {
        type = ItemType.cards;
    }
}
