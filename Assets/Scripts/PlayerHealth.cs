using Michsky.UI.ModernUIPack;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static bool isDead = false;
    public int maxHealth = 3; 
    public int health = 3; 
    public GameObject diedScreen; 
    public GameObject redScreenImage; 
    public AudioSource hitSound;
    public AudioClip audioClip;
    public float flashDuration = 0.5f; 
    public float damageCooldown = 1.0f;
    private float lastDamageTime;
    public Slider healthBar;
    private int fellOff = 0;

    private void Start()
    {
        diedScreen.SetActive(false); // Ensure the died screen is not visible at start
        hitSound.clip = audioClip;
        lastDamageTime = -damageCooldown; // Ensure damage can be taken immediately
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collided with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(5);
            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(3);
        }
        if (health <= 0)
        {
            Die();
        }
    }

    private void Update()
    {
        if (transform.position.y <= -5)
        {
            health = 0;
            if (fellOff == 0)
            {
                hitSound.Play();
                fellOff = 1;
            }
        }

       // Debug.Log(health);
        // Check if the player has died and presses 'R' to restart the game
        if (health <= 0)
        {
            Die();
            if (Input.GetKeyDown(KeyCode.R))
            {
                isDead = false; 
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (Time.time - lastDamageTime < damageCooldown)
        {
            return; // If still in cooldown, do not take damage
        }

        health -= damage;

        UpdateHealthBar(); // Update the health bar after taking damage
        FlashScreen(); // Visual feedback for damage
        hitSound.Play(); // Audio feedback for damage
        lastDamageTime = Time.time; // Update last damage time

        if (health <= 0)
        {
            Die(); // Handle player death
        }
    }

    public void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            Debug.Log("Health:  " + health + "Max Health:  " + maxHealth + "\n");
            float healthPercentage = ((float)health / maxHealth) * 100f; // Calculate health percentage
            healthBar.value = healthPercentage; // Update the Scrollbar's size to reflect current health
        }
    }


    void Die()
    {
        diedScreen.SetActive(true); // Show the "You Died" screen
        // Optionally, disable player controls here
        isDead = true;
    }

    void FlashScreen()
    {
        StartCoroutine(FlashScreenCoroutine());
    }

    IEnumerator FlashScreenCoroutine()
    {
        redScreenImage.SetActive(true); // Show the red screen
        yield return new WaitForSeconds(0.5f); // Wait for half a second
        redScreenImage.SetActive(false); // Hide the red screen
    }


    public void GiveHealth(int healAmount){
        Debug.Log("Health given\n");
        health += healAmount; // Apply damage based on percentage
        UpdateHealthBar(); // Update the health bar after taking damage

    
    }
}
