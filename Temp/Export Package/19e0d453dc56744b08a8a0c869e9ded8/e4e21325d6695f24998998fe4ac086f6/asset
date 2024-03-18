using Michsky.UI.ModernUIPack;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // max health points
    public int health = 3; // Player starts with 3 health points
    public GameObject diedScreen; // Assign the UI panel for "You Died" screen in the inspector
    public GameObject redScreenImage; // Assign the red screen Image in the inspector
    public AudioSource hitSound; // Assign the AudioSource in the inspector
    public AudioClip audioClip;
    public float flashDuration = 0.5f; // Duration of the red screen flash
    public float damageCooldown = 1.0f;
    private float lastDamageTime;
    public ProgressBar healthBar;
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
            health -= 1; // Reduce health by one
            FlashScreen();
            hitSound.Play();
            UpdateHealthBar();
            if (health <= 0)
            {
                Die();
            }
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

        Debug.Log(health);
        // Check if the player has died and presses 'R' to restart the game
        if (health <= 0)
        {
            Die();
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (Time.time - lastDamageTime < damageCooldown)
            return; // If still in cooldown, do not take damage

        float damagePercentage = (float)damage / maxHealth; // Calculate damage as a percentage of max health
        health -= Mathf.CeilToInt(damagePercentage * maxHealth); // Apply damage based on percentage

        UpdateHealthBar(); // Update the health bar after taking damage
        FlashScreen(); // Visual feedback for damage
        hitSound.Play(); // Audio feedback for damage
        lastDamageTime = Time.time; // Update last damage time

        if (health <= 0)
            Die(); // Handle player death
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            float healthPercentage = (health / 3f) * 100; // Assuming 3 is max health
            healthBar.currentPercent = healthPercentage;
            healthBar.loadingBar.fillAmount = healthPercentage / 100;
            healthBar.textPercent.text = ((int)healthPercentage).ToString("F0") + "%";
        }
    }


    void Die()
    {
        diedScreen.SetActive(true); // Show the "You Died" screen
        // Optionally, disable player controls here
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
}
