using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	Animator thinkAnim;

	GameObject startMenu;
	GameObject mainMenu;
	GameObject title;
	GameObject exitMenu;

	void Awake()
	{
		thinkAnim = GameObject.FindGameObjectWithTag ("ThinkMenu").GetComponent<Animator> ();
		startMenu = GameObject.FindGameObjectWithTag ("StartMenu");
		mainMenu = GameObject.FindGameObjectWithTag ("MainMenu");
		title = GameObject.FindGameObjectWithTag ("TitleMenu");
		exitMenu = GameObject.FindGameObjectWithTag ("ExitMenu");

		mainMenu.SetActive (false);
		exitMenu.SetActive (false);
	}


	public void pulsaEspacio()
	{
		startMenu.SetActive (false);
		title.SetActive (false);


		thinkAnim.SetBool ("Activated", true);

		Invoke ("activeMenu", 1.7f);

	}

	public void activaExit()
	{
		mainMenu.SetActive (false);
		exitMenu.SetActive (true);

	}

	public void desactivaExit()
	{
		exitMenu.SetActive (false);
		mainMenu.SetActive (true);
	}

	void activeMenu()
	{
		mainMenu.SetActive(true);
	}
}
