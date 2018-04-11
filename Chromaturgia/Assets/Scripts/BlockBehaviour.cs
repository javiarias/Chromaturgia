using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour 
{
	Color blockColor;
	Animator animator;
	Animator laserAnimator;
	Rigidbody2D laserRb2d;

	void Start () 
	{
		blockColor = gameObject.GetComponent <SpriteRenderer> ().color;
		animator = gameObject.GetComponent<Animator> ();
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		laserAnimator = coll.GetComponent<Animator> ();
		laserRb2d = coll.GetComponent<Rigidbody2D> ();

		if (coll.gameObject.tag == "Laser") 
		{
			if (coll.gameObject.GetComponent <SpriteRenderer> ().color == blockColor) 
			{
				laserRb2d.velocity = Vector2.zero;
				laserAnimator.SetBool ("Destroyed",true);
				Destroy (coll.gameObject, 0.35f);

				animator.SetBool ("Destroyed", true);
				Destroy (gameObject,0.75f);
			} 
			else 
			{
				laserRb2d.velocity = Vector2.zero;
				laserAnimator.SetBool ("Destroyed",true);
				Destroy (coll.gameObject, 0.35f);
			}
		}
	}
}
