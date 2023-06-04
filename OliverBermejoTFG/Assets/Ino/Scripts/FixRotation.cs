using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour {

	// Use this for initialization
	Quaternion rotation;
	void Awake()
	{
		rotation = transform.rotation;
	}
	void LateUpdate()
	{
		transform.rotation = rotation;
	}
	public void OnTriggerStay(Collider other){
		//Debug.Log(PlayerController.forceJump);
		if (other.gameObject.name == "Player"){
			other.transform.SetParent(gameObject.transform);         
			/*if (arriba == true){
				PlayerController.forceJump = 20f;
			}
			if (arriba == false){
				PlayerController.forceJump = 28.5f;
			}*/           
		}
	}
	public void OnTriggerExit(Collider other){
		if (other.gameObject.name == "Player"){
			other.transform.parent = null;
			//PlayerController.forceJump = 28.5f;
		}
	}
}
