﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
	public AudioMixer audioMixer;
    public AudioMixer musicMixer;

    bool needsBrightnessUpdate, needsSaturationUpdate;
    bool needsSoundUpdate, needsMusicUpdate;

	ParticleSystem.MainModule thinkFX;
    Animator thinkAnim;

    float sliderBrightness;
    float sliderSaturation;
    float soundVolume;
    float musicVolume;

    public Text brightText;
    public Text saturationText;
    public Text soundText;
    public Text musicText;

    public Button volverOptionsMenu;
	public Button continueButton;

    public Slider brightnessSlider;
    public Slider saturationSlider;
    public Slider soundSlider;
    public Slider musicSlider;

    GameObject mainMenu;
	GameObject currentGame;
	GameObject newGameMenu;

    EventSystem eventsystem;

    void Start()
    {
        thinkAnim = GameObject.FindGameObjectWithTag("ThinkMenu").GetComponent<Animator>();
		thinkFX = GameObject.FindGameObjectWithTag("ThinkMenu").GetComponentInChildren<ParticleSystem>().main;
        mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
		newGameMenu = GameObject.FindGameObjectWithTag("NewGameMenu");
		currentGame = GameObject.FindGameObjectWithTag("CurrentGame");
		currentGame.SetActive (false);
        mainMenu.SetActive(false);
		newGameMenu.SetActive(false);

		thinkFX.startSize = 0f;

        sliderBrightness = (GameManager.instance.brightness * 50) + 50;
        sliderSaturation = (GameManager.instance.saturation * 50) + 50;

        Debug.Log(soundVolume);

        soundVolume = GameManager.instance.soundVolume;
        musicVolume = GameManager.instance.musicVolume;

        Debug.Log(soundVolume);

        soundSlider.value = soundVolume;
        musicSlider.value = musicVolume;

        brightnessSlider.value = sliderBrightness;

        saturationSlider.value = sliderSaturation;

        needsBrightnessUpdate = true;
        needsSaturationUpdate = true;
        needsMusicUpdate = false;
        needsSoundUpdate = false;


    }

    void Update()
    {
        brightText.text = sliderBrightness + "%";
        saturationText.text = sliderSaturation + "%";
        soundText.text = ((soundVolume + 48) * 100) / 48 + "%";
        musicText.text = ((musicVolume + 48) * 100) / 48 + "%";

        eventsystem = EventSystem.current;
		if (eventsystem!=null)
		{
	        if (eventsystem.currentSelectedGameObject == null)
	            eventsystem.SetSelectedGameObject(eventsystem.firstSelectedGameObject);
		}

		if (Input.GetKeyDown (KeyCode.Escape) && volverOptionsMenu.isActiveAndEnabled)
		{
			volverOptionsMenu.onClick.Invoke ();
			FindObjectOfType<AudioManager> ().Play ("MenuBack");
		}

		if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
			FindObjectOfType<AudioManager> ().Play ("MenuSelect");

        if (needsBrightnessUpdate)
        {
            GameManager.instance.ChangeBrightness();
            needsBrightnessUpdate = false;
        }

        if (needsSaturationUpdate)
        {
            GameManager.instance.ChangeSaturation();
            needsSaturationUpdate = false;
        }



        if(!SaveLoad.instance.saveDataExists())
        {
            continueButton.GetComponentInChildren<Text>().canvasRenderer.SetAlpha(0.5f);
        }
    }

    public void pulsaEspacio()
    {
        thinkAnim.SetBool("Activated", true);

        Invoke("activeMenu", 1.7f);
    }

    void activeMenu()
    {
		if (SaveLoad.instance.saveDataExists()) 
		{
			currentGame.SetActive (true);
		}
		else
        {
            continueButton.GetComponentInChildren<Text>().canvasRenderer.SetAlpha(0.5f);
            continueButton.interactable = false;
        }

		thinkFX.startSize = 0.29f;
        mainMenu.SetActive(true);
    }

    public void alternaFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void setBright(float brillo)
    {
        sliderBrightness = brillo;
        needsBrightnessUpdate = true;

        GameManager.instance.brightness = (sliderBrightness - 50) / 50;
        SaveLoad.instance.brightness = GameManager.instance.brightness;
        SaveLoad.instance.SaveConfig();
    }

    public void setSaturation(float saturation)
    {
        sliderSaturation = saturation;
        needsSaturationUpdate = true;

        GameManager.instance.saturation = (sliderSaturation - 50) / 50;
        SaveLoad.instance.saturation = GameManager.instance.saturation;
        SaveLoad.instance.SaveConfig();
    }

    public void setSoundVolume(float volume)
    {
        if (volume == -40)
        {
            soundVolume = -80;
        }
        else
        {
            soundVolume = volume;
        }
        SaveLoad.soundVolume = volume;
		audioMixer.SetFloat ("SoundVolume",volume);

        SaveLoad.instance.SaveConfig();
    }

    public void setMusicVolume(float volume)
    {
        if (volume == -40)
        {
            musicVolume = -80;
        }
        else
        {
            musicVolume = volume;
        }
        SaveLoad.instance.musicVolume = volume;
        musicMixer.SetFloat("MusicVolume", volume);

        SaveLoad.instance.SaveConfig();
    }

    public void NewGame()
    {
        SaveLoad.instance.Reset();
        SaveLoad.instance.Load();
		StartCoroutine (GameObject.Find("Fade").GetComponent<FadeController>().Fading());
        SceneManager.LoadSceneAsync("Pre-Puzle 0-0", LoadSceneMode.Single);
    }

    public void quitGame()
    {
		StartCoroutine (GameObject.Find("Fade").GetComponent<FadeController>().Fading());
        Application.Quit();
    }

    public void continuarPartida()
    {
        SaveLoad.instance.Load();
        StartCoroutine (GameObject.Find("Fade").GetComponent<FadeController>().Fading());
        SceneManager.LoadSceneAsync("Hub", LoadSceneMode.Single);
    }

	public void NewGameMenu()
	{
		if (SaveLoad.instance.saveDataExists ()) 
		{
			newGameMenu.SetActive (true);
		}
		else 
		{
			NewGame ();
		}
	}
}
