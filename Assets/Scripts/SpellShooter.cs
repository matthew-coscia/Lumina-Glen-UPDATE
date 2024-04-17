using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellShooter : MonoBehaviour
{
    public SpellCard currentSpellCard;
    public AudioSource audioSource; 
    public Image reticleImage; 
    public Color reticleEnemyColor; 
    public Color reticleInteractableColor; 
    private Color originalReticleColor; 
    private float lastSpellTime = -Mathf.Infinity; 
    private float spellCooldown = .5f; 
    private float ammoStartTime;
    private bool ammoAbilityBool = false;
    public GameObject Menu;

    void Start()
    {
        originalReticleColor = reticleImage.color;
    }

    void Update()
    {
        bool paused = Menu.GetComponent<MenuManager>().isMenuActive;
        if (Input.GetMouseButtonDown(0) && CanCastSpell() && !PlayerHealth.isDead && !paused) 
        {
            CastSpell();
            lastSpellTime = Time.time;
        }
        else if (Input.GetMouseButtonDown(0) && !CanCastSpell())
        {
            Debug.Log("Cannot Cast Spell!");
        }
        CheckAmmo();
    }

    void FixedUpdate()
    {
        UpdateReticleEffect();
    }
    bool CanCastSpell()
    {
        return Time.time - lastSpellTime >= spellCooldown;
    }

    void CastSpell()
    {
        if (currentSpellCard != null && currentSpellCard.spellEffectPrefab != null)
        {

            GameObject spellVFX = Instantiate(currentSpellCard.spellEffectPrefab, transform.position, transform.rotation);
            ProjectileMoveScript projectileScript = spellVFX.GetComponent<ProjectileMoveScript>();
            if (projectileScript != null)
            {
                projectileScript.spellPower = currentSpellCard.spellPower;
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

    public void ammoAbility(){
        ammoStartTime = Time.time;
        spellCooldown = .1f;
        ammoAbilityBool = true;
    }

    void CheckAmmo()
    {
        if (ammoAbilityBool && Time.time - ammoStartTime >= 3.0f)
        {
            spellCooldown = .5f;
            ammoAbilityBool = false;
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
