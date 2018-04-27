using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosaicPiecesBehaviour : MonoBehaviour {

    void Start()
    {
        SetPiecesActive();
    }

    void SetPiecesActive()
    {
        if (gameObject.name == "Red")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = GameManager.instance.level1Complete;
            gameObject.GetComponent<Collider2D>().enabled = GameManager.instance.level1Complete;
        }
        else if (gameObject.name == "Green")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = GameManager.instance.level2Complete;
            gameObject.GetComponent<Collider2D>().enabled = GameManager.instance.level2Complete;
        }
        else if (gameObject.name == "Blue")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = GameManager.instance.level3Complete;
            gameObject.GetComponent<Collider2D>().enabled = GameManager.instance.level3Complete;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
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
            Destroy(gameObject);
        }
    }
}
