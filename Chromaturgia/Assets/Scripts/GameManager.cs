using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class GameManager : MonoBehaviour {

    public const int MAX_LEVELS = 20;
    //const float DEATH_ERROR = 0.05f;          //las razones de esta edición se hallan en Slack.

    public static GameManager instance = null;

	//[Range(0, 1)] //esto hace que en el inspector se vea como un slider, para hacer más fácil la edición de valores y asegurar que no se sale de un mínimo y un máximo
	public Vector4 colors; // x = R ,  y = G ,  z = B ,  w = ALPHA
    //[Range(0, 1)]
    [HideInInspector]
    public float bulletAmount = 0.05f; //la cantidad que se resta/suma cuando se pierde/gana color

    [HideInInspector] //esto esconde la variable en el inspector, aunque sea pública
    public enum Option { Red, Green, Blue };
    [HideInInspector]
    public Option chosenColor = Option.Red;

	[HideInInspector] //esto esconde la variable en el inspector, aunque sea pública
	public enum Action { Shoot, Talk, Interact };
	[HideInInspector]
	public Action currentAction = Action.Shoot;

	Text redLevels, greenLevels, blueLevels;

    [HideInInspector]
    public bool inHub, puzzleComplete = false;
    [HideInInspector]
    public Vector3 entryPosition;
    //[HideInInspector]
    public string sceneToLoad = "";
    [HideInInspector]
    public float playerInitialRotation;

    //[HideInInspector]
    public bool[] completedLevels = new bool[MAX_LEVELS];
    //[HideInInspector]
    public bool level1Complete = false, level2Complete = false, level3Complete = false;
    //[HideInInspector]
    public bool redPiecePicked, greenPiecePicked, bluePiecePicked;

    [HideInInspector]
    public float brightness = 0;
    [HideInInspector]
    public float saturation = 0;
    [HideInInspector]
    public float soundVolume = 50, musicVolume = 50;

    PostProcessingBehaviour cam;
    ColorGradingModel.Settings auxSettings;

    bool testedLevels = false;

	static string sSceneName = null;

    [HideInInspector]
    public float playtime;

    void Awake()
    {
		if (instance == null) 
		{
			instance = this;

			DontDestroyOnLoad (this.gameObject);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // initializing goes in Awake since it doesn't depend on other gO
            colors.x = 0.9f;
            colors.y = 0.9f;
            colors.z = 0.9f;
            colors.w = 1;
            bulletAmount = 0.05f;
            sceneToLoad = "";
            level1Complete = false;
            level2Complete = false;
            level3Complete = false;
            completedLevels = new bool[MAX_LEVELS];
        }
        else 
		{
			Destroy (this.gameObject);
		}
    }

	void Start()
	{
		SetMusic ();
	}

    public void Caching()
    {
        redLevels = GameObject.FindGameObjectWithTag("RedLevels").GetComponent<Text>();
        greenLevels = GameObject.FindGameObjectWithTag("GreenLevels").GetComponent<Text>();
        blueLevels = GameObject.FindGameObjectWithTag("BlueLevels").GetComponent<Text>();
    }

	public void StopMusic()
	{
		FindObjectOfType<MusicManager> ().StopAll();
	}

	void SetMusic()
	{
		StopMusic ();

		string pista = "";

		switch(SceneManager.GetActiveScene().name)
		{
			case "Puzle 0-0":
				pista = "Almacen";
				break;

			case "Hub":
				pista = "Hall";
				break;

			case "Intro":
				pista = "Intro";
				break;

			case "GameOver":
				pista = "GameOver";
				break;

			case "Puzle 1-1":
			case "Puzle 1-2":
			case "Puzle 1-3":
				pista = "Nivel1";
				break;

			case "Puzle 2-1":
			case "Puzle 2-2":
			case "Puzle 2-3":
				pista = "Nivel2";
				break;

			case "Puzle 3-1":
			case "Puzle 3-2":
			case "Puzle 3-3":
				pista = "Nivel3";
				break;

			case "TheEnd":
				pista = "Credits";
				break;
		}

		FindObjectOfType<MusicManager>().Play(pista);
	}

    public void ChangeBrightness()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>();

        // may seem redundant but it's the only way it compiles:
        // copy current settings into the temporary variable
        auxSettings = cam.profile.colorGrading.settings;

        // make changes in auxSettings
        auxSettings.basic.postExposure = brightness;

        // move those settings to the actual profile
        cam.profile.colorGrading.settings = auxSettings;
    }

    public void ChangeSaturation()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>();

        auxSettings = cam.profile.colorGrading.settings;
        auxSettings.basic.saturation = saturation + 1;
        cam.profile.colorGrading.settings = auxSettings;
    }

    public void ChangeMusic()
    {
       
    }

    public void ChangeSound()
    {

    }

    public void DecreaseColor(Option color)
    {
        if (color == Option.Red && colors.x > 0f)
        {
            colors.x -= bulletAmount;
			if (colors.x < bulletAmount / 2) 
			{
				colors.x = 0;
				FindObjectOfType<AudioManager>().Play("SinMunicion");
			}
			else
				FindObjectOfType<AudioManager>().Play("Disparo");
        }
        else if (color == Option.Green && colors.y > 0f)
        {
            colors.y -= bulletAmount;
			if (colors.y < bulletAmount/2)
			{
                colors.y = 0;
				FindObjectOfType<AudioManager>().Play("SinMunicion");
			}
			else
				FindObjectOfType<AudioManager>().Play("Disparo");
        }
        else if (color == Option.Blue && colors.z > 0f)
        {
            colors.z -= bulletAmount;
			if (colors.z < bulletAmount/2)
			{
                colors.z = 0;
				FindObjectOfType<AudioManager>().Play("SinMunicion");
			}
			else
				FindObjectOfType<AudioManager>().Play("Disparo");
        }
    }

    public void IncreaseColor(Option color)
    {
        if (color == Option.Red)
        {  
            colors.x += bulletAmount;
            if (colors.x > 1 - bulletAmount)
                colors.x = 1;
        }
        else if (color == Option.Green)
        {
            colors.y += bulletAmount;
            if (colors.y > 1 - bulletAmount)
                colors.y = 1;
        }
        else if (color == Option.Blue)
        {
            colors.z += bulletAmount;
            if (colors.z > 1 - bulletAmount)
                colors.z = 1;
        }
    }

    public void SetPuzzleAsCompleted()
    {
        //CÓDIGO BELÉN ORIGINAL
        // Transforms the text into an int
        //int index = (int)GameObject.FindGameObjectWithTag("Puzzle").GetComponent<Text>().text.ToCharArray()[0] - 48;

        //CÓDIGO JAVI
        //extrae del nombre de la escena el número del nivel y del puzle
        char[] tempArray = SceneManager.GetActiveScene().name.TrimStart("Puzle".ToCharArray()).Replace(" ", String.Empty).Replace("-", String.Empty).ToCharArray();

        int index = int.Parse(tempArray[1].ToString());
        if (tempArray[0] == '2')
        {
            index += 3;
        }
        else if (tempArray[0] == '3')
        {
            index += 6;
        }

        completedLevels[index] = true;
        puzzleComplete = true;

        //SaveLoad.instance.Save();                         //eliminado para asegurar que la partida se guarda SI Y SOLO SI el jugador sale del puzle
    }

    bool WholeLevelComplete(int zone)
    {
        bool isComplete = true;

        if (zone == 1)
        {
            for (int i = 1; i <= 3; ++i)
            {
                isComplete &= completedLevels[i];
            }
        }
        else if (zone == 2)
        {
            for (int i = 4; i <= 6; ++i)
            {
                isComplete &= completedLevels[i];
            }
        }
        else if (zone == 3)
        {
            for (int i = 7; i <= 9; ++i)
            {
                isComplete &= completedLevels[i];
            }
        }

        return isComplete;
    }

    void SetAsCompleted(int zone)
    {
        if (zone == 1)
        {
            level1Complete = true;
        }
        else if (zone == 2)
        {
            level2Complete = true;
        }
        else if (zone == 3)
        {
            level3Complete = true;
        }
    }

    void Update()
    {
		if (sSceneName != SceneManager.GetActiveScene ().name) {
			SetMusic ();
			sSceneName = SceneManager.GetActiveScene ().name;
		}

        inHub = SceneManager.GetActiveScene().name == "Hub";

        if (SceneIsPuzzle())
        {
            redLevels = GameObject.FindGameObjectWithTag("RedLevels").GetComponent<Text>();
            greenLevels = GameObject.FindGameObjectWithTag("GreenLevels").GetComponent<Text>();
            blueLevels = GameObject.FindGameObjectWithTag("BlueLevels").GetComponent<Text>();

            redLevels.text = Mathf.Round(colors.x / bulletAmount).ToString();
            greenLevels.text = Mathf.Round(colors.y / bulletAmount).ToString();
            blueLevels.text = Mathf.Round(colors.z / bulletAmount).ToString();
            CheckHealth();

            sceneToLoad = SceneManager.GetActiveScene().name;
        }

        if (inHub)
        {
            for (int i = 1; i <= 3; i++)
            {
                if (WholeLevelComplete(i))
                {
                    SetAsCompleted(i);
                }
            }
        }

        if(SceneManager.GetActiveScene().name.Contains("Puzle") || SceneManager.GetActiveScene().name == "Hub")
        {
            playtime += Time.deltaTime;
        }
    }

    bool SceneIsPuzzle()
    {
        return (SceneManager.GetActiveScene().name.Contains("Puzle"));
    }

    void CheckHealth()
    {
		if (colors.x == 0 && colors.y == 0 && colors.z == 0)            //las razones de esta edición se hallan en Slack.
        {
            Death();
        }
    }

    void Death() 
	{
        SceneManager.LoadScene("GameOver");

		FindObjectOfType<AudioManager>().Play("Death");
	}

    public void QuitGame()
    {
        Debug.Log("Goodbye");

        Application.Quit();
    }
}


