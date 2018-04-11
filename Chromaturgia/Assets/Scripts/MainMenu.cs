using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	Animator thinkAnim;

	float bright;
	float soundVolume;
	float musicVolume;

	public Text brightText;
	public Text soundText;
	public Text musicText;

	GameObject mainMenu;

	void Awake()
	{
		thinkAnim = GameObject.FindGameObjectWithTag ("ThinkMenu").GetComponent<Animator> ();
		mainMenu = GameObject.FindGameObjectWithTag ("MainMenu");

		mainMenu.SetActive (false);

		bright = 50;
		soundVolume = 50;
		musicVolume = 50;
	}

	void Update()
	{
		brightText.text = bright + "%";
		soundText.text = soundVolume + "%";
		musicText.text = musicVolume + "%";


		//RenderSettings.ambientLight = new Color (bright, bright, bright, 1.0f);
	}


	public void pulsaEspacio()
	{
		thinkAnim.SetBool ("Activated", true);

		Invoke ("activeMenu", 1.7f);
	}

	void activeMenu()
	{
		mainMenu.SetActive(true);
	}

	public void alternaFullscreen()
	{
		Screen.fullScreen = !Screen.fullScreen;
	}

	public void setBright(float brillo)
	{
		bright = brillo;
	}

	public void setSoundVolume(float volume)
	{
		soundVolume = volume;
	}

	public void setMusicVolume(float volume)
	{
		musicVolume = volume;
	}

	public void quitGame()
	{
		Application.Quit ();
	}
}
