using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>(); // The current list of items in the inventory
    public int capacity = 20; // Maximum number of items the inventory can hold

    // Adds an item to the inventory if there is space
    public bool AddItem(Item item)
    {
        if (items.Count >= capacity)
        {
            Debug.Log("Inventory is full!");
            return false;
        }
        items.Add(item);
        Debug.Log(item.itemName + " added to inventory.");
        // Here, you could invoke an event to update the UI
        return true;
    }

    // Removes an item from the inventory
    public void RemoveItem(Item item)
    {
        items.Remove(item);
        Debug.Log(item.itemName + " removed from inventory.");
        // Here, you could invoke an event to update the UI
    }

    // Check if the inventory contains a specific item
    public bool ContainsItem(Item item)
    {
        return items.Contains(item);
    }

    // You can add more methods here based on your game's requirements, such as methods to check for specific quantities of items, or to find all items of a certain type.
}
