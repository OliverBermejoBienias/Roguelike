using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour{
    //Player variables
    public float speed;
    public static float forceJump;
    public float forceJumpJetpack;
    public float gravity;
	public float velProyetil = 30;
	public static bool permitDoublej;
	public static bool permitShoot;
	public bool DoubleJump;
	public bool stopsalto;
	public bool canflip;
	public bool canload;
	public bool canloadroutine;
	public bool canrespawn;
	public static bool facingright;
	public bool batactiv;
	private bool activar;
	public GameObject agua;
	public Transform shotSpawn;
	public GameObject tuercaPart;
	public GameObject pilaParticle;
	public GameObject BuhoDaño;
	//public bool emision;
	public int inverso = 1;
    public Texture2D fadeTexture;
    //Player animator
    public ParticleSystem dust;
	public ParticleSystem[] boost=new ParticleSystem[4];
	public ParticleSystem[] dead=new ParticleSystem[3];
	public static Animator InobotAnim;
	public Renderer rend;
	public Renderer pistolrend;
	public Renderer jetrend;
	public int escena;
	public Vector3 checkstart;
	public Vector3 checkuno;
	public Vector3 checkdos;
	public Vector3 checktres;
	public Vector3 lvldoscheckstart;
	public Vector3 lvldoscheckuno;
	public Vector3 lvldoscheckdos;
	public Vector3 lvldoschecktres;
    //permite acceder al scrip de stat
    public bool fadeout;
    [Range(0.1f, 1f)]
    public float fadespeed;
    public int drawDepth = -1000;
    private float alpha = 1f;
    private float fadeDir = -1f;
    [SerializeField]
    private Stats health;
	public Vector3 movDirection;
    //Funcion de los huesos para que se desplomen cuando el jugador muera
	void SetKinematic(bool newValue)
	{
		Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody rb in bodies)
		{
			rb.isKinematic = newValue;
		}
	}
    private void Awake()
    {
		health.initialize();
		escena = SceneManager.GetActiveScene ().buildIndex;
		if (escena == 0) {
			PlayerPrefs.SetInt ("PlayerController", 0);
        
			if (PlayerPrefs.GetInt ("PlayerController") == 0) {
				this.transform.position = checkstart;
				health.CurrentValue = 100;
			}
			if (PlayerPrefs.GetInt ("PlayerController") == 1) {
				this.transform.position = checkuno;
			}
			if (PlayerPrefs.GetInt ("PlayerController") == 2) {
				this.transform.position = checkdos;
			}
			if (PlayerPrefs.GetInt ("PlayerController") == 3) {
				this.transform.position = checktres;
			}
		}
		if (escena == 2) {
			PlayerPrefs.SetInt ("PlayerController", 7);

			if (PlayerPrefs.GetInt ("PlayerController") == 7) {
				this.transform.position = lvldoscheckstart;
				health.CurrentValue = 100;
			}
			if (PlayerPrefs.GetInt ("PlayerController") == 4) {
				this.transform.position = lvldoscheckuno;
			}
			if (PlayerPrefs.GetInt ("PlayerController") == 5) {
				this.transform.position = lvldoscheckdos;
			}
			if (PlayerPrefs.GetInt ("PlayerController") == 6) {
				this.transform.position = lvldoschecktres;
			}
		}
		for (int i = 0; i < dead.Length; i++) {
			dead [i].gameObject.SetActive (false);	
		}

    }
    void Start(){
		canload = false;
		canflip = true;
        fadeout = false;
		//GameManager.instance.dañoAlBuho = true;
        //esta variable con parametro de entrada hace que el jugador no se depslome al principio
        //permite el salto cuando tiene el jetpack
		permitDoublej = false;
		permitShoot = false;
        //Coge el animator
		SetKinematic(true);
		InobotAnim = GetComponent<Animator> ();
		facingright = true;
        speed = 17f;
        forceJump = 28.5f;
        forceJumpJetpack = 30f;
		gravity = 61f;

		if (escena == 0) {
			GameManager.instance.rellenod.SetActive (false);
			GameManager.instance.rellenocentro.SetActive (false);
			GameManager.instance.rellenoi.SetActive (false);
		}
		if (escena == 2) {
			GameManager.instance.agarre.SetActive (false);
			GameManager.instance.deposito.SetActive (false);
			GameManager.instance.cannon.SetActive (false);
		}

    }

    void Update(){
		//Debug.Log (GameManager.instance.dañoAlBuho);
		CharacterController controller = GetComponent<CharacterController>();   
		if (escena == 1) {
			forceJumpJetpack = 35f;
		}
		if (health.CurrentValue > 0) {
			health.CurrentValue -= GameManager.instance.resta;
			for (int i = 0; i < dead.Length; i++) {
				dead [i].gameObject.SetActive (false);	
			}
		} else {
			if (health.CurrentValue <= 0) {
				for (int i = 0; i < dead.Length; i++) {
					dead [i].gameObject.SetActive (true);	
				}
				if (canloadroutine == false) {
					StartCoroutine (descargado ());
				}
				if (canload == true) {
					if (escena == 0) {
						if (PlayerPrefs.GetInt ("PlayerController") == 0) {
							canload = false;
							this.transform.position = checkstart;
							health.CurrentValue = 100;
							GameManager.instance.activarbat ();
						}
						if (PlayerPrefs.GetInt ("PlayerController") == 1) {
							canload = false;
							this.transform.position = checkuno;
							health.CurrentValue = 50;
							GameManager.instance.activarbat ();
						}
						if (PlayerPrefs.GetInt ("PlayerController") == 2) {
							canload = false;
							this.transform.position = checkdos;
							health.CurrentValue = 50;
							GameManager.instance.activarbat ();
						}
						if (PlayerPrefs.GetInt ("PlayerController") == 3) {
							canload = false;
							this.transform.position = checktres;
							health.CurrentValue = 50;
							GameManager.instance.activarbat ();
						}
					}
					if (escena == 1) {
						SceneManager.LoadScene (1, LoadSceneMode.Single);
					}
					if (escena == 2) {
						if (PlayerPrefs.GetInt ("PlayerController") == 7) {
							canload = false;
							this.transform.position = lvldoscheckstart;
							health.CurrentValue = 100;
							GameManager.instance.activarbat ();
						}
						if (PlayerPrefs.GetInt ("PlayerController") == 4) {
							canload = false;
							this.transform.position = lvldoscheckuno;
							health.CurrentValue = 50;
							GameManager.instance.activarbat ();
						}
						if (PlayerPrefs.GetInt ("PlayerController") == 5) {
							canload = false;
							this.transform.position = lvldoscheckdos;
							health.CurrentValue = 50;
							GameManager.instance.activarbat ();
						}
						if (PlayerPrefs.GetInt ("PlayerController") == 6) {
							canload = false;
							this.transform.position = lvldoschecktres;
							health.CurrentValue = 50;
							GameManager.instance.activarbat ();
						}
					}
				}
			} 
		}
        
		if (escena == 2) {
			if (Input.GetMouseButtonDown (0) && permitShoot==true && GameManager.instance.aguaPistola > 0) {
				GameObject proyectil = Instantiate (agua, shotSpawn.position, Quaternion.identity) as GameObject;
				GameManager.instance.aguaPistola -= 1;
				if (facingright == true) {
					proyectil.GetComponent<Rigidbody> ().velocity = this.transform.right * velProyetil;
					proyectil.transform.Rotate (0, 180, 0);
				} else {
					proyectil.GetComponent<Rigidbody> ().velocity = this.transform.right * -1 * velProyetil;
				}
				InobotAnim.SetBool ("shoot_run", true);
				Destroy (proyectil, 1.25f);
			} else {
				InobotAnim.SetBool ("shoot_run", false);
			}
		}
		if (controller.isGrounded) {
			for (int i = 0; i < boost.Length; i++) {
				boost [i].gameObject.SetActive (false);	
			}
			InobotAnim.SetInteger ("OnAir", 0);
            InobotAnim.SetInteger("inicio", 0);
            InobotAnim.SetBool ("dmg", false);
			movDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);
			movDirection = transform.TransformDirection (movDirection);
			movDirection *= speed;
            DoubleJump = false;
		} else {
			movDirection = new Vector3 (Input.GetAxis ("Horizontal"), movDirection.y , 0);
			movDirection = transform.TransformDirection (movDirection);
			movDirection.x *= speed;
			movDirection.z *= speed / 2;
			InobotAnim.SetInteger ("OnAir", 1);
		}          
		if (Input.GetButtonDown ("Jump")) {				
				if (controller.isGrounded) {
					stopsalto = false;
                    StartCoroutine(saltoi());
					movDirection.y = forceJump;
				} else {
					if(permitDoublej){
						if (DoubleJump) {
							DoubleJump = false;
							stopsalto = true;
							movDirection.y = forceJumpJetpack;
						}	
					}
				}
			}
		if (Input.GetButtonUp ("Jump") && !controller.isGrounded && !stopsalto && permitDoublej) {
			DoubleJump = true;
		}
		if (GameManager.instance.activated == true) {
			if (Input.GetButton ("Jump")) {	
				for (int i = 0; i < boost.Length; i++) {
					boost [i].gameObject.SetActive (true);	
				}
			} else {
				if (Input.GetButtonUp ("Jump")) {
					for (int i = 0; i < boost.Length; i++) {
						boost [i].gameObject.SetActive (true);	
					}
				}
			}
		}
		movDirection.y -= gravity * Time.deltaTime;
		controller.Move(movDirection * Time.deltaTime);
		float move = Input.GetAxis ("Horizontal");
		InobotAnim.SetFloat ("AnimSpeed", Mathf.Abs (move));
		if (canflip == true) {
			if (move > 0 && !facingright) {
				Flip ();
			} else {
				if (move < 0 && facingright)
					Flip ();
			}
			if (Mathf.Abs (move) == 0 || !controller.isGrounded) {
				Invoke ("stopemit", 0.1f);
			} else {
				if (move != 0) {
					Invoke ("emit", 0f);
				}
			}
		}
    }
   
	void Flip(){	
		facingright = !facingright;
		Vector3 Therotate = transform.localScale; 
		Therotate.x *= -1;
		inverso *= -1;
		transform.localScale = Therotate;
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "tuerca") {
			GameManager.instance.numtuercas += 1;
			GameManager.instance.sumatuerca ();
			GameObject particleTuerca = Instantiate (tuercaPart, other.transform.position, Quaternion.identity) as GameObject;
			Destroy (other.gameObject);
			Destroy (particleTuerca, 0.9f);
		}

		if (other.gameObject.tag == "jetpackpart") {
			GameManager.instance.JetCounter += 1;
			Destroy (other.gameObject);
		}
		if (other.gameObject.name == "Jetpackcentro") {
			GameManager.instance.rellenocentro.SetActive (true);
			PlayerPrefs.SetInt ("PlayerController", 1);
		}
		if (other.gameObject.name == "Jetpackd") {
			GameManager.instance.rellenod.SetActive (true);
			PlayerPrefs.SetInt ("PlayerController", 2);
		}
		if (other.gameObject.name == "Jetpacki") {
			GameManager.instance.rellenoi.SetActive (true);
			PlayerPrefs.SetInt ("PlayerController", 3);
		}
		if (other.gameObject.name == "Gun_Principal") {
			GameManager.instance.GunCounter += 1;
			GameManager.instance.cannon.SetActive (true);
			PlayerPrefs.SetInt ("PlayerController", 5);
			Destroy (other.gameObject);
		}
		if (other.gameObject.name == "Gun_Deposito") {
			GameManager.instance.GunCounter += 1;
			GameManager.instance.deposito.SetActive (true);
			PlayerPrefs.SetInt ("PlayerController",6);
			Destroy (other.gameObject);
		}
		if (other.gameObject.name == "Gun_Brazo") {
			GameManager.instance.GunCounter += 1;
			GameManager.instance.agarre.SetActive (true);
			PlayerPrefs.SetInt ("PlayerController", 4);
			Destroy (other.gameObject);
		}
		if (other.gameObject.tag == "spikes" || other.gameObject.tag == "saw" || other.gameObject.tag == "pendulo" || other.gameObject.tag == "dead") {
			if (PlayerPrefs.GetInt ("PlayerController") == 0) {
				
				StartCoroutine ("deathtotal");
				Debug.Log ("estrue");
				if (canrespawn == true) {
					Debug.Log ("entra");
					this.transform.position = checkstart;
					health.CurrentValue = 100;
					GameManager.instance.activarbat ();
				}
			}
			if (PlayerPrefs.GetInt ("PlayerController") == 1) {
				StartCoroutine ("deathtotal");
				if (canrespawn == true) {
					this.transform.position = checkuno;
					health.CurrentValue = 50;
					GameManager.instance.activarbat ();
				}
			}
			if (PlayerPrefs.GetInt ("PlayerController") == 2) {
				StartCoroutine ("deathtotal");
				if (canrespawn == true) {
					this.transform.position = checkdos;
					health.CurrentValue = 50;
					GameManager.instance.activarbat ();
				}
			}
			if (PlayerPrefs.GetInt ("PlayerController") == 3) {
				StartCoroutine ("deathtotal");
				if (canrespawn == true) {
					this.transform.position = checktres;
					health.CurrentValue = 50;
					GameManager.instance.activarbat ();
				}
			}
		}
		if (other.gameObject.tag == "helice" || other.gameObject.tag=="deadvaciolvldos") {
			if (PlayerPrefs.GetInt ("PlayerController") == 7) {
				StartCoroutine ("deathtotal");
				if (canrespawn == true) {
					this.transform.position = lvldoscheckstart;
					health.CurrentValue = 100;
					GameManager.instance.activarbat ();
				}
			}
			if (PlayerPrefs.GetInt ("PlayerController") == 4) {
				StartCoroutine ("deathtotal");
				if (canrespawn == true) {
					this.transform.position = lvldoscheckuno;
					health.CurrentValue = 50;
					GameManager.instance.activarbat ();
				}
			}
			if (PlayerPrefs.GetInt ("PlayerController") == 5) {
				StartCoroutine ("deathtotal");
				if (canrespawn == true) {
					this.transform.position = lvldoscheckdos;
					health.CurrentValue = 50;
					GameManager.instance.activarbat ();
				}
			}
			if (PlayerPrefs.GetInt ("PlayerController") == 6) {
				StartCoroutine ("deathtotal");
				if (canrespawn == true) {
					this.transform.position = lvldoschecktres;
					health.CurrentValue = 50;
					GameManager.instance.activarbat ();
				}
			}
		}
		if (other.gameObject.tag == "rat") {
			if(health.CurrentValue>0){
				health.CurrentValue -= 4f;
			}

			StartCoroutine (damage (2.0f));

			InobotAnim.SetBool("dmg",true);
			//StartCoroutine ("Impulso");
		}
		if (other.gameObject.tag == "Firetrap" ) {
			if(health.CurrentValue>0){
				health.CurrentValue -= 20f;
			}
			StartCoroutine (damage (2.0f));
			InobotAnim.SetBool("dmg",true);
			//StartCoroutine ("Impulsof");
		}
		if (other.gameObject.tag == "bomba" ) {
			if(health.CurrentValue>0){
				health.CurrentValue -= 15f;
			}
			StartCoroutine (damage (2.0f));
			InobotAnim.SetBool("dmg",true);
			//StartCoroutine ("Impulsof");
		}
        if (other.gameObject.tag == "rata") {
			movDirection.y +=30.0f;
        }
		if (other.gameObject.tag == "buhohead") {
			movDirection.y +=30.0f;
		}
		if (other.gameObject.tag == "buho") {
			if(health.CurrentValue>0){
				health.CurrentValue -= 5f;
			}
			StartCoroutine (damage (2.0f));
			InobotAnim.SetBool("dmg",true);
			StartCoroutine ("Impulso");
		}
		if (other.gameObject.tag == "bateria") {
			if (health.CurrentValue > 0 && health.CurrentValue <= 99f) {
				health.CurrentValue += 30f;		
			}
			if(health.CurrentValue > 100){
				health.CurrentValue = 100f;
			}
			GameObject particlePila = Instantiate (pilaParticle, other.transform.position, Quaternion.identity) as GameObject;
			other.gameObject.SetActive (false);
			Destroy (particlePila, 0.7f);
		}
		if (other.gameObject.tag == "bosshead" ) {
			InobotAnim.SetBool("dmg",true);
			//movDirection.y += 9.0f;
			StartCoroutine ("HeadImpulso");
		}
		if (other.gameObject.tag == "shield" ) {
			if(health.CurrentValue>0){
				health.CurrentValue -= 10f;
			}
			InobotAnim.SetBool("dmg",true);
			movDirection.y += 30.0f;
			//StartCoroutine ("ShieldImpulso");
			StartCoroutine (damage (2.0f));
		}
		if (other.gameObject.tag == "next" ) {
			SceneManager.LoadScene (1);
		}
	}

	IEnumerator damage(float waittime){
		float endtime =2.5f;

		while (endtime>0) {
			//GameManager.instance.dañoAlBuho = false;
			Physics.IgnoreLayerCollision (10,12,true);
			endtime -= 0.5f;
			rend.enabled = false;
			jetrend.enabled = false;
			pistolrend.enabled = false;
			yield return new WaitForSeconds(0.2f);
			rend.enabled = true;
			jetrend.enabled = true;
			pistolrend.enabled = true;
			yield return new WaitForSeconds(0.2f);
		}
		Physics.IgnoreLayerCollision (10,12,false);
		//GameManager.instance.dañoAlBuho = true;
	}
    IEnumerator saltoi()
    {
        if (Input.GetButton("Jump")) {
            InobotAnim.SetInteger("inicio", 1);
            yield return new WaitForSeconds(0.07f);
            InobotAnim.SetInteger("OnAir", 1);
        }
    }
    IEnumerator Impulso()
	{
		float tiempazo = Time.time;
		Vector3 dire = new Vector3(-2*inverso,1,0);
		for (int i = 0; i < 200; i++) 
		{
			GetComponent<CharacterController>().Move(dire * 7.5f * Time.deltaTime);
			yield return 0;
			if (tiempazo + 0.4f < Time.time)
				break;
		}
		InobotAnim.SetBool("dmg",false);
	}
	IEnumerator Impulsof()
	{
		float tiempazo = Time.time;
		Vector3 dire = new Vector3(-2*inverso,1,0);
		for (int i = 0; i < 50; i++) 
		{
			GetComponent<CharacterController>().Move(dire * 7.5f * Time.deltaTime);
			yield return 0;
			if (tiempazo + 0.3f < Time.time)
				break;
		}
		InobotAnim.SetBool("dmg",false);
	}
	IEnumerator ShieldImpulso()
	{
		float tiempazo = Time.time;
		Vector3 dire = new Vector3(-2*inverso,1,0);
		for (int i = 0; i < 200; i++) 
		{
			GetComponent<CharacterController>().Move(dire * 7.5f * Time.deltaTime);
			yield return 0;
			if (tiempazo + 0.4f < Time.time)
				break;
		}
		InobotAnim.SetBool("dmg",false);
	}
	IEnumerator HeadImpulso()
	{
		
		float tiempazo = Time.time;
		Vector3 dire = new Vector3(-2*inverso,2,0);
		for (int i = 0; i < 200; i++) 
		{
			GetComponent<CharacterController>().Move(dire * 10f * Time.deltaTime);
			yield return 0;
			if (tiempazo + 0.4f < Time.time)
				break;
		}
		InobotAnim.SetBool("dmg",false);
	}
		
	public void emit(){	
		CharacterController controller = GetComponent<CharacterController> ();
		if (controller.isGrounded) {	
			var pm = dust.emission;
			pm.enabled = true;
		}
	}
	public void stopemit(){
		var pm=dust.emission;
		pm.enabled = false;
	}
	IEnumerator descargado(){
		canloadroutine = true;
		speed = 0;
		Invoke ("stopemit", 0.1f);
		InobotAnim.SetBool ("MuerteD", true);
		canflip = false;
		yield return new WaitForSeconds (3);
		fadeout = true;
		yield return new WaitForSeconds (1.2f);
		fadeout = false;
		canload = true;
		canflip = true;
		speed=17f;
		InobotAnim.SetBool ("MuerteD", false);
		yield return new WaitForSeconds (1f);
		canloadroutine = false;
	}
	IEnumerator deathtotal(){
		fadeout = true;
		canrespawn = true;
		yield return new WaitForSeconds (1f);
		fadeout = false;
	}
    void OnGUI()
    {
        if (fadeout)
        {
            alpha -= fadeDir * fadespeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);

            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
            GUI.depth = drawDepth;
        }
        else
        {
            alpha += fadeDir * fadespeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);

            Color newColor = GUI.color;
            newColor.a = alpha;

            GUI.color = newColor;

            GUI.depth = drawDepth;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
        }
    }
}
