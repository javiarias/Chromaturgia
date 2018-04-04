using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour {

	public GameObject message;

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			GameManager.instance.currentAction = GameManager.Action.Talk;
			coll.GetComponent<PlayerInputs>().npc = this;
		}
	}

	void OnTriggerExit2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			GameManager.instance.currentAction = GameManager.Action.Shoot;
			HideText ();
		}
	}
		
	public void DisplayText()
	{
		message.SetActive (true);
	}

	public void HideText()
	{
		message.SetActive (false);
	}
}
