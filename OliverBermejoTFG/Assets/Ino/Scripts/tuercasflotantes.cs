using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tuercasflotantes : MonoBehaviour {
	public float speedR;
	public Vector3 startPos;
	public float delta;
	public float speed;


	// Use this for initialization
	void Start () {
		startPos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 v = startPos;
		v.y += delta * Mathf.Sin(Time.time * speed);
		transform.position = v;
		transform.Rotate( new Vector3 (0, 0, speedR * Time.deltaTime));
	}

}
