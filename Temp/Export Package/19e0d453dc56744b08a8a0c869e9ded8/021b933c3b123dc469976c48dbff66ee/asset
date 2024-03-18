using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory; // Assign in the inspector
    public GameObject itemSlotPrefab; // Assign your item UI prefab
    private bool inventoryVisible = true; // Track visibility of the inventory

    private void Start()
    {
        // Initialize the UI without toggling its visibility off
        UpdateUI();
        gameObject.SetActive(inventoryVisible); // Ensure the UI reflects the initial 'inventoryVisible' state
    }

    private void Update()
    {
        // Check for the 'I' key press to toggle the inventory display
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    // Call this method whenever an item is added or removed from the inventory
    public void UpdateUI()
    {
        // Remove old item slots
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Create a new item slot for each item in the inventory
        foreach (Item item in inventory.items)
        {
            GameObject itemSlot = Instantiate(itemSlotPrefab, transform);
            itemSlot.GetComponent<Image>().sprite = item.icon; // Assuming your items have an 'icon' Sprite
            // If you have text for quantity or names, set it here
            // itemSlot.transform.GetChild(0).GetComponent<Text>().text = item.itemName;
        }
    }

    // Toggle the visibility of the inventory panel
    private void ToggleInventory()
    {
        inventoryVisible = !inventoryVisible;
        gameObject.SetActive(inventoryVisible);
        Debug.Log("Inventory visibility toggled to: " + inventoryVisible);

        if (inventoryVisible)
        {
            UpdateUI();
        }
    }
}
