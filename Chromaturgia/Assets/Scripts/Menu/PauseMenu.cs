using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour {

	public AudioMixer audioMixer;
    public AudioMixer musicMixer;

	public static bool GameIsPaused = false;

	public GameObject pauseMenu;
	public EventSystem eventSystemPauseMenu;
	public Button volverOptionsMenu;

	bool needsBrightnessUpdate, needsSaturationUpdate, needsSoundUpdate, needsMusicUpdate;

	public float sliderBrightnessValue;
    public float sliderSaturationValue;
    public float soundVolume;
	public float musicVolume;

	public Text brightText;
	public Text saturationText;
	public Text soundText;
	public Text musicText;

    public Slider brightnessSlider;
    public Slider saturationSlider;
    public Slider soundSlider;
    public Slider musicSlider;

    EventSystem eventsystem;

	void Awake()
	{
		needsBrightnessUpdate = true;
		needsSaturationUpdate = true;
        needsSoundUpdate = true;
        needsMusicUpdate = true;
	}

	void Start()
	{
        sliderBrightnessValue = (GameManager.instance.brightness * 50) + 50;
        try { brightnessSlider.value = sliderBrightnessValue; }
        catch { }

		sliderSaturationValue = (GameManager.instance.saturation * 50) + 50;
        try { saturationSlider.value = sliderSaturationValue; }
        catch { }

		soundVolume = GameManager.instance.soundVolume;
        try { soundSlider.value = soundVolume; }
        catch { }

        Debug.Log(musicVolume);
        Debug.Log(GameManager.instance.musicVolume);
        musicVolume = GameManager.instance.musicVolume;
        try { musicSlider.value = musicVolume; }
        catch { }
        Debug.Log(musicVolume);
        Debug.Log(GameManager.instance.musicVolume);
    }

	void Update()
    {
        brightText.text = Mathf.RoundToInt(sliderBrightnessValue) + "%";
        saturationText.text = Mathf.RoundToInt(sliderSaturationValue) + "%";
        soundText.text = Mathf.RoundToInt(((soundVolume + 40) * 100) / 48) + "%";
        musicText.text = Mathf.RoundToInt(((musicVolume + 40) * 100) / 48) + "%";

        if (GameIsPaused)
        {
            eventsystem = EventSystem.current;
            if (eventsystem.currentSelectedGameObject == null)
            {
                eventsystem.SetSelectedGameObject(eventsystem.firstSelectedGameObject);
            }
        }

        if (Input.GetKeyDown (KeyCode.Escape))
		{
			if (eventSystemPauseMenu.enabled)
			{
				if (GameIsPaused)
					resume ();
				else
					pause ();
			} 
			else if (volverOptionsMenu.isActiveAndEnabled)
			{
				volverOptionsMenu.onClick.Invoke ();
				FindObjectOfType<AudioManager> ().Play ("MenuBack");
			}
		}

		if(GameIsPaused && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)))
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
    }

	public void setBright(float brillo)
	{
		sliderBrightnessValue = brillo;
		needsBrightnessUpdate = true;

		GameManager.instance.brightness = (sliderBrightnessValue - 50) / 50;
		SaveLoad.instance.brightness = GameManager.instance.brightness;
        SaveLoad.instance.SaveConfig();
	}

	public void setSaturation(float saturation)
	{
		sliderSaturationValue = saturation;
		needsSaturationUpdate = true;

		GameManager.instance.saturation = (sliderSaturationValue - 50) / 50;
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

	public void resume()
	{
		FindObjectOfType<AudioManager> ().Play ("PausaOff");
		pauseMenu.SetActive (false);
		Time.timeScale = 1;
		GameIsPaused = false;
	}

	public void resetLevel()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		Time.timeScale = 1;
		GameIsPaused = false;
	}

	void pause()
	{
		FindObjectOfType<AudioManager> ().Play ("PausaOn");
		EventSystem.current = eventSystemPauseMenu;
		pauseMenu.SetActive (true);
		Time.timeScale = 0;
		GameIsPaused = true;
	}

	public void loadMainMenu()
	{
		Time.timeScale = 1;
		GameIsPaused = false;
		SceneManager.LoadScene ("MainMenu");
	}

    public void alternaFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
