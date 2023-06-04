using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

	public float DelayT;

	// Use this for initialization
	void Start () {		
		StartCoroutine (go ());

	}
	
	IEnumerator go(){
	
		while (true) {
		
			GetComponent<Animation> ().Play ();
			yield return new WaitForSeconds (DelayT);
		}
	
	}
	/*void OnTriggerEnter(Collider other){
	
		if (other.gameObject.tag == "player") {
			PlayerController.Die ();
			//Destroy (other.gameObject);
		}
	
	}*/

}
