using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoorScript : MonoBehaviour {
    
    GameObject child;
    string sceneName = "";
    public string sceneToLoad = "";
    public Vector4 colorAmounts;

    private void Awake()
    {
        child = gameObject.transform.GetChild(0).gameObject;
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "Hub")
        {
            child.SetActive(true);
            GameManager.instance.colors = colorAmounts;
            GameManager.instance.entryPosition = gameObject.transform.GetChild(1).position;
            GameManager.instance.playerInitialRotation = gameObject.transform.eulerAngles.z;
        }

        else if (sceneName == "Hub" && GameManager.instance.sceneToLoad == sceneToLoad)
        {
            GameManager.instance.entryPosition = gameObject.transform.GetChild(1).position;
            GameManager.instance.playerInitialRotation = gameObject.transform.eulerAngles.z;

            if (GameManager.instance.puzzleComplete)
            {
                GameManager.instance.puzzleComplete = false;
                child.SetActive(true);
                Destroy(this);
            }
        }
    }

    private void Update()
    {
        if (sceneName != "Hub" && GameManager.instance.puzzleComplete)
        {
            child.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (sceneName != "Hub")
            {
                SceneManager.LoadSceneAsync("Hub", LoadSceneMode.Single);
            }
            else
            {
                GameManager.instance.sceneToLoad = sceneToLoad;
                GameManager.instance.puzzleComplete = false;
                SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
            }
        }
    }


}
