using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMosaicController : MonoBehaviour {

    GameObject redPiece, greenPiece, bluePiece;
    GameObject floorMosaic;
    
	void Start () {
		// get children, activate the pieces that are already picked-- maybe 
        // i need three more bool in GameManager to keep track of picked
        // pieces-- not the same as finished levels

	}

    void Caching()
    {
        redPiece = gameObject.GetComponentsInChildren<GameObject>(true)[0];
    }

    void OnTriggerEnter2D()
    {
        // check if its player, if it is, pick the 2nd child and activate
    }
}
