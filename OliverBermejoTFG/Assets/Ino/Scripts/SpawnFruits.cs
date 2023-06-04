using UnityEngine;
using System.Collections;

public class SpawnFruits : MonoBehaviour {

	public GameObject[] fruits;
	public float counter = 0;
	float forceX;
	float segundosR;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		counter += Time.deltaTime;

		if (counter >= segundosR) {
			segundosR = Random.Range (0.5f, 2.0f);
			int miRandom = Random.Range (0, fruits.Length);
			int forceYRandom = Random.Range (14, 20);
			if (transform.position.x > 0) {
				forceX = Random.Range (-3, -8);
			} else {
				forceX = Random.Range (4, 6);
			}

			GameObject clon = (GameObject) Instantiate (fruits [miRandom], transform.position, fruits[0].transform.rotation);
			clon.GetComponent <Rigidbody> ().AddForce (new Vector3 (forceX, forceYRandom, 0), ForceMode.Impulse);
			counter = 0;
		}
	}
}
