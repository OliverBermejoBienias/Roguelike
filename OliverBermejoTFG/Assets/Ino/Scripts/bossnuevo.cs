using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossnuevo : MonoBehaviour {
	private StatesBoss CurrentState = StatesBoss.Idle;
	public Transform bosst;
	public Transform target;
	public Transform bspawn;
	public Transform rspawn;
	public float Bspeed;
	public float fireangle = 45.0f;
	public float gravity = 9.8f;
	private float tiempo;
	public int Launchnum;
	public int SpawnedRat;
	public int randomCase;
	public int bhits = 0;
	public int estado=0;
	public int ProyectilLanzado=1;
	public int RataLanzada=1;
	public GameObject proyectil;
	public GameObject rata;
	public GameObject puertaBoss;
	public static bool BossHit;
	public GameObject escapeIdle1;
	public GameObject escapeIdle2;
	public GameObject humoDañoEscape1;
	public GameObject humoDañoEscape2;
	public GameObject humoDaño2Escape1;
	public GameObject humoDaño2Escape2;
	public GameObject motoruno;
	public GameObject motordos;
	public GameObject polvoMuerteBoss;
	public ParticleSystem shield;
	public Collider escudo;
	public Animator jefe;
	// Use this for initialization
	void Start () {
		PlayerController.permitDoublej = true;
		/*humo=GameManager.instance.humoidle;
		humo.Play ();
		humod=GameManager.instance.GolpeUno;
		humod.Stop ();
		humodd=GameManager.instance.GolpeDos;
		humodd.Stop ();
		motoruno=GameManager.instance.motorUno;
		motoruno.Stop ();
		motordos=GameManager.instance.motorDos;
		motordos.Stop ();*/
		Launchnum = 0;
		SpawnedRat = 0;

		StartCoroutine (ChooseStates ());
	}
	
	// Update is called once per frame
	void Update () {
	}
	public enum StatesBoss{
		Idle,
		Dash,
		SpawnRats,
		Launch,
		GoBack,
		Weak,
		Death
	}

	public IEnumerator ChooseStates(){
		switch (CurrentState) {
		case StatesBoss.Idle:
			yield return new WaitForSeconds (0.5f);
			StartCoroutine (DoIdle ());
			break;
		case StatesBoss.Dash:
			StartCoroutine (DoDash ());
			break;
		case StatesBoss.SpawnRats:
			StartCoroutine (SpawnR ());
			break;
		case StatesBoss.Launch:
			StartCoroutine (Lanzar ());
			break;
		case StatesBoss.GoBack:
			StartCoroutine (DoGoback ());
			break;
		case StatesBoss.Weak:
			StartCoroutine (GoWeak ());
			break;
		case StatesBoss.Death:
			StartCoroutine (GoDeath ());
			break;
		}
		yield return null;
	}

	public IEnumerator DoIdle(){
		Launchnum = 0;
		SpawnedRat = 0;
		BossHit = false;
		GameManager.instance.headcolided = false;
		if (bhits <= 3) {
			switch (estado) {
			case 0:
				CurrentState = StatesBoss.Launch;
				break;
			case 1:
				CurrentState = StatesBoss.Dash;
				break;
			case 2:
				CurrentState = StatesBoss.SpawnRats;
				break;
			}
			estado += 1;
			yield return new WaitForSeconds (0.4f);
			StartCoroutine (ChooseStates ());
			yield return null;
		} 

	}

	public IEnumerator DoDash(){
		jefe.SetBool ("dash", true);
		yield return new WaitForSeconds (2.5f);
		Bspeed = 15;
		bool parar = false;
		while(parar==false){
			Vector3 fwd = transform.TransformDirection (transform.right * -1);
			RaycastHit hit;
			Debug.DrawRay (transform.position, fwd * 11, Color.red);
			if (Physics.Raycast (transform.position, fwd, out hit, 11)) {
				if (hit.collider.tag == "pared") {
					Bspeed = 0;
					jefe.SetBool ("dash", false);
					parar = true;
				}
			}
			transform.Translate (-Vector2.right * Bspeed * Time.fixedDeltaTime);
			yield return new WaitForFixedUpdate ();
		}
		StartCoroutine ("WaitBack");
		yield return null;
	}

	public IEnumerator DoGoback(){
		Bspeed = 10;
		bool parar=false;
		while(parar==false){
			Vector3 bwd = transform.TransformDirection (transform.right);
			RaycastHit hitb;
			Debug.DrawRay (transform.position, bwd * 7, Color.red);
			if (Physics.Raycast (transform.position, bwd, out hitb, 7)) {
				if (hitb.collider.tag == "pared") {
					Bspeed = 0;
					jefe.SetBool ("back", false);
					parar = true;
					}
				}
			transform.Translate (Vector2.right * Bspeed * Time.fixedDeltaTime);
			yield return new WaitForFixedUpdate ();
		}
		CurrentState = StatesBoss.Idle;
		StartCoroutine (ChooseStates ());
		yield return null;
	}

	public IEnumerator GoWeak(){
		shield.Stop ();
		escudo.enabled = false;
		jefe.SetBool ("weak", true);
		while (GameManager.instance.headcolided == false) {
			yield return null;
		}
		estado = 0;
		bhits += 1;
		if (bhits == 1){
			escapeIdle1.SetActive (false);
			escapeIdle2.SetActive (false);
			humoDañoEscape1.SetActive (true);
			humoDañoEscape2.SetActive (true);
			motoruno.SetActive (true);
		}
		if (bhits == 2) {
			humoDañoEscape1.SetActive (false);
			humoDañoEscape2.SetActive (false);
			motoruno.SetActive (false);
			humoDaño2Escape1.SetActive (true);
			humoDaño2Escape2.SetActive (true);
			motordos.SetActive (true);
		}
		GameManager.instance.batteryspawn ();
		//Debug.Log (bhits);
		jefe.SetBool ("weak", false);
		CurrentState = StatesBoss.Idle;
		yield return new WaitForSeconds (1f);
		if (shield.isStopped)
			shield.Play ();
		escudo.enabled = true;
		if (bhits < 3) {
			StartCoroutine (ChooseStates ());
			yield return null;
		}
		else
		{
			StartCoroutine (morir ());
		}

	}

	public IEnumerator GoDeath(){
		jefe.SetBool ("muerte", true);
		shield.Stop ();
		escudo.enabled = false;
		yield return new WaitForSeconds (2.5f);
		GameObject polvoBoss = Instantiate (polvoMuerteBoss, this.transform.position, Quaternion.identity) as GameObject;
		Destroy (gameObject);
	}

	public IEnumerator morir(){
		GameManager.instance.puertaBoss = true;
		CurrentState = StatesBoss.Death;
		StartCoroutine (ChooseStates ());
		yield return null;

	}

	public IEnumerator WaitBack(){
		Bspeed = 0;
		jefe.SetBool ("back", true);
		yield return new WaitForSeconds (4.0f);
		CurrentState = StatesBoss.GoBack;
		StartCoroutine (ChooseStates ());
	}

	public IEnumerator Lanzar(){
		Launchnum = 0;
		while (Launchnum < ProyectilLanzado) {
			jefe.SetBool ("lattack", true);
			yield return new WaitForSeconds (4f);
			GameObject clone = Instantiate (proyectil, bspawn.position, Quaternion.identity) as GameObject;
			float target_Distance = Vector3.Distance (bspawn.position, target.position);
			float projectile_Velocity = target_Distance / (Mathf.Sin (2 * fireangle * Mathf.Deg2Rad) / gravity);
			float Vx = Mathf.Sqrt (projectile_Velocity) * Mathf.Cos (fireangle * Mathf.Deg2Rad);
			float Vy = Mathf.Sqrt (projectile_Velocity) * Mathf.Sin (fireangle * Mathf.Deg2Rad);
			float flightDuration = target_Distance / Vx;
			float elapse_time = 0;
			clone.transform.rotation = Quaternion.LookRotation (target.position - clone.transform.position);
			while (elapse_time < flightDuration) {
				clone.transform.Translate (0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

				elapse_time += Time.deltaTime;

				yield return null;
				jefe.SetBool ("lattack", false);
			}
			clone.SetActive (false);
			Destroy (clone.gameObject,1.5f);
			Launchnum += 1;
		}
		ProyectilLanzado += 1;
		CurrentState = StatesBoss.Idle;
		StartCoroutine (ChooseStates ());
	}

	public IEnumerator SpawnR(){
		jefe.SetBool ("sattack", true);
		GameObject [] ratas = new GameObject[RataLanzada];
		for (int x = 0;  x <ratas.Length; x++) {
			yield return new WaitForSeconds (3f);
			ratas[x]= Instantiate(rata,rspawn.position,Quaternion.Euler(new Vector3(0,-90, 0))) as GameObject;
		}
		bool quedanRatas = true;
		while (quedanRatas) {
			//Premisa optimista: no quedan!
			quedanRatas = false;
			for (int x = 0; x < ratas.Length; x++) {
				if (ratas [x] != null)
					quedanRatas = true;
			}
			yield return new WaitForSeconds (0.2f);
		}
		jefe.SetBool ("sattack", false);
		yield return null;
		RataLanzada += 1;
		CurrentState = StatesBoss.Weak;
		StartCoroutine (ChooseStates ());

	}
}
