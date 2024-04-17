using UnityEngine;

[CreateAssetMenu(fileName = "NewSpellCard", menuName = "Inventory/Spell Card")]
public class SpellCard : Item
{
    public int spellPower = 50;
    public int manaCost = 20;
    public float cooldown = 5f;
    public float timetillDestruction = 10f;
    public AudioClip spellSound;
    public GameObject spellEffectPrefab; 
    public void CastSpell(Vector3 castPosition, Quaternion castRotation)
    {
        Debug.Log(itemName + " cast! Spell Power: " + spellPower);

        if (spellEffectPrefab != null)
        {
            GameObject spellEffectInstance = GameObject.Instantiate(spellEffectPrefab, castPosition, castRotation);
            GameObject.Destroy(spellEffectInstance, 5f);
        }
        else
        {
            Debug.LogWarning("Spell effect prefab for " + itemName + " is not set.");
        }

    }
}
