using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour {

	Animator animFade;
	Image Fade;

	void Start()
	{
		animFade = gameObject.GetComponent<Animator>();
		Fade = gameObject.GetComponent<Image>();
	}

	public IEnumerator Fading()
	{
		animFade.SetBool ("Fade", true);
		yield return new WaitUntil (()=> Fade.color.a == 1);
	}
}
