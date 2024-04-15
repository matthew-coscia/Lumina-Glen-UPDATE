using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public GameObject minion;
    public GameObject projectile;
    public Transform player;
    Vector3 startScale;
    Vector3 startPos;
    float startTime;
    bool isJumping = false;
    bool cast = true;
    bool summon = true;
    float timer = 0;
    float duration = 5;
    void Start()
    {
        startPos = transform.position;
        startScale = transform.localScale;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FaceTarget(player.position);
        breathAnimation();
        if (Time.time % 10 <= 1 && cast)
        {
            cast = false;
            castAttack();
        }
        if (isJumping)
        {
            jumpAnimation();
        }

        if (Time.time % 7 <= 0.1 && !summon)
        {
            int randomAmount = Random.Range(3, 10);
            callSummonProjectileNTimes(randomAmount);
            summon = true;
        }

        timer += Time.deltaTime;
        if (timer >= duration)
        {
            summon = false;
            timer = 0;
        }

    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    void breathAnimation()
    {
        Vector3 newScale = transform.localScale;
        newScale.y = startScale.y + Mathf.Sin(Time.time * 2) * 5;
        transform.localScale = newScale;
    }

    void jumpAnimation()
    {
        Vector3 newPos = transform.position;

        float elapsedTime = Time.time - startTime;

        if (elapsedTime <= Mathf.PI * 0.5)
        {
            newPos.y = startPos.y + Mathf.Sin(elapsedTime * 2) * 20;
            transform.position = newPos;
        }
        else
        {
            int randomAmount = Random.Range(3, 6);
            callSummonMinionsNTimes(randomAmount);
            cast = true;
            isJumping = false;
            transform.position = startPos;
        }
    }
    void castAttack()
    {
        if (!isJumping)
        {
            startTime = Time.time;
            isJumping = true;
        }
    }

    void summonMinions()
    {
        Vector3 enemyPosition;
        enemyPosition.x = Random.Range(transform.position.x - 20, transform.position.x + 20);
        enemyPosition.y = transform.position.y;
        enemyPosition.z = transform.position.z - 25;
        GameObject spawnedEnemy = Instantiate(minion, enemyPosition, transform.rotation) as GameObject;
    }

    void summonProjectile()
    {
        Vector3 projPosition;
        projPosition.x = Random.Range(transform.position.x - 25, transform.position.x + 25);
        projPosition.y = Random.Range(transform.position.y + 20, transform.position.y + 40);
        projPosition.z = transform.position.z - 35;
        GameObject spawnedProj = Instantiate(projectile, projPosition, transform.rotation) as GameObject;
    }

    void callSummonMinionsNTimes(int n)
    {
        for (int i = 0; i < n; i++)
        {
            summonMinions();
        }
    }

    void callSummonProjectileNTimes(int n)
    {
        for (int i = 0; i < n; i++)
        {
            summonProjectile();
        }
    }
}
