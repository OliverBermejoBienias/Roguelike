using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollider : MonoBehaviour {
	// Use this for initialization
	public Renderer[] bossr= new Renderer[7];
	Color rojo=Color.red;
	//Color micolor=new Color(150,150,150);
	Color elcolor;
	void Start () {
		for (int i = 0; i < bossr.Length; i++) {
			elcolor = bossr [i].GetComponent<Renderer> ().material.GetColor ("_Color"); ;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "player") {
			GameManager.instance.headcolided = true;
			StartCoroutine (damage ());
		}
	}
	IEnumerator damage(){
		float endtime =2f;

		while (endtime>0) {
			//Physics.IgnoreLayerCollision (10,12,true);
			endtime -= 0.5f;
			for (int i = 0; i < bossr.Length; i++) {
				bossr[i].material.color = rojo;
			}
			yield return new WaitForSeconds(0.2f);
			for (int i = 0; i < bossr.Length; i++) {
				bossr[i].material.color = elcolor;
			}
			yield return new WaitForSeconds(0.2f);
		}
		//Physics.IgnoreLayerCollision (10,12,false);
	}
}
