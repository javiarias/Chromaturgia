using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAndDeactivationController : MonoBehaviour {

	Animator animator;

	public int[] identifierList = new int[5];
    public bool RotateLeft = false;

    public bool isWallActive = true;

    bool containsWall;

    private void Awake()
    {
        for (int i = 0; i < identifierList.Length; i++)
        {
            if (identifierList[i] == 0)
            {
                identifierList[i] = -1;
            }
        }

		// caching
		containsWall = gameObject.name.Contains ("Wall");
        gameObject.GetComponent<BoxCollider2D>().enabled = isWallActive;

        if (containsWall)
        {
            animator = gameObject.GetComponent<Animator>();
            animator.SetBool("On", isWallActive);
        }
    }

    public void OrbLeverSignal()
    {
		if (containsWall)
        {
            isWallActive = !isWallActive;

            animator = gameObject.GetComponent<Animator> ();
			animator.SetBool ("On", isWallActive);
			Invoke ("OcultacionMuro", 0.5f);
        }
        else
        {
            int rotationModifier = -1;
            if (RotateLeft)
            {
                rotationModifier = 1;
            }
            gameObject.transform.Rotate(Vector3.forward * rotationModifier * 90);
        }
    }

	void OcultacionMuro()
	{
		gameObject.GetComponent<BoxCollider2D> ().enabled = isWallActive;
	}
}
