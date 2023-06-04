using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gears : MonoBehaviour {
	public float speedR;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate( new Vector3 (0, 0, speedR * Time.deltaTime));
	}
	public void OnColliderStay(Collider other){
		//Debug.Log(PlayerController.forceJump);
		if (other.gameObject.name == "Player"){
			other.transform.SetParent(gameObject.transform);         	         
		}
	}
	public void OnTriggerExit(Collider other){
		if (other.gameObject.name == "Player"){
			other.transform.parent = null;
		}
	}
}
