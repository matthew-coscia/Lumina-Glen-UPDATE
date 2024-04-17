using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>(); 
    public int capacity = 20; 

    public bool AddItem(Item item)
    {
        if (items.Count >= capacity)
        {
            Debug.Log("Inventory is full!");
            return false;
        }
        items.Add(item);
        Debug.Log(item.itemName + " added to inventory.");
        return true;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        Debug.Log(item.itemName + " removed from inventory.");
    }

    public bool ContainsItem(Item item)
    {
        return items.Contains(item);
    }

}
