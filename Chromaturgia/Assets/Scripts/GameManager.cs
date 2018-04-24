﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class GameManager : MonoBehaviour {

    public const int MAX_LEVELS = 5;

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
    [HideInInspector]
    public string sceneToLoad = "";
    [HideInInspector]
    public float playerInitialRotation;

    [HideInInspector]
    public bool[] completedLevels = new bool[MAX_LEVELS];

    [HideInInspector]
    public float brightness;
    [HideInInspector]
    public float saturation;

    PostProcessingBehaviour cam;
    ColorGradingModel.Settings auxSettings;

    void Awake()
    {
		if (instance == null) 
		{
			instance = this;

			DontDestroyOnLoad (this.gameObject);

            // initializing goes in Awake since it doesn't depend on other gO
            colors.x = 0.9f;
            colors.y = 0.9f;
            colors.z = 0.9f;
            colors.w = 1;
        }
        else 
		{
			Destroy (this.gameObject);
		}

        playerInitialRotation = 180;


        if (!puzzleComplete && SceneManager.GetActiveScene().name == "Hub")
        {
            entryPosition = new Vector3(-46.48f, -1.94f, 0f);
        }
    }

    public void Start()
    {
        inHub = SceneManager.GetActiveScene().name == "Hub";
        Debug.Log(playerInitialRotation);
    }

    public void Caching()
    {
        redLevels = GameObject.FindGameObjectWithTag("RedLevels").GetComponent<Text>();
        greenLevels = GameObject.FindGameObjectWithTag("GreenLevels").GetComponent<Text>();
        blueLevels = GameObject.FindGameObjectWithTag("BlueLevels").GetComponent<Text>();
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

    public void DecreaseColor(Option color)
    {
        if (color == Option.Red && colors.x > 0f)
        {
            colors.x -= bulletAmount;
            if (colors.x < bulletAmount)
                colors.x = 0;
        }
        else if (color == Option.Green && colors.y > 0f)
        {
            colors.y -= bulletAmount;
            if (colors.y < bulletAmount)
                colors.y = 0;
        }
        else if (color == Option.Blue && colors.z > 0f)
        {
            colors.z -= bulletAmount;
            if (colors.z < bulletAmount)
                colors.z = 0;
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
        // Transforms the text into an int
        int index = (int)GameObject.FindGameObjectWithTag("Puzzle").GetComponent<Text>().text.ToCharArray()[0] - 48;

        completedLevels[index] = true;

        SaveLoad.instance.Save();
        Debug.Log("Saved");
    }

    void Update()
    {
        inHub = SceneManager.GetActiveScene().name == "Hub";

        if (SceneManager.GetActiveScene().name != "Hub")
        {
            redLevels = GameObject.FindGameObjectWithTag("RedLevels").GetComponent<Text>();
            greenLevels = GameObject.FindGameObjectWithTag("GreenLevels").GetComponent<Text>();
            blueLevels = GameObject.FindGameObjectWithTag("BlueLevels").GetComponent<Text>();

            redLevels.text = Mathf.Round(colors.x / bulletAmount).ToString();
            greenLevels.text = Mathf.Round(colors.y / bulletAmount).ToString();
            blueLevels.text = Mathf.Round(colors.z / bulletAmount).ToString();
            CheckHealth();
        }
    }

    void CheckHealth()
    {
		if (colors.x == 0 && colors.y == 0 && colors.z == 0) 
        {
            Death();
        }
    }

    void Death() 
	{
		Debug.Log ("Muerte");
	}
    
}


