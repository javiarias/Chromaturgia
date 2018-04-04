using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismBehaviour : MonoBehaviour {

	PrismMouthBehaviour mouth;
	Animator animator;

	void Start()
	{
		mouth = gameObject.GetComponentInChildren<PrismMouthBehaviour> ();
		animator = gameObject.GetComponent<Animator> ();
	}

	void OnTriggerEnter2D (Collider2D coll) 
	{
		if (coll.gameObject.tag == "Laser") 
		{
			animator.SetTrigger ("Shoot");

			mouth.ShootLaser (coll.GetComponent<SpriteRenderer>().color);

			Destroy (coll.gameObject);
		}
	}


}

