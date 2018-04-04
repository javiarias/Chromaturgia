using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputs : MonoBehaviour {

	public Sprite Shoot, Act, Talk;

	[HideInInspector]
	public NPCBehaviour npc;
    public OrbLeverBehaviour orbLever;
	Animator animator;

	Image currentSprite;
	GunController playerGun;

	void Start () 
	{
		currentSprite = GameObject.FindGameObjectWithTag("Spacebar").GetComponent<Image>();
        playerGun = GameObject.FindGameObjectWithTag ("Gun").GetComponent<GunController> ();
	}

	public void ChangeSprite(GameManager.Action action)
	{
		if (action == GameManager.Action.Shoot) 
		{
			currentSprite.sprite = Shoot;
		}
		else if (action == GameManager.Action.Interact) 
		{
			currentSprite.sprite = Act;
		}
		else if (action == GameManager.Action.Talk) 
		{
			currentSprite.sprite = Talk;
		}
	}

	void CheckInput ()
	{
		if (Input.GetKeyUp (KeyCode.Space))
		{
			if (GameManager.instance.currentAction == GameManager.Action.Shoot) 
			{
				playerGun.ShootLaser ();
			} 
			else if (GameManager.instance.currentAction == GameManager.Action.Talk) 
			{
				npc.DisplayText();
			}
			else if(GameManager.instance.currentAction == GameManager.Action.Interact)
			{
				animator = orbLever.gameObject.GetComponent<Animator> ();

				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Off"))
				{
					orbLever.SendSignal();
					animator.SetBool ("On", true);
				} 
				else if (animator.GetCurrentAnimatorStateInfo (0).IsName ("On")) 
				{
					orbLever.SendSignal();
					animator.SetBool ("On", false);
				}
			}
		}
    }

	void Update () 
	{
		ChangeSprite (GameManager.instance.currentAction);
		CheckInput ();
	}
}
