using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinState : MonoBehaviour {
	public Text win;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			win.gameObject.SetActive (true);
		}
	}
}
