using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Important Object" , menuName = "Inventory System/Items/Important")]

public class ImportantObject : ItemObject
{
    public void Awake()
    {
    type = ItemType.Important;
    }
}
