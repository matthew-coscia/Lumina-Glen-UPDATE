using UnityEngine;

[CreateAssetMenu(fileName = "NewSpellCard", menuName = "Inventory/Spell Card")]
public class SpellCard : Item
{
    public int spellPower = 50;
    public int manaCost = 20;
    public float cooldown = 5f;
    public float timetillDestruction = 10f;
    public AudioClip spellSound;
    public GameObject spellEffectPrefab; // Reference to the spell effect prefab

    // Add any spell-specific methods or properties here
    // For example, you might want to add a method to cast the spell
    public void CastSpell(Vector3 castPosition, Quaternion castRotation)
    {
        Debug.Log(itemName + " cast! Spell Power: " + spellPower);

        // Check if the spellEffectPrefab is assigned
        if (spellEffectPrefab != null)
        {
            // Instantiate the spell effect at the given position and rotation
            GameObject spellEffectInstance = GameObject.Instantiate(spellEffectPrefab, castPosition, castRotation);
            // You can add additional logic here, such as setting the spell effect's size or duration based on spellPower
            
            // Optional: Destroy the spell effect instance after a certain duration if it doesn't destroy itself
            GameObject.Destroy(spellEffectInstance, 5f); // Adjust the duration based on the effect's needs
        }
        else
        {
            Debug.LogWarning("Spell effect prefab for " + itemName + " is not set.");
        }

        // Implement additional spell casting logic here, such as applying damage
    }
}
