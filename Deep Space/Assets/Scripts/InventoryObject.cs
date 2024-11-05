using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    private ItemDataBaseObject database;
    public InventorySlot[] Container = new InventorySlot[24];

    private void OnEnable ()
    {
#if UNITY_EDITOR
        database = (ItemDataBaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Items/Database.asset", typeof(ItemDataBaseObject));
#else
        database = Resources.Load<ItemDataBaseObject>("DataBase");
#endif
    }
    public void AddItem(ItemObject _item, int _amount)
    {
        for (int i = 0; 1 < Container.Length; i++)
        {
            if (Container.Items[i].ID == _item.Id)
            {
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }
        SetEmptySlot(_item, _amount);
        return;
    }
    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].ID <= -1)
            {
                Container.Items[i].UpdateSlot(_item.id, _item, _amount);
                return Container.Items[i];
            }
        }
    }
    //set up functionality for full inventory
    return null;

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.ID, item2.item, item2.amount);
        item2.UpdateSlot(item1.ID, item1.item, item,amount);
        item1.UpdateSlot(temp.ID, temp.item, temp.amount);
    }

    public void RemoveItem(item _item)
    {
        for (int i = 0; < Container.Item.Length; i ++)
        {
            if(Container.Item[i].item == _item)
            {
                Container.item[i].UpdateSlot(-1, nulll, 0);
            }
        }
    }
    public void Save()
    {
        //string saveData = JsonUtility.ToJson(this, true);
       // BinaryFormatter bf = new BinaryFormatter();
       // FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
       // bf.Serialize(file, saveData);
       // file.Close();
    IFormatter formatter = new BinaryFormatter();
    Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
    Inventory newContainer = (Inventory)formatter.Deserialize(stream);
    }
    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
           // BinaryFormatter bf = new BinaryFormatter();
           // FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
           // JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
           // file.Close();

           IFormatter formatter = new BinaryFormatter();
           Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
           Inventory newContainer = (Inventory)formatter.Deserialize(stream);
           for(int i = 0; Container.Items.Length; i++)
           {
               Container.items[i].UpdateSlot(newContainer.Items[i].ID, newContainer.Items[i].item, newContainer.Items[i].amount);
           }
           stream.Close();
        }
    }
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Container.Count; i++)
        {
            Container[i].item = database.GetItem[Container[i].ID];
        }
    }
    public void OnBeforeSerialize()
    {

    }
}

  [System.Serializable]
public class InventorySlot
{
    public int ID = -1;
    public ItemObject item;
    public  int amount;
    public InventorySlot(int _id, ItemObject _item, int _amount)
    {
        ID = -1;
        item = null;
        amount = 0;
    }
    public InventorySlot()
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
    public void UpdateSlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}
