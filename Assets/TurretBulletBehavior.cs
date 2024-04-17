using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletBehavior : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(2f, 2f, 2f); 
    public float duration = 2f; 

    private float timer = 0f;
    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        timer += Time.deltaTime;

        float progress = Mathf.Clamp01(timer / duration);

        transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);
    }

     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
