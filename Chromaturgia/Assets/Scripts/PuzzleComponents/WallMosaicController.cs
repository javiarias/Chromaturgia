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
        floorMosaicPieces[1].gameObject.SetActive(GameManager.instance.redPiecePicked);
        floorMosaicPieces[2].gameObject.SetActive(GameManager.instance.greenPiecePicked);
        floorMosaicPieces[3].gameObject.SetActive(GameManager.instance.bluePiecePicked);
    }

    void Update()
    {
        if (!allPicked)
            CheckPickedPieces();
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

    public void GameFinished()
    {
        StartCoroutine(Sounds());
        Invoke("Finish", 2f);
    }

    void Finish()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        GameObject door = GameObject.Find("Ending").transform.GetChild(0).gameObject;
        door.GetComponent<BoxCollider2D>().enabled = false;
        door.transform.GetChild(0).gameObject.SetActive(true);
        door.transform.GetChild(1).gameObject.SetActive(true);
        door.transform.GetChild(2).gameObject.SetActive(true);
        door.transform.GetChild(3).gameObject.SetActive(true);
    }

    IEnumerator Sounds()
    {
        FindObjectOfType<AudioManager>().Play("BrokenGlass");
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<AudioManager>().Play("BrokenGlass");
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<AudioManager>().Play("BrokenGlass");
        yield return new WaitForSeconds(1f);
        FindObjectOfType<AudioManager>().Play("DoorTransition");
    }
}
