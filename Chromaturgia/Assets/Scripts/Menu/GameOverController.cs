using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {

    // auxiliar class to avoid menu breaking when it loads from 
    // other scene and the original game manager and save load 
    // are deleted

    public const float WAIT_TIME = 0.3f;

	public void CallLoad()
    {
        Invoke("ActualLoad", WAIT_TIME);
    }

    public void CallExit()
    {
        GameManager.instance.QuitGame();
    }

    public void ActualLoad()
    {
        bool auxiliar = SaveLoad.instance.Load();
        if (!auxiliar)
        {
            SceneManager.LoadSceneAsync("Hub", LoadSceneMode.Single);
        }
    }
}
