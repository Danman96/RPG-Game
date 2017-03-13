using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public GameObject tut;
	public GameObject tutspace;
	public GameObject goal;
	public GameObject boss;
	public GameObject win;
	bool atboss = false;
	public GameObject time;
	public GameObject timeloss;

	float timer;

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			Time.timeScale = 1;
			SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().name);
		}
		timer += Time.deltaTime;

		time.GetComponent<Text> ().text = "Time: " + (300 - (int)timer).ToString();

		if (timer > 300) {
			timeloss.GetComponent<Text> ().enabled = true;
			Time.timeScale = 0;
		}

		if (timer > 3) {
			tut.GetComponent<Text> ().enabled = false;
			tutspace.GetComponent<Text> ().enabled = true;
		}
		if (timer > 6) {
			tutspace.GetComponent<Text> ().enabled = false;
			goal.GetComponent<Text> ().enabled = true;
		}
		if (timer > 9) {
			goal.GetComponent<Text> ().enabled = false;
		}

		if (boss.activeSelf == true) {
			atboss = true;
		}

		if (atboss) {
			if (boss.activeSelf == false) {
				win.GetComponent<Text> ().enabled = true;
				Time.timeScale = 0;
			}
		}
	}
}
