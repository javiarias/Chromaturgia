using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour 
{
	public Transform salida;

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			Time.timeScale = 0;
			DoorAnimation ();
			coll.gameObject.transform.position = salida.position;
			Time.timeScale = 1;
		}
	}

	void DoorAnimation()
	{
		StartCoroutine (GameObject.Find("Fade").GetComponent<FadeController>().Fading());
		GameObject.Find ("Fade").GetComponent<Animator> ().Rebind ();
	}
}
