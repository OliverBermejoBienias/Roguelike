using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositoAgua : MonoBehaviour {
	public GameObject burbujeo;
	public Transform spawnBurbujas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "player" && GameManager.instance.aguaPistola < 5) {
			GameManager.instance.aguaPistola = 5;
			GameObject Burbujas = Instantiate (burbujeo, spawnBurbujas.transform.position, Quaternion.identity) as GameObject;
			Destroy (Burbujas, 2.5f);
		}
	}
}
