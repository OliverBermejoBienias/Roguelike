using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionbomba : MonoBehaviour {
	public GameObject explosion;
	public SphereCollider radioexplosion;
	// Use this for initialization
	void Start () {
		radioexplosion.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other){
		GameObject exp = Instantiate (explosion, this.transform.position, Quaternion.identity) as GameObject;
		radioexplosion.enabled = true;
		Destroy (exp,1f);
		StartCoroutine(desactivar());

	}
	public IEnumerator desactivar(){
		yield return new WaitForSeconds (1f);
		this.gameObject.SetActive (false);
	}
}
