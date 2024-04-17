using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossProjectileBehavior : MonoBehaviour
{
    Vector3 startScale;
    Vector3 endScale;
    float startTime;
    GameObject player;
    bool called = false;
    Vector3 playerPosition;
    void Start()
    {
        startScale = transform.localScale;
        startTime = Time.time;
        endScale = startScale;
        endScale.y = startScale.y * 10;
        endScale.x = startScale.x * 10;
        endScale.z = startScale.z * 10;
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float elapsedTime = Time.time - startTime;

        if (elapsedTime < 5)
        {
            float lerpFactor = elapsedTime / 5f;
            Vector3 lerpedScale = Vector3.Lerp(startScale, endScale, lerpFactor);
            transform.localScale = lerpedScale;
        }

        int randomNum = Random.Range(5, 10);
        if (elapsedTime > randomNum)
        {
            if (!called)
            {
                playerPosition = player.transform.position;
                called = true;
            }

            float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);
            float moveSpeed = 65;

            if (distanceToPlayer > 0.1f)
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, playerPosition, step);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
