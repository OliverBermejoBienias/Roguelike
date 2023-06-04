using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proyectilwater : MonoBehaviour {
	public GameObject salpicadura;
	// Use this for initialization
	void Start () {
		//salpicadura.Stop ();
		gameObject.SetActive (true);
	}

	// Update is called once per frame
	void Update () {

	}
	void OnCollisionEnter (Collision other){
		GameObject particleAgua = Instantiate (salpicadura, gameObject.transform.position, Quaternion.identity) as GameObject;
		gameObject.SetActive (false);
		Destroy (particleAgua, 0.7f);
		Destroy (gameObject, 0.8f);
	}
}
