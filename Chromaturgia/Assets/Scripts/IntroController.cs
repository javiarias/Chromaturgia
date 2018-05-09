using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour {

    void Start()
    {
        Invoke("LoadMainMenu", 38f);
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadMainMenu();
        }
	}

    void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }
}
