using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory; 
    public GameObject itemSlotPrefab; 
    private bool inventoryVisible = true; 

    private void Start()
    {
        UpdateUI();
        gameObject.SetActive(inventoryVisible);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void UpdateUI()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in inventory.items)
        {
            GameObject itemSlot = Instantiate(itemSlotPrefab, transform);
            itemSlot.GetComponent<Image>().sprite = item.icon; 
        }
    }

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
