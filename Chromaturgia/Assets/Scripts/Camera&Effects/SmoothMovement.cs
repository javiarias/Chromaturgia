using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMovement : MonoBehaviour {

    Vector3 playerPos;
    Vector3 mosaicPos;
    Vector3 movementDestination;
    Vector3 velocity;
    bool move = false;

    float time = 0.3f;

    void Update ()
    {
        if (move)
        {
            transform.position = Vector3.SmoothDamp(transform.position, movementDestination, ref velocity, time, Mathf.Infinity, Time.deltaTime);
            if (Vector3.Distance(transform.position, movementDestination) <= 0.01f && movementDestination == mosaicPos)
            {
                move = false;
                Invoke("ResetPos", 0.6f);
            }
            if (Vector3.Distance(transform.position, movementDestination) <= 0.01f && movementDestination == playerPos)
            {
                move = false;
                gameObject.GetComponentInParent<Movement>().canMove = true;
            }
        }
	}

    public void ActivateMovement()
    {
        gameObject.GetComponentInParent<Movement>().canMove = false;

        playerPos = gameObject.transform.position;
        mosaicPos = GameObject.Find("WallMosaic").transform.position + new Vector3(-0.72f, -0.317f, -2);
        velocity = Vector3.zero;
        movementDestination = mosaicPos;

        transform.position = Vector3.SmoothDamp(transform.position, movementDestination, ref velocity, time, Mathf.Infinity, Time.deltaTime);

        move = true;
    }

    void ResetPos()
    {
        velocity = Vector3.zero;
        movementDestination = playerPos;
        move = true;
    }
}
