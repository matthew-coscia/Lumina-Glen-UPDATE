using Michsky.UI.ModernUIPack;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static bool isDead = false;
    public int maxHealth = 10; 
    public int health = 10; 
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
        diedScreen.SetActive(false);
        hitSound.clip = audioClip;
        lastDamageTime = -damageCooldown;
    }

    private void OnCollisionEnter(Collision collision)
    {
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

        if (health <= 0)
        {
            Die();
            if (Input.GetKeyDown(KeyCode.R))
            {
                isDead = false; 
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (Time.time - lastDamageTime < damageCooldown)
        {
            return;
        }

        health -= damage;

        UpdateHealthBar();
        FlashScreen();
        hitSound.Play(); 
        lastDamageTime = Time.time; 

        if (health <= 0)
        {
            Die(); 
        }
    }

    public void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            Debug.Log("Health:  " + health + "Max Health:  " + maxHealth + "\n");
            float healthPercentage = ((float)health / maxHealth) * 100f; 
            healthBar.value = healthPercentage; 
        }
    }


    void Die()
    {
        diedScreen.SetActive(true); 
        isDead = true;
    }

    void FlashScreen()
    {
        StartCoroutine(FlashScreenCoroutine());
    }

    IEnumerator FlashScreenCoroutine()
    {
        redScreenImage.SetActive(true); 
        yield return new WaitForSeconds(0.5f); 
        redScreenImage.SetActive(false); 
    }


    public void GiveHealth(int healAmount){
        Debug.Log("Health given\n");
        
        health += healAmount;

        if (health > maxHealth){
            health = maxHealth; 
        }

        UpdateHealthBar(); 

    
    }
}
