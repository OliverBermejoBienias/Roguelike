using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGearR : MonoBehaviour {
	public float RotateSpeed=3f;
	public GameObject gear;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	void OnTriggerStay(Collider other){

		if (other.gameObject.tag == "player") {

			gear.transform.Rotate (0,5 * RotateSpeed * Time.deltaTime, 0);

		}

	}
	void OnTriggerExit(Collider other){



		gear.transform.Rotate (0, 0, 0);



	}
}
