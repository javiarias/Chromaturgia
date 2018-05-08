using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCBehaviour : MonoBehaviour {

	public GameObject message;
	public GameObject background;

	TextMesh text;
	string sentence;
	bool talk;

	void Start()
	{
		text = message.GetComponent<TextMesh> ();
		sentence = text.text;
		text.text = "";
		talk = false;
	}


    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            GameManager.instance.currentAction = GameManager.Action.Shoot;
            HideText();
        }
    }

    void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			GameManager.instance.currentAction = GameManager.Action.Talk;
			coll.GetComponent<PlayerInputs>().npc = this;
		}
	}

	public void DisplayText()
	{
		if (!talk) 
		{
			message.SetActive (true);
			StartCoroutine (TypeSentence (sentence));
			talk = true;
		}
	}

	public void HideText()
	{
		StopAllCoroutines();
		message.SetActive (false);
		talk = false;
	}

	IEnumerator TypeSentence(string sentence)
	{
		text.text = "";
		foreach(char letter in sentence.ToCharArray())
		{
			text.text += letter;
			FindObjectOfType<AudioManager>().Play("Talk");
			if (letter == ',' || letter == '.' || letter == '?' || letter == '!')
				yield return new WaitForSeconds (0.5f);
			else
				yield return new WaitForSeconds (0.05f);;
		}
	}
}
