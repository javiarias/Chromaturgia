using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosaicPiecesBehaviour : MonoBehaviour {

    void Update()
    {
        SetPiecesActive();
    }

    void SetPiecesActive()
    {
        if (gameObject.name == "Red")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = GameManager.instance.level1Complete;
            gameObject.GetComponent<Collider2D>().enabled = GameManager.instance.level1Complete;
            if (GameManager.instance.redPiecePicked)
            {
                Destroy(gameObject);
            }
        }
        else if (gameObject.name == "Green")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = GameManager.instance.level2Complete;
            gameObject.GetComponent<Collider2D>().enabled = GameManager.instance.level2Complete;
            if (GameManager.instance.greenPiecePicked)
            {
                Destroy(gameObject);
            }
        }
        else if (gameObject.name == "Blue")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = GameManager.instance.level3Complete;
            gameObject.GetComponent<Collider2D>().enabled = GameManager.instance.level3Complete;
            if (GameManager.instance.bluePiecePicked)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
			FindObjectOfType<AudioManager> ().Play ("CollectPiece");

            if ((GameManager.instance.redPiecePicked && GameManager.instance.greenPiecePicked) || (GameManager.instance.redPiecePicked && GameManager.instance.bluePiecePicked) || (GameManager.instance.greenPiecePicked && GameManager.instance.bluePiecePicked))
            {
                GameManager.instance.openEndingDoor = false;
            }

            if (gameObject.name == "Red")
            {
                GameManager.instance.redPiecePicked = true;
            }
            else if (gameObject.name == "Green")
            {
                GameManager.instance.greenPiecePicked = true;
            }
            else if (gameObject.name == "Blue")
            {
                GameManager.instance.bluePiecePicked = true;
            }

            other.gameObject.GetComponentInChildren<SmoothMovement>().ActivateMovement();

            SaveLoad.instance.SaveLevels();
            Destroy(gameObject);
        }
    }
}