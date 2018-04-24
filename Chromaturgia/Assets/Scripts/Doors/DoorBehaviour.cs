using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBehaviour : MonoBehaviour {
	public string Scene = "Puzle 1";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			SceneManager.SetActiveScene (SceneManager.GetSceneByName (Scene));
			SceneManager.LoadScene (Scene, LoadSceneMode.Additive);
		}
	}
}
