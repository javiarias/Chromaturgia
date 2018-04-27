using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    bool needsBrightnessUpdate, needsSaturationUpdate;

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
	public Button continuarPartida;

    GameObject mainMenu;
	GameObject currentGame;

    EventSystem eventsystem;

    void Awake()
    {
        soundVolume = 50;
        musicVolume = 50;

        needsBrightnessUpdate = true;
        needsSaturationUpdate = true;
    }

    void Start()
    {
        thinkAnim = GameObject.FindGameObjectWithTag("ThinkMenu").GetComponent<Animator>();
        mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
		currentGame = GameObject.FindGameObjectWithTag("CurrentGame");
		currentGame.SetActive (false);
        mainMenu.SetActive(false);

        sliderBrightness = (GameManager.instance.brightness * 50) + 50;
        sliderSaturation = (GameManager.instance.saturation * 50) + 50;
    }

    void Update()
    {
        brightText.text = sliderBrightness + "%";
        saturationText.text = sliderSaturation + "%";
        soundText.text = soundVolume + "%";
        musicText.text = musicVolume + "%";

        eventsystem = EventSystem.current;
		if(eventsystem!=null)
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
    }

    public void pulsaEspacio()
    {
        thinkAnim.SetBool("Activated", true);

        Invoke("activeMenu", 1.7f);
    }

    void activeMenu()
    {
		if (SaveLoad.instance.existePartida) 
		{
			currentGame.SetActive (true);
		}
		else
			continuarPartida.GetComponentInChildren<Text> ().canvasRenderer.SetAlpha (0.5f);
		
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
        SaveLoad.instance.Save();
    }

    public void setSaturation(float saturation)
    {
        sliderSaturation = saturation;
        needsSaturationUpdate = true;

        GameManager.instance.saturation = (sliderSaturation - 50) / 50;
        SaveLoad.instance.saturation = GameManager.instance.saturation;
        SaveLoad.instance.Save();
    }

    public void setSoundVolume(float volume)
    {
        soundVolume = volume;
        SaveLoad.instance.soundVolume = volume;

        SaveLoad.instance.Save();
    }

    public void setMusicVolume(float volume)
    {
        musicVolume = volume;
        SaveLoad.instance.musicVolume = volume;

        SaveLoad.instance.Save();
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
