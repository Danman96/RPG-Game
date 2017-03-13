using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
	public int MaxHP = 100;

	[SyncVar(hook = "OnChangeHealth")]
	public int HP = 100;

	public RectTransform HPbar;

	public GameObject player;

	//public AudioClip impact;
	//AudioSource audio;
	//private NavMeshAgent agent;

	void Start()
	{
		//agent = GetComponent<NavMeshAgent> ();
		//audio = GetComponent<AudioSource> ();
	}

	public void Hit (int damage) 
	{
		//if (!isServer)
		//	return;
		HP = HP - damage;

		if (HP <= 0) {
			//player.GetComponent<Rage> ().CurRage -= 5;
			//audio.PlayOneShot (impact, 1.0F);
			RpcRespawn();
			//gameObject.SetActive(false);
			//StartCoroutine (MC());

		}
	}


	void OnChangeHealth(int health)
	{
			HPbar.sizeDelta = new Vector2 (HP, HPbar.sizeDelta.y);
	}

	[ClientRpc]
	void RpcRespawn()
	{
		if (isLocalPlayer)
		{
			// move back to zero location
			transform.position = Vector3.zero;
		}
	}

	//IEnumerator MC(){
	//	agent.speed = 0;
	//	yield return new WaitForSeconds(2.0F);
	//	gameObject.SetActive(false);
	//}
}
