using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class UpdatedDoorLogic : MonoBehaviour {
    
  
    public static string[] levelNames = {"", "Puzle 1-1", "Puzle 2-1", "Puzle 3-1" };
    public int desiredLevel;
    Rigidbody2D playerRB;

    int levelIndex;
    bool movePlayer = false;

	void Start ()
    {
        levelIndex = (int)GameObject.FindGameObjectWithTag("Puzzle").GetComponent<Text>().text.ToCharArray()[0] - 48;
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        if (desiredLevel == levelIndex)
        {
            movePlayer = true;
        }
    }
	
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            if (desiredLevel != levelIndex)
            {
                SceneManager.LoadScene(levelNames[desiredLevel]);
            }
            else if (SceneManager.GetActiveScene().name != "Hub")
            {
                SceneManager.LoadScene("Hub");
            }
        }
    }

    void FixedUpdate()
    {
        if (movePlayer)
        {
            playerRB.position.Set(gameObject.transform.position.x, gameObject.transform.position.y);
        }
    }
}
