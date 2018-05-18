using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour 
{
	public Transform exit;
    public Transform cameraPosition;
    bool needsCameraUpdate = false;

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			Time.timeScale = 0;
            coll.gameObject.GetComponent<Movement>().canMove = false;
            DoorAnimation ();

			coll.gameObject.transform.position = exit.position;

            coll.gameObject.GetComponent<Movement>().canMove = true;

            GameManager.instance.MoveMainCamera(cameraPosition.position);
            Time.timeScale = 1;
		}
	}

	void DoorAnimation()
	{
		FindObjectOfType<AudioManager> ().Play ("DoorTransition");
		StartCoroutine (GameObject.Find("Fade").GetComponent<FadeController>().Fading());
		GameObject.Find("Fade").GetComponent<Animator>().Rebind();
    }

    void LateUpdate()
    {
        GameManager.instance.MoveMainCamera(cameraPosition.position);
        needsCameraUpdate = false;
    }
}
