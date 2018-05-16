using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrePuzleController : MonoBehaviour {

	public GameObject text1;
	public GameObject text2;
	public GameObject text3;
	public GameObject text4;
	public GameObject text5;
	public GameObject text6;
	public GameObject text7;
	public GameObject text8;
	public GameObject text9;

	void Start () 
	{
		StartCoroutine (Animation ());
	}
	

	IEnumerator Animation()
	{
		yield return new WaitForSeconds(1f);

		text1.SetActive (true);
		yield return new WaitForSeconds(6f);
		text1.SetActive (false);
		FindObjectOfType<AudioManager>().Play("PuertaCerrada");
		yield return new WaitForSeconds(0.1f);
		FindObjectOfType<AudioManager>().Play("PuertaCerrada");
		yield return new WaitForSeconds(0.1f);
		FindObjectOfType<AudioManager>().Play("PuertaCerrada");

		yield return new WaitForSeconds(2f);
		//sonido de puerta
		text2.SetActive (true);
		yield return new WaitForSeconds(5f);
		text2.SetActive (false);

		text3.SetActive (true);
		yield return new WaitForSeconds(5f);
		text3.SetActive (false);
		FindObjectOfType<AudioManager>().Play("PuertaCerrada");
		yield return new WaitForSeconds(0.1f);
		FindObjectOfType<AudioManager>().Play("PuertaCerrada");
		yield return new WaitForSeconds(0.1f);
		FindObjectOfType<AudioManager>().Play("PuertaCerrada");

		yield return new WaitForSeconds(2f);
		//sonido de puerta
		text4.SetActive (true);
		yield return new WaitForSeconds(5f);
		text4.SetActive (false);

		text5.SetActive (true);
		yield return new WaitForSeconds(9f);
		text5.SetActive (false);

		text6.SetActive (true);
		yield return new WaitForSeconds(5f);
		text6.SetActive (false);

		text7.SetActive (true);
		yield return new WaitForSeconds(3f);
		text7.SetActive (false);

		text8.SetActive (true);
		StartCoroutine(GameObject.FindWithTag ("MainCamera").GetComponent<Shaker> ().Shake (5, .2f));
		FindObjectOfType<AudioManager>().Play("BrokenGlass");
		yield return new WaitForSeconds(1f);
		FindObjectOfType<AudioManager>().Play("BrokenGlass");
		yield return new WaitForSeconds(0.1f);
		FindObjectOfType<AudioManager>().Play("BrokenGlass");
		yield return new WaitForSeconds(3f);
		text8.SetActive (false);

		yield return new WaitForSeconds(2f);

		text9.SetActive (true);
		yield return new WaitForSeconds(7f);
		text9.SetActive (false);

		yield return new WaitForSeconds(2f);

		SceneManager.LoadScene("Puzle 0-0");
	}
}
