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
        

        if (sceneName != "Hub")
        {
            child.SetActive(true);
            GameManager.instance.colors = colorAmounts;
        }

        if (sceneName == "Hub" && sceneToLoad != "" && sceneToLoad != "Hub" && TestIfPuzzleComplete())
        {
            GameManager.instance.puzzleComplete = false;
            child.SetActive(true);
            Destroy(this);
        }

        gmSceneToLoad = GameManager.instance.sceneToLoad;
        scenePositionPointer = ((sceneName == "Hub" && sceneToLoad == gmSceneToLoad) || (sceneName != "Hub" && sceneToLoad == ""));

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
        if (sceneName != "Hub" && GameManager.instance.puzzleComplete)
        {
            child.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.chosenColor = GameManager.Option.Red;

            if (sceneName != "Hub")
            {
                SceneManager.LoadSceneAsync("Hub", LoadSceneMode.Single);
                SaveLoad.instance.SaveLevels();                //añadido aquí el guardado para asegurar que se guardará la partida si y solo si el jugador sale del puzle
            }
            else
            {
                GameManager.instance.sceneToLoad = sceneToLoad;
                GameManager.instance.puzzleComplete = false;
                SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
            }
        }
    }

    private bool TestIfPuzzleComplete()
    {
        char[] tempArray = sceneToLoad.TrimStart("Puzle".ToCharArray()).Replace(" ", String.Empty).Replace("-", String.Empty).ToCharArray();
        int index = int.Parse(tempArray[1].ToString());
        if (tempArray[0] == '2')
        {
            index += 3;
        }
        else if (tempArray[0] == '3')
        {
            index += 6;
        }

        return GameManager.instance.completedLevels[index];
    }
}
