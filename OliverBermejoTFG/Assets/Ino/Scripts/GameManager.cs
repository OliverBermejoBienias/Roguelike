using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public int escena;
	public int JetCounter;
	public int GunCounter;
	public bool activated;
	public bool headcolided;
	public bool givespeed;
	public bool puertaBoss;
	public int aguaPistola;
	//public bool dañoAlBuho;
	public GameObject Jetpack;
	public float resta=0.01f;
	public GameObject WaterGun;
	public GameObject rellenod;
	public GameObject rellenoi;
	public GameObject rellenocentro;
	public GameObject agarre;
	public GameObject cannon;
	public GameObject deposito;
	public GameObject[] baterias=new GameObject[8];
	public Text tuercas;
	public int numtuercas;
	public Transform batrespawn;
	public GameObject batery;
	//public Transform JetpackPos;
	public static GameManager instance;
	// Use this for initialization

	void Awake () {
		instance = this;
		//DontDestroyOnLoad (this);
	}

	void Start () {
		puertaBoss = false;
		headcolided = false;
		numtuercas = 0;
		JetCounter = 0;
		aguaPistola = 5;
		GunCounter = 0;
		WaterGun.SetActive (false);
		Jetpack.SetActive (false);
		activated = true;
		escena = SceneManager.GetActiveScene ().buildIndex;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (puertaBoss);
		if (escena == 0) {
			if (GameManager.instance.JetCounter == 3) {
				PutJetpack ();
			}
		} 
		if (escena == 2) {
			if (GameManager.instance.GunCounter == 3) {
				PlayerController.permitShoot = true;
				PutWaterGun ();
			}
		}
		if (escena!=0) {
			Jetpack.SetActive (true);
			PlayerController.permitDoublej = true;
		}

	}

	public void sumatuerca(){
		tuercas.text = numtuercas.ToString();
	}

	public void PutJetpack(){
			Jetpack.SetActive (true);
			PlayerController.permitDoublej = true;
			activated = true;
	}

	public void activarbat(){
		for (int i = 0; i < baterias.Length; i++) {
			if (baterias [i].gameObject.activeSelf == false) {
				baterias [i].gameObject.SetActive (true);
			}
		}
	}
	public void PutWaterGun(){
		WaterGun.SetActive (true);
	}
	public void batteryspawn(){
		GameObject bat = Instantiate (batery, batrespawn.position, Quaternion.identity) as GameObject;
	}
}
