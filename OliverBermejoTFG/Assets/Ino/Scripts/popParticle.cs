using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popParticle : MonoBehaviour {
	public ParticleSystem pop;
	// Use this for initialization
	void Start () {
		pop = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "Player") {
			pop.Play ();
			StartCoroutine ("ds");
		}
	
	}
	IEnumerator ds(){
		yield return new WaitForSeconds (1f);
		Destroy (gameObject);

	}
}
