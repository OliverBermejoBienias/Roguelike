using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour {

    Quaternion _start, _end; 

    private float _angle = 60.0f;
    private float _speed = 2.0f;
    private float _startTime = 0.0f;

    // Use this for initialization
    void Start(){
        _start = PendulumRotation(_angle);
        _end = PendulumRotation(-_angle);
    }

    // Update is called once per frame
    void Update(){

    }

    void FixedUpdate(){
        _startTime += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(_start, _end, (Mathf.Sin(_startTime * _speed + Mathf.PI / 2.0f) + 1.0f) / 2.0f);
    }

    void ResetTimer(){
        _startTime = 0.0f;
    }

    Quaternion PendulumRotation(float angle){
        var pendulumRotation = transform.rotation;
        var angleZ = pendulumRotation.eulerAngles.z + angle;

        if(angleZ > 180){
            angleZ -= 360;
        }
        else if(angleZ < -180){
            angleZ += 360;
        }
        pendulumRotation.eulerAngles = new Vector3(angleZ, pendulumRotation.eulerAngles.y, pendulumRotation.eulerAngles.z);
        return pendulumRotation;
    }
}
