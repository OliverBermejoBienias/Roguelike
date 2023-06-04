using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuhoEnemy : MonoBehaviour {
	private PlayerController theplayer;
	private stateowl CurrState = stateowl.Idle;
	public float speed=1.0f;
	public Vector3 startpos;
	public float playerRange;
	public LayerMask playerlayer;
	public bool PlayerInRange;
	float vel = 0.01f *1.1f;
	public GameObject dbox;
	public float distancia;
	public Animator buhoAnimator;
	public Collider boxHead;
	public Collider boxBody;

	// Use this for initialization
	void Start () {
		startpos = transform.position;
		transform.LookAt (this.transform.position+Vector3.back);
		theplayer = FindObjectOfType<PlayerController> ();
		StartCoroutine (stateadmin ());
		buhoAnimator = GetComponent<Animator> ();
	}
	public enum stateowl{
		Idle,
		dash,
		goback
	}
	// Update is called once per frame
	void Update () {
		distancia = Vector3.Distance (this.transform.position, theplayer.transform.position);
		if (boxBody.enabled == false || boxHead.enabled == false) {
			vel = 0;
		}
		if (distancia <= 20f) {
			PlayerInRange = true;
		} else {
			PlayerInRange = false;
		}
	}

	public IEnumerator stateadmin(){
		switch (CurrState) {
		case stateowl.Idle:
			StartCoroutine (DoIdle ());
			break;
		case stateowl.dash:
			StartCoroutine (DoDash ());
			break;
		case stateowl.goback:
			StartCoroutine (DoGoback ());
			break;
		}
		yield return null;
	}
	public IEnumerator DoIdle(){
		while (PlayerInRange == false) {
			yield return null;
		}
		StartCoroutine (DoDash ());
		yield return null;
	}
	public IEnumerator DoDash(){
		while (PlayerInRange == true) {
			transform.position = Vector3.Lerp (transform.position, theplayer.transform.position, vel);
			transform.LookAt (theplayer.transform.position);
			transform.localRotation = Quaternion.Euler(0.0f, transform.localEulerAngles.y, 0.0f);
			yield return null;
		}
		StartCoroutine (DoGoback ());
		yield return null;
	}
	public IEnumerator DoGoback(){
		
		while (PlayerInRange == false && transform.position != startpos && boxHead.enabled == true && boxBody.enabled == true) {
			transform.position = Vector3.MoveTowards(transform.position, startpos, 0.1f);;
			transform.LookAt (startpos);
			yield return null;
		}
		transform.LookAt (this.transform.position+Vector3.back);
		transform.localRotation = Quaternion.Euler(0.0f, transform.localEulerAngles.y, 0.0f);
		//if (transform.position == startpos) {
			StartCoroutine (DoIdle ());
		//}
		yield return null;
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "player" || other.gameObject.tag == "proyectil" /*&& GameManager.instance.dañoAlBuho == true*/) {
			boxHead.enabled = false;
			boxBody.enabled = false;
			buhoAnimator.SetBool ("dead", true);
			Destroy (gameObject, 1.7f);
		}
	}
	/*bool EstaEnDistancia()
	{
		distancia = Vector3.Distance (this.transform.position, theplayer.transform.position);

		if (distancia <= 20f) {
			return true;
		} else {
			return false;
		}
		return false;
	}*/
}
