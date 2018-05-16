using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroText : MonoBehaviour {

	TextMesh text;
	string sentence;

	void Start () {
		text = GetComponent<TextMesh> ();
		sentence = text.text;
		text.text = "";
		StartCoroutine (TypeSentence (sentence));
	}
	
	IEnumerator TypeSentence(string sentence)
	{
		text.text = "";
		foreach(char letter in sentence.ToCharArray())
		{
			text.text += letter;
			FindObjectOfType<AudioManager>().Play("Talk");
			if (letter == ',' || letter == '.' || letter == '?' || letter == '!')
				yield return new WaitForSeconds (0.2f);
			else
				yield return new WaitForSeconds (0.03f);;
		}
	}
}
