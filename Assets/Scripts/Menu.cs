using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	public Button start;
	public Button help;
	public Button quit;
	public Button back;
	public GameObject mainmenu;
	public GameObject helpmenu;

	void Start () {
		start.onClick.AddListener (startGame);
		help.onClick.AddListener (helpMenu);
		quit.onClick.AddListener (quitGame);
		back.onClick.AddListener (backMenu);
	}
	
	void startGame(){
		SceneManager.LoadSceneAsync ("Town", LoadSceneMode.Single);
	}

	void helpMenu(){
		helpmenu.SetActive (true);
		mainmenu.SetActive (false);
	}

	void quitGame(){
		Application.Quit();
	}

	void backMenu(){
		mainmenu.SetActive (true);
		helpmenu.SetActive (false);
	}
}
