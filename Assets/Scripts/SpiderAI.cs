using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SpiderAI : MonoBehaviour
{
    public Transform[] points;
    public GameObject spiderLootPrefab;
    private int destPoint = 0;
    NavMeshAgent agent;
    public Transform target;
    bool chasing;


    // Use this for initialization
    void Start()
    {
        chasing = false;
        agent = GetComponent<NavMeshAgent>();
        //agent.autoBraking = false;
        GotoNextPatrolPoint();


    }

    // Update is called once per frame
    void Update()
    {
        var heading = target.position - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        RaycastHit hit;
        Ray ray = new Ray(transform.position, direction);
        //print(agent.speed);
        if (Physics.Raycast(ray, out hit))
        {

            if (hit.collider.tag == "Player" && distance < 17 && !chasing)
            {

                transform.LookAt(target.position);
                agent.SetDestination(target.position);
               
                agent.speed += 2f;
                chasing = true;
            }
        }
        if (agent.remainingDistance < 1f  && !chasing)
        {
            GotoNextPatrolPoint();
        }
        if(chasing)
        {
            transform.LookAt(target.position);
            agent.SetDestination(target.position);
        }

    }
    void GotoNextPatrolPoint()
    {
        if (points.Length == 0)
        {
            return;
        }

        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    void OnDestroy()
    {
        var loot = (GameObject)Instantiate(spiderLootPrefab, transform.position, transform.rotation);
    }
}

