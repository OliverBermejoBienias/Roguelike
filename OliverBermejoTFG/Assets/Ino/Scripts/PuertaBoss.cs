using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaBoss : MonoBehaviour {
	public Collider doorC;
	public Animator DoorAnim;
	// Use this for initialization
	void Start () {
		DoorAnim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (GameManager.instance.puertaBoss == true) {
			DoorAnim.SetBool ("abrir", true);
			StartCoroutine (OpenDoorTime ());
		}
	}
	IEnumerator OpenDoorTime(){
		yield return new WaitForSeconds (1);
		doorC.enabled = false;
	}
	void OnTriggerEnter (Collider other){
		if (other.gameObject.name == "Player") {
			SceneManager.LoadScene (2);
		}
	}
}
