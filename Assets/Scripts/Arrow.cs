using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
	void OnTriggerEnter(Collider other)
    {
		//other.gameObject.GetComponent<Health>().Hit (20);
		if (other.gameObject.CompareTag ("Enemy")) {
			Destroy (other.gameObject);
		}
        Destroy(gameObject);

    }
}
