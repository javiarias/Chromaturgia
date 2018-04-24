using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputs : MonoBehaviour {

	public Sprite Shoot, DisabledShoot, Act, Talk;

    [HideInInspector]
    public bool gunEnabled = false;
    [HideInInspector]
	public NPCBehaviour npc;
    [HideInInspector]
    public OrbLeverBehaviour orbLever;

	Image currentSprite;
    Text spacebarText;
	GunController playerGun;

    void Start () 
	{
        currentSprite = GameObject.FindGameObjectWithTag("Spacebar").GetComponent<Image>();
        spacebarText = currentSprite.transform.Find("Text").GetComponent<Text>();
        spacebarText.text = "Spacebar";
        playerGun = GameObject.FindGameObjectWithTag ("Gun").GetComponent<GunController> ();
        if (!GameManager.instance.inHub)
        {
            gunEnabled = true;
        }
        gameObject.GetComponent<Movement>().canMove = true;
        gameObject.transform.position = GameManager.instance.entryPosition;
    }

	public void ChangeSprite(GameManager.Action action)
	{
		if (action == GameManager.Action.Shoot) 
		{
            if (GameManager.instance.inHub)
            {
                currentSprite.sprite = DisabledShoot;
                spacebarText.text = "----------";
            }
            else
            {
                currentSprite.sprite = Shoot;
            }
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
			if (GameManager.instance.currentAction == GameManager.Action.Shoot && gunEnabled) 
			{
				playerGun.ShootLaser ();
			} 
			else if (GameManager.instance.currentAction == GameManager.Action.Talk) 
			{
				npc.DisplayText();
			}
			else if (GameManager.instance.currentAction == GameManager.Action.Interact)
			{
				orbLever.SendSignal();
			}
		}
    }

	void Update () 
	{
		ChangeSprite (GameManager.instance.currentAction);
        if (!PauseMenu.GameIsPaused)
        {
            CheckInput();
        }
	}
}
