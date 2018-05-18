using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoorScript : MonoBehaviour {
    
    GameObject child, player;
    string sceneName = "";
    public string sceneToLoad = "";
    bool scenePositionPointer = false;
    public Vector4 colorAmounts;
    float angle;
    string gmSceneToLoad;

    private void Start()
    {
        child = gameObject.transform.GetChild(0).gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        sceneName = SceneManager.GetActiveScene().name;
        
        child.SetActive(true);
        GameManager.instance.colors = colorAmounts;

        gmSceneToLoad = GameManager.instance.sceneToLoad;
        scenePositionPointer = (sceneName == "Puzle 0-0" && sceneToLoad == "") || (sceneName != "Puzle 0-0" && (sceneToLoad.Contains("Puzle") || sceneToLoad == "MainMenu"));

        if (scenePositionPointer)
        {
            player.transform.position = gameObject.transform.GetChild(1).position;
            angle = (gameObject.transform.eulerAngles.z * Mathf.PI) / 180;
            Vector2 auxVect = new Vector2(Mathf.Round(Mathf.Sin(angle)), -Mathf.Round(Mathf.Cos(angle)));
            player.GetComponent<Movement>().UpdateOrientation(auxVect);
        }
    }

    private void Update()
    {
        if (GameManager.instance.puzzleComplete)
        {
            child.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.sceneToLoad = sceneToLoad;
            GameManager.instance.puzzleComplete = false;
            SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
        }
    }
}
