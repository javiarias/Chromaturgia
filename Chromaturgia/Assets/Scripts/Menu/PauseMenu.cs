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

	float sliderBrightnessValue;
	float sliderSaturationValue;
	float soundVolume;
	float musicVolume;

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
        brightnessSlider.value = sliderBrightnessValue;

		sliderSaturationValue = (GameManager.instance.saturation * 50) + 50;
        saturationSlider.value = sliderSaturationValue;

		soundVolume = GameManager.instance.soundVolume;
        soundSlider.value = soundVolume;

		musicVolume = GameManager.instance.musicVolume;
        musicSlider.value = musicVolume;
    }

	void Update()
    {
        brightText.text = sliderBrightnessValue + "%";
        saturationText.text = sliderSaturationValue + "%";
        soundText.text = ((soundVolume + 48) * 100) / 48 + "%";
        musicText.text = ((musicVolume + 48) * 100) / 48 + "%";

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
            soundVolume = -80;
        }
        else
        {
            soundVolume = volume;
        }
        needsBrightnessUpdate = true;
		audioMixer.SetFloat ("SoundVolume",volume);

        GameManager.instance.soundVolume = soundVolume;
        SaveLoad.soundVolume = GameManager.instance.soundVolume;
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
        needsBrightnessUpdate = true;
        musicMixer.SetFloat("MusicVolume", volume);

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
