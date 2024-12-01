using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayInventory : MonoBehaviour
{
    public InventoryObject Inventory;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;
    Dictionary<InventorySlot, GameObject> itemDisplayed = new Dictionary<InventorySlot, GameObject>();
    //start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    //update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }
    public void UpdateDisplay()
    {
        for(int i = 0; i < Inventory.Container.Count; i++)
        {
            if (itemDisplayed.ContainsKey(Inventory.Container[i]))
            {
                itemDisplayed[Inventory.Container[i]].GetComponentInChildren<TextMeshProGUI>().text = Inventory.Container[i].amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(Inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProGUI>().text = Inventory.container[i].amount.ToString("n0");
                itemDisplayed.Add(Inventory.Container[i], obj);
            }
        }
    }
    public void CreateDisplay()
    {
        for (int i = 0; i < Inventory.Container.Count; i ++)
        {
            var obj = Instantiate(Inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProGUI>().text = Inventory.container[i].amount.ToString("n0");

            itemDisplayed.Add(Inventory.Container[i], obj);
        }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START +(X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i/NUMBER_OF_COLUMN)), 0f);
    }
}