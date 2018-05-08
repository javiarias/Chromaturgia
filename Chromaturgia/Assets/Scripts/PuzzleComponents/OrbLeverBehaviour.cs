using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbLeverBehaviour : MonoBehaviour {

	public int masterIdentifier = 0;
    RotationAndDeactivationController[] objectList = null;
    Animator animator;
    bool deactivate = false;

    private void Awake()
    {
        if (objectList == null)
        {
            objectList = FindObjectsOfType<RotationAndDeactivationController>();
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player" && gameObject.tag == "Lever")
        {
            GameManager.instance.currentAction = GameManager.Action.Shoot;
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Player" && gameObject.tag == "Lever")
        {
            GameManager.instance.currentAction = GameManager.Action.Interact;
            coll.GetComponent<PlayerInputs>().orbLever = this;
        }
    }

    public void SendSignal()
    {
        if (!deactivate)
        {
            deactivate = true;
            animator = gameObject.GetComponent<Animator>();
            bool orbLeverState = false;

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Off"))
            {
				FindObjectOfType<AudioManager> ().Play ("SwitchOff");
                orbLeverState = true;
            }

            animator.SetBool("On", orbLeverState);
			FindObjectOfType<AudioManager> ().Play ("SwitchOn");
            foreach (RotationAndDeactivationController objectInstance in objectList)
            {
                foreach (int objectIdentifier in objectInstance.identifierList)
                {
                    if (objectIdentifier == masterIdentifier)
                    {
                        objectInstance.OrbLeverSignal();
                    }
                }
            }
            Invoke("Reactivation", 0.5f);
        }
    }

    void Reactivation()
    {
        deactivate = false;
    }
}
