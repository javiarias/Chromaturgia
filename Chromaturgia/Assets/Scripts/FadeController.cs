using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour {

	Animator animFade;
	Image Fade;
    GameObject player;

	void Start()
	{
		animFade = gameObject.GetComponent<Animator>();
		Fade = gameObject.GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player");
	}

    void RestartMovement()
    {
        player.GetComponent<Movement>().canMove = true;
    }

    public IEnumerator Fading()
	{
        Invoke("RestartMovement", 0.3f);
		animFade.SetBool ("Fade", true);
        yield return new WaitUntil (()=> Fade.color.a == 1);
    }
}
