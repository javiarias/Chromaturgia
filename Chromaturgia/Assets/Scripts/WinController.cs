using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour {

	public GameObject text1;
	public GameObject text2;
	public GameObject text3;
	public GameObject text4;
	public GameObject text5;
	public GameObject text6;
	public GameObject text7;
	public GameObject text8;
	public GameObject unknown;
	public GameObject what;

	void Start () 
	{
		StartCoroutine (Animation ());
	}


	IEnumerator Animation()
	{
		yield return new WaitForSeconds(1f);

		text1.SetActive (true);
		yield return new WaitForSeconds(4f);
		text1.SetActive (false);

		text2.SetActive (true);
		yield return new WaitForSeconds(2f);
		text2.SetActive (false);

		text3.SetActive (true);
		yield return new WaitForSeconds(2f);
		text3.SetActive (false);

		yield return new WaitForSeconds(3f);
		//FindObjectOfType<AudioManager>().Play("Win");
		StartCoroutine(GameObject.FindWithTag ("MainCamera").GetComponent<Shaker> ().Shake (4.5f, .05f));
		text4.SetActive (true);
		yield return new WaitForSeconds(4f);
		text4.SetActive (false);

		text5.SetActive (true);
		yield return new WaitForSeconds(4f);
		text5.SetActive (false);

		text6.SetActive (true);
		yield return new WaitForSeconds(8f);
		text6.SetActive (false);

		text7.SetActive (true);
		yield return new WaitForSeconds(3f);
		text7.SetActive (false);

		text8.SetActive (true);
		yield return new WaitForSeconds(4f);
		text8.SetActive (false);

		yield return new WaitForSeconds(3f);
		FindObjectOfType<AudioManager>().Play("Laugh");
		unknown.SetActive (true);
		yield return new WaitForSeconds(8f);
		unknown.SetActive (false);
		FindObjectOfType<AudioManager> ().StopAll ();
		yield return new WaitForSeconds(2f);

		what.SetActive (true);
		FindObjectOfType<AudioManager>().Play("Thunder");
		yield return new WaitForSeconds(0.2f);
		what.SetActive (false);
		FindObjectOfType<AudioManager>().Play("Thunder");
		yield return new WaitForSeconds(0.1f);
		what.SetActive (true);
		yield return new WaitForSeconds(0.2f);
		FindObjectOfType<AudioManager> ().Play ("Thunder");
		what.SetActive (false);

		yield return new WaitForSeconds(1f);

		what.SetActive (true);
		FindObjectOfType<AudioManager>().Play("Thunder");
		yield return new WaitForSeconds(0.1f);
		what.SetActive (false);
		FindObjectOfType<AudioManager>().Play("Thunder");
		yield return new WaitForSeconds(0.1f);
		what.SetActive (true);
		yield return new WaitForSeconds(0.1f);
		FindObjectOfType<AudioManager> ().Play ("Thunder");
		what.SetActive (false);

		yield return new WaitForSeconds(4f);

		SceneManager.LoadScene("TheEnd");
	}
}
