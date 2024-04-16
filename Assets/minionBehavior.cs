using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class minionBehavior : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agent;
    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        Vector3 up = transform.position;
        up.y = up.y + 1;
        transform.SetPositionAndRotation(up, transform.rotation);
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 15, -5), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        FaceTarget(player.position);

    }

    private void FixedUpdate()
    {
        if (transform.position.y - startPos.y <= 0.1)
        {
            agent.enabled = true;
            agent.SetDestination(player.transform.position);
        }
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }
}
