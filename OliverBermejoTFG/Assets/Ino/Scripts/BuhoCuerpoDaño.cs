using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuhoCuerpoDaño : MonoBehaviour {
	public float contadorVida;
	public GameObject buho;
	public Animator buhoAnimator;
	public Collider boxHead;
	public Collider boxBody;

	// Use this for initialization
	void Start () {
		contadorVida = 0f;
		buhoAnimator = buho.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (contadorVida >= 3) {
			boxHead.enabled = false;
			boxBody.enabled = false;
			buhoAnimator.SetBool ("dead", true);
			Destroy (gameObject, 1.7f);
		}
	}
	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "proyectil") {
			contadorVida += 1;
			Debug.Log ("daño");
		}
	}
}
