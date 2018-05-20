using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEndController : MonoBehaviour {

	public GameObject npc1;
	public GameObject npc2;
	public GameObject npc3;
	public GameObject npc4;
	public GameObject npc5;
	public GameObject npc6;
	public GameObject animal1;
	public GameObject animal2;
	public GameObject animal3;
	public GameObject FIN;


	void Start () 
	{
		StartCoroutine (Animation ());
	}


	IEnumerator Animation()
	{
		
		yield return new WaitForSeconds(1.5f);

		animal1.SetActive (true);
		yield return new WaitForSeconds(3f);
		animal2.SetActive (true);
		yield return new WaitForSeconds(3f);
		animal3.SetActive (true);
		yield return new WaitForSeconds(7f);
		animal1.SetActive (false);
		animal2.SetActive (false);
		animal3.SetActive (false);

		yield return new WaitForSeconds(1f);

		npc1.SetActive (true);
		yield return new WaitForSeconds(7f);
		npc1.SetActive (false);

		yield return new WaitForSeconds(1f);

		npc2.SetActive (true);
		yield return new WaitForSeconds(7f);
		npc2.SetActive (false);

		yield return new WaitForSeconds(1f);

		npc3.SetActive (true);
		yield return new WaitForSeconds(7f);
		npc3.SetActive (false);

		yield return new WaitForSeconds(1f);

		npc4.SetActive (true);
		yield return new WaitForSeconds(7f);
		npc4.SetActive (false);

		yield return new WaitForSeconds(1f);

		npc5.SetActive (true);
		yield return new WaitForSeconds(7f);
		npc5.SetActive (false);

		yield return new WaitForSeconds(1f);

		npc6.SetActive (true);
		yield return new WaitForSeconds(7f);
		npc6.SetActive (false);

		yield return new WaitForSeconds(2f);

		FIN.SetActive (true);
		yield return new WaitForSeconds(7f);
		FIN.SetActive (false);

		yield return new WaitForSeconds(2f);

		SceneManager.LoadScene("MainMenu");
	}
}
