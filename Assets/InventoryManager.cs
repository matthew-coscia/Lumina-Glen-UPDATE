using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    //public List<Card> cards = new List<Card>(3); // Updated to store Card script references
    public Card[] cards;
    public Image[] uiSlots = new Image[3]; // References to the UI slots
    public Image[] uiInventory = new Image[3]; // References to the UI slots
    private int selectedSlotIndex; // Index of the currently selected slot, -1 means no selection
    private Color originalColor; // To store the original color of the UI component
    private Vector3 originalScale = Vector3.one; // Original scale, assuming it starts at 1,1,1
    private Vector3 targetScale = new Vector3(1.1f, 1.1f, 1.1f); // Target scale when highlighted
    private Color highlightColor = Color.blue; // Change as needed
    private float lerpSpeed = 5f; // Speed of the lerp animation

    void Start(){

        originalColor = uiSlots[0].color;
        originalScale = uiSlots[0].transform.localScale;
        selectedSlotIndex = 0; 
        cards = new Card[3];

    }


    void Update()
    {
        CheckSlotSelection();
        CheckUseCard();
        UpdateHighlight();
    }

    // Checks if a slot is selected (1, 2, or 3 keys)
    void CheckSlotSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectedSlotIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) selectedSlotIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) selectedSlotIndex = 2;
    }

    // Checks for right-click to use the selected card's ability
    void CheckUseCard()
    {

        if (Input.GetMouseButtonDown(1)) // Right-click
        {
            if (selectedSlotIndex >= 0 && selectedSlotIndex < 3)
            {
                Debug.Log("CheckUseCard in INventory Manager");
                cards[selectedSlotIndex].UseAbility(); // Use the card's ability
                RemoveCardFromInventory(selectedSlotIndex); // Then remove it from the inventory
            }
        }
    }

    public bool AddCard(Card card)
    {
        if (CountNonNullCards() < 3)
        {
            Debug.Log("Card Added");

            Debug.Log("Cards Length: " + cards.Length);

            for(int i = 0; i < 3; i++){
                if (cards[i] == null){
                    Debug.Log("Added Card");
                    cards[i] = card;
               UpdateInventoryUI();
            return true;
                }
            }

            
        }
        return false;
    }

    void RemoveCardFromInventory(int index)
    {
        if (index >= 0 && index < 3)
        {
            cards[index] = null;
            UpdateInventoryUI();
        }
    }

    void UpdateInventoryUI()
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            Debug.Log("Index: " + i);
            if (cards[i] != null)
            {
                uiSlots[i].gameObject.SetActive(true);
                uiSlots[i].sprite = cards[i].cardSprite; // Assuming Card has a Sprite for UI
            }
            else
            {
               uiSlots[i].gameObject.SetActive(false);
            }
        }
    }

    void UpdateHighlight()
    {
        for (int i = 0; i < uiInventory.Length; i++)
        {
            if (i == selectedSlotIndex)
            {
                Debug.Log("Here");
                // Highlight the selected component
                uiInventory[i].color = Color.Lerp(uiInventory[i].color, highlightColor, Time.deltaTime * lerpSpeed);
                uiInventory[i].transform.localScale = Vector3.Lerp(uiInventory[i].transform.localScale, targetScale, Time.deltaTime * lerpSpeed);
            }
            else
            {
                // Return non-selected components to their original state
                uiInventory[i].color = Color.Lerp(uiInventory[i].color, originalColor, Time.deltaTime * lerpSpeed);
                uiInventory[i].transform.localScale = Vector3.Lerp(uiInventory[i].transform.localScale, originalScale, Time.deltaTime * lerpSpeed);
            }
        }
    }

    int CountNonNullCards()
{
    int count = 0;
    foreach (var card in cards)
    {
        if (card != null)
        {
            count++;
        }
    }
    Debug.Log("Cards Counted: " + count);
    return count;
}

}
