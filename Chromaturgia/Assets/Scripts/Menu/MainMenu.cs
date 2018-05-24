using System.Collections;
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

    public float sliderBrightness;
    public float sliderSaturation;
    public float soundVolume;
    public float musicVolume;

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
        SaveLoad.instance.Load();
        SaveLoad.instance.LoadConfig();
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
        try { brightnessSlider.value = sliderBrightness; }                      //por alguna razón, al intentar actualizar los sliders por segunda vez saltan excepciones extrañas, pero que no parecen tener ningún tipo de impacto. Esto debería impedir que salten
        catch{ }
        sliderSaturation = (GameManager.instance.saturation * 50) + 50;
        try { saturationSlider.value = sliderSaturation; }
        catch{ }

        soundVolume = GameManager.instance.soundVolume;
        try { soundSlider.value = soundVolume; }
        catch { }
        musicVolume = GameManager.instance.musicVolume;
        try { musicSlider.value = musicVolume; }
        catch { }

        needsBrightnessUpdate = true;
        needsSaturationUpdate = true;
        needsMusicUpdate = true;
        needsSoundUpdate = true;
    }

    void Update()
    {
        brightText.text = Mathf.RoundToInt(sliderBrightness) + "%";
        saturationText.text = Mathf.RoundToInt(sliderSaturation) + "%";
        soundText.text = Mathf.RoundToInt(((soundVolume + 40) * 100) / 48) + "%";
        musicText.text = Mathf.RoundToInt(((musicVolume + 40) * 100) / 48) + "%";

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

		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
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

        if (needsMusicUpdate)
        {
            setMusicVolume(musicVolume);
            needsMusicUpdate = false;
        }

        if (needsSoundUpdate)
        {
            setSoundVolume(soundVolume);
            needsSoundUpdate = false;
        }

        if (!SaveLoad.instance.saveDataExists())
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
            Debug.Log("a");
			currentGame.SetActive (true);
            Debug.Log("a");
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
            audioMixer.SetFloat("SoundVolume", -80);
        }
        else
        {
            audioMixer.SetFloat("SoundVolume", Mathf.Round(volume * 100) / 100);
        }

        soundVolume = Mathf.Round(volume * 100) / 100;

        GameManager.instance.soundVolume = soundVolume;
        SaveLoad.soundVolume = GameManager.instance.soundVolume;
        SaveLoad.instance.SaveConfig();
    }

    public void setMusicVolume(float volume)
    {
        if (volume == -40)
        {
            musicMixer.SetFloat("MusicVolume", -80);
        }
        else
        {
            musicMixer.SetFloat("MusicVolume", Mathf.Round(volume * 100) / 100);
        }

        musicVolume = Mathf.Round(volume * 100) / 100;

        GameManager.instance.musicVolume = musicVolume;
        SaveLoad.instance.musicVolume = GameManager.instance.musicVolume;
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
