using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {
    private float fill;
    [SerializeField]
    private Image content;

    public float Maxvalue { get; set; }
                             
    public float value {
        set {

            fill = map(value,0,Maxvalue,0,1);
        }

    }          
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        HandleBar(); 
	}

    private void HandleBar() {
        if (content.fillAmount != fill){
            content.fillAmount = fill;
        }
    }
    private float map(float value, float inMin,float inMax,float outMin,float outMax) {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;


    }
}
