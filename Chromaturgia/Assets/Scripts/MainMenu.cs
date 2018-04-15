using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	Animator thinkAnim;

	float bright;
	float soundVolume;
	float musicVolume;
	float alphaBright;
	float alphaDark;

	public Text brightText;
	public Text soundText;
	public Text musicText;

	public Image brightness;
	public Image darkness;

	public Button volverOptionsMenu;

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

		alphaBright = (bright - 50)/100;
		alphaDark = (50 - bright)/100;

		brightness.color = new Vector4 (1, 1, 1, alphaBright);
		darkness.color = new Vector4 (0, 0, 0, alphaDark);

		if (Input.GetKeyDown (KeyCode.Escape))
			volverOptionsMenu.onClick.Invoke ();
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
