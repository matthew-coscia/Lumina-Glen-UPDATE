using UnityEngine;

public class SpellCastingController : MonoBehaviour
{
    public SpellCard currentSpellCard; // Assign this in the inspector or dynamically
    public Transform cameraTransform; // Assign your camera transform in the inspector
    public AudioSource audioSource;
    private float lastSpellTime = -Mathf.Infinity; // Initialize to allow immediate casting


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanCastSpell()) // 0 is the left mouse button
        {
            CastSpell();
            lastSpellTime = Time.time;
        }
        if (Input.GetMouseButtonDown(0) && !CanCastSpell())
        {
            Debug.Log("Cannot Cast Spell!");
        }
    }

    bool CanCastSpell()
    {
        // Check if enough time has elapsed since the last spell was cast
        return Time.time - lastSpellTime >= currentSpellCard.cooldown;
    }

    void CastSpell()
    {
        if (currentSpellCard != null && currentSpellCard.spellEffectPrefab != null)
        {
            GameObject spellVFX = Instantiate(currentSpellCard.spellEffectPrefab, cameraTransform.position, cameraTransform.rotation);
            ProjectileMoveScript projectileScript = spellVFX.GetComponent<ProjectileMoveScript>();
            if (projectileScript != null)
            {
                projectileScript.spellPower = currentSpellCard.spellPower; // Set the spell power
                projectileScript.timetillDestruction = currentSpellCard.timetillDestruction;
                audioSource.clip = currentSpellCard.spellSound;
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("Spell card or spell effect prefab is missing!");
        }
    }

}
