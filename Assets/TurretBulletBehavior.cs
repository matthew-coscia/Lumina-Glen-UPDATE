using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletBehavior : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(2f, 2f, 2f); // The target scale to reach
    public float duration = 2f; // Duration in seconds

    private float timer = 0f;
    private Vector3 initialScale;

    void Start()
    {
        // Get the initial scale of the GameObject
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Increment timer
        timer += Time.deltaTime;

        // Calculate the progress
        float progress = Mathf.Clamp01(timer / duration);

        // Interpolate between initial scale and target scale
        transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);
    }

     private void OnTriggerEnter(Collider other)
    {
        // Check if the GameObject is tagged as "Player" or "Enemy".
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            // Do nothing if the tag is "Player" or "Enemy".
        }
        else
        {
            // Destroy the other GameObject if it has a different tag or is not tagged.
            Destroy(gameObject);
        }
    }

}
