using UnityEngine;

public class SpellCastingController : MonoBehaviour
{
    public SpellCard currentSpellCard;
    public Transform cameraTransform; 
    public AudioSource audioSource;
    private float lastSpellTime = -Mathf.Infinity; 


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanCastSpell())
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
                projectileScript.spellPower = currentSpellCard.spellPower; 
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
