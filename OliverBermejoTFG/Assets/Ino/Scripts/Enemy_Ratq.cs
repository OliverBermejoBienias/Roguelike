using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ratq : MonoBehaviour {
	private bool faceRight;
	public float speed = 8.5f;
	public Vector3 StartPos;
	public int Limit=20;
	public float RatRot = 60f;
	private bool Stoprot;
	private bool StoprotL;
    public GameObject fBox;
    public GameObject bBox;
	public GameObject rBox;
	public GameObject lBox;
	public GameObject Polvo;
	// Use this for initialization
	void Start () {
		faceRight = true;
		StartPos = transform.position;
	}
	void Update(){
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
		if (faceRight) {
			if (transform.position.x >= StartPos.x + Limit) {
					speed = 0;
					StartCoroutine ( "RotateR");
			}
		} else {
			if (!faceRight) {
				if (transform.position.x  <=  StartPos.x - Limit) {
						speed = 0;
						StartCoroutine ( "RotateL");
				}
			}
		}
	}

	IEnumerator FlipR(){
		faceRight = !faceRight;
		Limit *= -1;
		yield return 0;
	}
	IEnumerator RotateR(){
		faceRight = !faceRight;
		while (Stoprot == false) {
			transform.Rotate (new Vector3 (0, RatRot * Time.deltaTime, 0));
			if (transform.rotation.eulerAngles.y >= 270) {
				transform.rotation = Quaternion.Euler (new Vector3 (0, 270, 0));
				Stoprot = true;
				}
			yield return 0;
			}

		speed = 10;
		Stoprot = false;
		}

	IEnumerator RotateL(){
		faceRight = !faceRight;
		while (Stoprot == false) {
			transform.Rotate (new Vector3 (0, -RatRot * Time.deltaTime, 0));
			if (transform.rotation.eulerAngles.y <= 90) {
				transform.rotation = Quaternion.Euler (new Vector3 (0, 90, 0));
				Stoprot = true;
			}
			yield return 0;
		}
		speed = 10;
		Stoprot = false;
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "Player") {
			speed = 0;	
			RatRot = 0;
			GetComponent<BoxCollider> ().enabled = false;
			GetComponent<Animator> ().SetBool ("muerto", true);
            fBox.GetComponent<BoxCollider>().enabled = false;
            bBox.GetComponent<BoxCollider>().enabled = false;
			lBox.GetComponent<BoxCollider>().enabled = false;
			rBox.GetComponent<BoxCollider>().enabled = false;
            EfectoMuerte();
			Destroy (gameObject.transform.parent.gameObject,1f);

		}

	}
    private void EfectoMuerte(){
        Destroy(Instantiate(Polvo, transform.position + (new Vector3 (0, 1, 0)), Quaternion.Euler(new Vector3(90,-90,0))),2f);
    }

}
