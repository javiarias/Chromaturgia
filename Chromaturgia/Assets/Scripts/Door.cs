using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour 
{
	public Transform salida;
	public Camera mainCamera;
	public Transform posCamara;

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			coll.gameObject.transform.position = salida.position;
            Vector3 cameraPos = new Vector3(posCamara.transform.position.x, posCamara.transform.position.y, -2.6f);
			mainCamera.gameObject.transform.position = cameraPos;
		}
	}
}
