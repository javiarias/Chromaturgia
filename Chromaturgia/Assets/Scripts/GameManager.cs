using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	//[Range(0, 1)] //esto hace que en el inspector se vea como un slider, para hacer más fácil la edición de valores y asegurar que no se sale de un mínimo y un máximo
	public Vector4 colors; // x = R ,  y = G ,  z = B ,  w = ALPHA
    [Range(0, 1)]
    public float bulletAmount; //la cantidad que se resta/suma cuando se pierde/gana color

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
    public bool inHub = false;

    void Awake()
    {
		if (instance == null) 
		{
			instance = this;

			DontDestroyOnLoad (this.gameObject);

		} else 
		{
			Destroy (this.gameObject);
		}

        if (SceneManager.GetActiveScene().name == "Hub")
        {
            inHub = true;
        }
    }

    public void Start()
    {
		
		// caching 
		redLevels = GameObject.FindGameObjectWithTag("RedLevels").GetComponent<Text>();
		greenLevels = GameObject.FindGameObjectWithTag("GreenLevels").GetComponent<Text>();
		blueLevels = GameObject.FindGameObjectWithTag("BlueLevels").GetComponent<Text>();
        
		// initializing 
		colors.x = 0.9f;
		colors.y = 0.9f;
		colors.z = 0.9f;
		colors.w = 1;
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

    void Update()
    {
        if (!inHub)
        {
            redLevels.text = Mathf.Round(colors.x / bulletAmount).ToString();
            greenLevels.text = Mathf.Round(colors.y / bulletAmount).ToString();
            blueLevels.text = Mathf.Round(colors.z / bulletAmount).ToString();
            CheckHealth();
        }
    }

    void CheckHealth()
    {
		if (colors.x == 0 && colors.y == 0 && colors.z == 0) //(FirstSmallerThanSecond(colors, deathThreshold))
        {
            Death();
        }
    }

    void Death() 
	{
		Debug.Log ("Muerte");
	}
    
}


