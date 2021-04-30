using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.Collections;

public class PedestrianWander : MonoBehaviour
{
    public float wanderRadius;
    [Space(10)]
    public float wanderTimer;
    public float wanderTimerMax;
    public float wanderTimerMin;
    [Space(10)]
    public float speed;
    [Space(10)]
    public float speedMin;
    public float speedMax;

    private NavMeshAgent agent;
    public float timer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wanderTimer = Random.Range(wanderTimerMin, wanderTimerMax);
        speed = Random.Range(speedMin, speedMax);
        agent.speed = speed;
        timer = 0;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            WanderRandomize();
            timer = wanderTimer;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    void WanderRandomize()
    {
        wanderTimer = Random.Range(wanderTimerMin, wanderTimerMax);
    }
}