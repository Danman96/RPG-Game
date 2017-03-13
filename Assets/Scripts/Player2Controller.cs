using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player2Controller : MonoBehaviour {
	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	public float moveSpeed;
	public float rotationSpeed;
	public float bulletSpeed;

	private Vector3 previousLocation;
	private Vector3 moveDirection;

	bool invulnerable = false;
	float invulnerableTime = 0;
	private NavMeshAgent nma;
	float nmaspeed;

	void Update()
	{

		moveDirection = Vector3.zero;
		previousLocation = transform.position;
		Vector3 s = transform.position - previousLocation;

		if (Input.GetKeyDown(KeyCode.RightControl))
		{
			Fire();
		}

		bool moved = false;
		if(Input.GetKey(KeyCode.UpArrow))
		{
			moveDirection.z = 1;
			s.z = 90;
			moved = true;
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			moveDirection.z = -1;
			s.z = -90;
			moved = true;
		}
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			moveDirection.x = -1;
			s.x = -90;
			moved = true;
		}
		if(Input.GetKey(KeyCode.RightArrow))
		{
			moveDirection.x = 1;
			s.x = 90;
			moved = true;
		}

		if (moved) {
			Move (s);
		}

		if (invulnerable) 
		{
			invulnerableTime += Time.deltaTime;
			if ((int)invulnerableTime > 1) 
			{
				invulnerable = false;
				invulnerableTime = 0.0F;
				nma.speed = nmaspeed;
				nma = null;
				nmaspeed = 0.0f;
			}
		}
	}


	void Fire()
	{
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

		Destroy(bullet, 2.0f);        
	}

	void Move(Vector3 s)
	{
		transform.position = Vector3.Lerp(transform.position, transform.position + moveDirection.normalized, Time.fixedDeltaTime * moveSpeed);
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation(s), Time.fixedDeltaTime * rotationSpeed);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Democrat"))
		{
			if (!invulnerable) {
				invulnerable = true;
				nma = other.gameObject.GetComponent<NavMeshAgent> ();
				nmaspeed = nma.speed;
				nma.speed = 0.0f;
			}
		}
	}
}