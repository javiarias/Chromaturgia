using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {

    // auxiliar class to avoid menu breaking when it loads from 
    // other scene and the original game manager and save load 
    // are deleted

    public const float WAIT_TIME = 0.3f;

	public GameObject animation;

    EventSystem eventsystem;

	void Start()
	{
		StartCoroutine(animation.GetComponent<Shaker> ().Shake (5f,.01f));
	}

    private void Update()
    {
        eventsystem = EventSystem.current;
        if (eventsystem != null)
        {
            if (eventsystem.currentSelectedGameObject == null)
                eventsystem.SetSelectedGameObject(eventsystem.firstSelectedGameObject);
        }
    }

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
