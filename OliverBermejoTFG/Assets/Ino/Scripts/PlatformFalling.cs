using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFalling : MonoBehaviour{
	public bool permitfall;
    public float gravity;
    public Vector3 startPosition;
    private Vector3 movDirection;
	public GameObject Viga;

    // Use this for initialization
    void Start(){
		gravity = -60.0f;
        startPosition = transform.position;
		permitfall = false;
    }

    // Update is called once per frame
    void Update(){
    }
    public void OnTriggerEnter(Collider other){
        if (other.gameObject.name == "Player"){
			StartCoroutine (anim ());
			if (permitfall == true) {
				//Debug.Log ("entra");
				StartCoroutine (Fall ());
			}
        }
    }
	public void OnTriggerStay(Collider other){
		if (other.gameObject.name == "Player") {
			if (permitfall == true) {
				//Debug.Log ("entra");
				StartCoroutine (Fall ());
			}
		}
	}
	IEnumerator anim(){
		GetComponent<Animation> ().Play();
		yield return new WaitForSeconds (0.25f);
		permitfall = true;
	}
	IEnumerator Activate(){
		yield return new WaitForSeconds (3.0f);
		transform.position = startPosition;
		gravity = -60;
		Viga.GetComponent<BoxCollider> ().enabled = true;
	}

	IEnumerator Fall(){
		yield return new WaitForSeconds (1.0f);
		Viga.GetComponent<BoxCollider> ().enabled = false;
		while (transform.position.y > (startPosition.y - 300)) 
		{
			transform.Translate (0, gravity * Time.deltaTime, 0);
			yield return 0;
		}
		StartCoroutine (Activate ());
	}

}