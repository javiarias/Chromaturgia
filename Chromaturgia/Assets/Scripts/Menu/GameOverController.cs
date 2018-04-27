using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour {

    // auxiliar class to avoid menu breaking when it loads from 
    // other scene and the original game manager and save load 
    // are deleted

    public const float WAIT_TIME = 0.3f;

	public void CallLoad()
    {
        Invoke("SaveLoad.instance.Load()", WAIT_TIME);
        SaveLoad.instance.Load();
    }

    public void CallExit()
    {
        GameManager.instance.QuitGame();
    }
}
