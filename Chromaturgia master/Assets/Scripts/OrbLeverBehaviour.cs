using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbLeverBehaviour : MonoBehaviour {

	public int masterIdentifier = 0;
    static RotationAndDeactivationController[] objectList = null;

    private void Awake()
    {
        if (objectList == null)
        {
            objectList = Object.FindObjectsOfType<RotationAndDeactivationController>();
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player" && gameObject.tag == "Lever")
        {
            GameManager.instance.currentAction = GameManager.Action.Interact;
            coll.GetComponent<PlayerInputs>().orbLever = this;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player" && gameObject.tag == "Lever")
        {
            GameManager.instance.currentAction = GameManager.Action.Shoot;
        }
    }

    public void SendSignal()
    {
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
    }
}
