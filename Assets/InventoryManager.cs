using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Card[] cards;
    public Image[] uiSlots = new Image[3]; 
    public Image[] uiInventory = new Image[3]; 
    private int selectedSlotIndex;
    private Color originalColor; 
    private Vector3 originalScale = Vector3.one; 
    private Vector3 targetScale = new Vector3(1.1f, 1.1f, 1.1f); 
    private Color highlightColor = Color.blue; 
    private float lerpSpeed = 5f;

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

    void CheckSlotSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectedSlotIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) selectedSlotIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) selectedSlotIndex = 2;
    }

    void CheckUseCard()
    {


        if (Input.GetMouseButtonDown(1))
        {
            if (!string.IsNullOrEmpty(cards[selectedSlotIndex]?.cardName))
            {
                if (selectedSlotIndex >= 0 && selectedSlotIndex < 3)
                {
                    Debug.Log("CheckUseCard in INventory Manager. Card Name: " + cards[selectedSlotIndex].cardName);

                    if (cards[selectedSlotIndex].cardName == "Slam")
                    {
                        Debug.Log("Slam used correctly. Current Veloctiy: " + gameObject.GetComponent<PlayerController>().velocity.y);
                        if (gameObject.GetComponent<PlayerController>().velocity.y > 0)
                        {
                            cards[selectedSlotIndex].UseAbility(); 
                            RemoveCardFromInventory(selectedSlotIndex); 

                        }
                    }
                    else
                    {
                        cards[selectedSlotIndex].UseAbility();
                        RemoveCardFromInventory(selectedSlotIndex);
                    }

                }
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
           
            if (cards[i] != null)
            {
                uiSlots[i].gameObject.SetActive(true);
                uiSlots[i].sprite = cards[i].cardSprite; 
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
                uiInventory[i].color = Color.Lerp(uiInventory[i].color, highlightColor, Time.deltaTime * lerpSpeed);
                uiInventory[i].transform.localScale = Vector3.Lerp(uiInventory[i].transform.localScale, targetScale, Time.deltaTime * lerpSpeed);
            }
            else
            {
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
