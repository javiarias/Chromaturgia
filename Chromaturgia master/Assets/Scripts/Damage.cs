using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

	SpriteRenderer renderer;

	void Start () 
	{
		renderer = gameObject.GetComponent<SpriteRenderer> ();
	}

	public void Flash()
	{
		renderer.color = Color.red;
		Invoke ("NormalColor", 0.25f);
	}

	public void NormalColor()
	{
		renderer.color = Color.white;
	}
}
