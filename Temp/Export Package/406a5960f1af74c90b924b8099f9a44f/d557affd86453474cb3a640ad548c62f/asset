using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellShooter : MonoBehaviour
{
    public SpellCard currentSpellCard; // Assign in inspector
    public AudioSource audioSource; // Assign in inspector
    public Image reticleImage; // UI element for aiming reticle
    public Color reticleEnemyColor; // Color when aiming at enemy
    public Color reticleInteractableColor; // Color for interactables
    private Color originalReticleColor; // Original color of the reticle
    private float lastSpellTime = -Mathf.Infinity; // For cooldown management

    void Start()
    {
        // Initialize original reticle color
        originalReticleColor = reticleImage.color;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanCastSpell()) // 0 is the left mouse button
        {
            CastSpell();
            lastSpellTime = Time.time;
        }
        else if (Input.GetMouseButtonDown(0) && !CanCastSpell())
        {
            Debug.Log("Cannot Cast Spell!");
        }
    }

    void FixedUpdate()
    {
        // Handle reticle effect in Update to ensure smooth visual feedback
        UpdateReticleEffect();
    }
    bool CanCastSpell()
    {
        // check if enough time has elapsed since the last spell was cast
        return Time.time - lastSpellTime >= currentSpellCard.cooldown;
    }

    void CastSpell()
    {
        if (currentSpellCard != null && currentSpellCard.spellEffectPrefab != null)
        {

            GameObject spellVFX = Instantiate(currentSpellCard.spellEffectPrefab, transform.position, transform.rotation);
            ProjectileMoveScript projectileScript = spellVFX.GetComponent<ProjectileMoveScript>();
            if (projectileScript != null)
            {
                projectileScript.spellPower = currentSpellCard.spellPower; // Set the spell power
                projectileScript.timetillDestruction = currentSpellCard.timetillDestruction;
            }
            audioSource.clip = currentSpellCard.spellSound;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Spell card or spell effect prefab is missing!");
        }
    }

    void UpdateReticleEffect()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                reticleImage.color = Color.Lerp(reticleImage.color, reticleEnemyColor, Time.deltaTime * 2);
                reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, new Vector3(0.7f, 0.7f, 1), Time.deltaTime * 2);
            }
            else if (hit.collider.CompareTag("Interactable"))
            {
                reticleImage.color = Color.Lerp(reticleImage.color, reticleInteractableColor, Time.deltaTime * 2);
                reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, new Vector3(0.7f, 0.7f, 1), Time.deltaTime * 2);
            }
            else
            {
                ResetReticle();
            }
        }
        else
        {
            ResetReticle();
        }
    }

    void ResetReticle()
    {
        reticleImage.color = Color.Lerp(reticleImage.color, originalReticleColor, Time.deltaTime * 2);
    }
}

