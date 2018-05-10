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

    bool needsBrightnessUpdate, needsSaturationUpdate;

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

    EventSystem eventsystem;

    void Start()
    {
        thinkAnim = GameObject.FindGameObjectWithTag("ThinkMenu").GetComponent<Animator>();
		thinkFX = GameObject.FindGameObjectWithTag("ThinkMenu").GetComponentInChildren<ParticleSystem>().main;
        mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
		currentGame = GameObject.FindGameObjectWithTag("CurrentGame");
		currentGame.SetActive (false);
        mainMenu.SetActive(false);

		thinkFX.startSize = 0f;

        sliderBrightness = (GameManager.instance.brightness * 50) + 50;
        sliderSaturation = (GameManager.instance.saturation * 50) + 50;

        needsBrightnessUpdate = true;
        needsSaturationUpdate = true;

        brightnessSlider.value = sliderBrightness;

        saturationSlider.value = sliderSaturation;

        soundSlider.value = soundVolume;

        musicSlider.value = musicVolume;
    }

    void Update()
    {
        brightText.text = sliderBrightness + "%";
        saturationText.text = sliderSaturation + "%";
        soundText.text = soundVolume+80 + "%";
        musicText.text = musicVolume+80 + "%";

        eventsystem = EventSystem.current;
		if (eventsystem!=null)
		{
	        if (eventsystem.currentSelectedGameObject == null)
	            eventsystem.SetSelectedGameObject(eventsystem.firstSelectedGameObject);
		}

        if (Input.GetKeyDown(KeyCode.Escape))
            volverOptionsMenu.onClick.Invoke();

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
        soundVolume = volume;
        SaveLoad.soundVolume = volume;
		audioMixer.SetFloat ("Volume",volume);

        SaveLoad.instance.SaveConfig();
    }

    public void setMusicVolume(float volume)
    {
        musicVolume = volume;
        SaveLoad.instance.musicVolume = volume;

        SaveLoad.instance.SaveConfig();
    }

    public void NewGame()
    {
        SaveLoad.instance.Reset();
        SaveLoad.instance.Load();
		StartCoroutine (GameObject.Find("Fade").GetComponent<FadeController>().Fading());
        SceneManager.LoadScene("Puzle 0-0");
    }

    public void quitGame()
    {
		StartCoroutine (GameObject.Find("Fade").GetComponent<FadeController>().Fading());
        Application.Quit();
    }

    public void continuarPartida()
    {
		StartCoroutine (GameObject.Find("Fade").GetComponent<FadeController>().Fading());
        SaveLoad.instance.Load();
    }
}
