using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMosaicController : MonoBehaviour {

    public GameObject redPiece, greenPiece, bluePiece;
    public GameObject floorMosaic;
    Transform[] floorMosaicPieces;
    bool allPicked = false;
    
	void Start () {
        floorMosaicPieces = floorMosaic.GetComponentsInChildren<Transform>(true);
	}

    void CheckPickedPieces()
    {
        redPiece.SetActive(GameManager.instance.redPiecePicked);
        greenPiece.SetActive(GameManager.instance.greenPiecePicked);
        bluePiece.SetActive(GameManager.instance.bluePiecePicked);
    }

    void Update()
    {
        if (!allPicked)
            CheckPickedPieces();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            floorMosaicPieces[1].gameObject.SetActive(GameManager.instance.redPiecePicked); // red
            floorMosaicPieces[2].gameObject.SetActive(GameManager.instance.greenPiecePicked); // green
            floorMosaicPieces[3].gameObject.SetActive(GameManager.instance.bluePiecePicked); // blue
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            floorMosaic.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            floorMosaic.SetActive(false);
        }
    }
}
