using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puerta : MonoBehaviour {
	public Collider doorC;
	public Animator DoorAnim;
	// Use this for initialization
	void Start () {
		DoorAnim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "Player"){
			DoorAnim.SetBool ("abrir", true);
			StartCoroutine (OpenDoorTime ());
		}
	}
	void OnTriggerExit(Collider other){
		if (other.gameObject.name == "Player"){
			DoorAnim.SetBool ("abrir", false);
			StartCoroutine (closeDoorTime ());
		}
	}
	IEnumerator OpenDoorTime(){
		yield return new WaitForSeconds (1);
		doorC.enabled = false;
	}
	IEnumerator closeDoorTime(){
		yield return new WaitForSeconds (1);
		doorC.enabled = true;
	}
}
