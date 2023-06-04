using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinchos_polvo : MonoBehaviour {
	public GameObject polvo;
	// Use this for initialization
	void Start () {
		polvo.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other){
	
		if (other.gameObject.tag == "spikes") {
			
			polvo.SetActive (true);
		}
	}
	void OnTriggerExit(Collider other){
		
		polvo.SetActive (false);
	}
}
