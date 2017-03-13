using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class BatAI : MonoBehaviour {
    //https://docs.unity3d.com/Manual/nav-AgentPatrol.html 7-8 63-71
    public Transform[] points;
    public GameObject batLootPrefab;
    private int destPoint = 0;
    NavMeshAgent agent;
    public Transform target;
    public float attackInterval;
    public GameObject batProjectilePrefab;
    public Transform batProjectileSpawn;
    public float batProjectileSpeed;
    public Transform originalTransform;

    float lastShot = 0;


	// Use this for initialization
	void Start () {
        originalTransform = gameObject.transform;
        agent = GetComponent<NavMeshAgent>();
        GotoNextPatrolPoint();

		
	}
    
    // Update is called once per frame
    void Update() {
        var heading = target.position - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        RaycastHit hit;
        Ray ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray, out hit))
        {

            if (hit.collider.tag == "Player" && distance < 15)
            {
                
                agent.Stop();
                transform.LookAt(target.position);
                Fire();
            }
            else
            {
                transform.transform.rotation = new Quaternion(0.0f, -1.0f, 0.0f, 0.0f);
                agent.Resume();

            }
        }
        if (agent.remainingDistance < 0.5f)
        {
            GotoNextPatrolPoint();
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
    void Fire()
    {
        if (Time.time > attackInterval + lastShot)
        {
            var batProjectile = (GameObject)Instantiate(batProjectilePrefab, batProjectileSpawn.position, batProjectileSpawn.rotation);

            batProjectile.GetComponent<Rigidbody>().velocity = batProjectile.transform.forward * batProjectileSpeed;

            Destroy(batProjectile, 3.5f);

            lastShot = Time.time;
        }


    }
    void OnDestroy()
    {
        var loot = (GameObject)Instantiate(batLootPrefab, transform.position, transform.rotation);
    }


}

