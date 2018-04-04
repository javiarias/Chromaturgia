using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserLogic : MonoBehaviour {

	Animator animator;
	Rigidbody2D rb2d;

	[HideInInspector]
	public GameManager.Option turretColor;
	[HideInInspector]
	public float speed = 50f;

	Vector2 direction;

	void Start ()
	{
		direction = gameObject.transform.up;

		animator = gameObject.GetComponent<Animator> ();
		rb2d = gameObject.GetComponent<Rigidbody2D> ();

		rb2d.AddForce (direction * speed, ForceMode2D.Force);
	}

	void OnTriggerEnter2D (Collider2D coll)
	{

		if (coll.gameObject.tag == "Player")
		{
			coll.gameObject.GetComponent<Damage> ().Flash();
			GameManager.instance.DecreaseColor (turretColor);
		}
        if (coll.gameObject.tag != "Transparent")
        {
			rb2d.velocity = Vector2.zero;
			animator.SetBool ("Destroyed",true);
			Destroy (gameObject, 0.35f);
        }
	}
}
