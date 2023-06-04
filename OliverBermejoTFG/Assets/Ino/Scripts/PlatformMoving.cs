using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour {

    public Vector3 startPos;
    public float delta;
    public float speed;
    public bool arriba;
    

    // Use this for initialization
    void Start () {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 v = startPos;
        float valor =delta* Mathf.Sin(Time.time * speed);
        int valorbueno=Mathf.RoundToInt(valor);
        v.y += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
        if (valorbueno == 330)
        {
            arriba = true;
        }
        if (valorbueno == -330)
        {
            arriba = false;
        }
        }
    public void OnTriggerStay(Collider other){
        //Debug.Log(PlayerController.forceJump);
        if (other.gameObject.name == "Player"){
            other.transform.SetParent(gameObject.transform);         
            if (arriba == true){
                    PlayerController.forceJump = 24f;
            }
            if (arriba == false){
                    PlayerController.forceJump = 28.5f;
            }           
        }
    }
    public void OnTriggerExit(Collider other){
        if (other.gameObject.name == "Player"){
            other.transform.parent = null;
            PlayerController.forceJump = 28.5f;
        }
    }
}
