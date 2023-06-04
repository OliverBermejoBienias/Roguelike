using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingplat : MonoBehaviour {
	public float speedr = 5f;
	public bool permiteRotar;
	// Use this for initialization
	void Start () {
		permiteRotar = true;
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator rotar(){
		yield return new WaitForSeconds (1.25f);
		while (transform.rotation.eulerAngles.z <= 90) {
			transform.Rotate (0,0, speedr * Time.deltaTime);
			//transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));
			yield return null;
		}
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));
		permiteRotar = false;
		StartCoroutine (volver ());

	}
	IEnumerator volver(){
		yield return new WaitForSeconds (1f);
		while (transform.rotation.eulerAngles.z - speedr * Time.deltaTime >= 0 ) {
			transform.Rotate (0,0, -speedr * Time.deltaTime);
			//transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));
			yield return null;
		}
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
		permiteRotar = true;

	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "player" && permiteRotar == true) {
			StartCoroutine(rotar());
		}
	}
}
