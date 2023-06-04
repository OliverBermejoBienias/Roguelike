using UnityEngine;
using UnityEngine.SceneManagement;


public class Camera2DFollow : MonoBehaviour{

	public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
	public int escena;
	void Start (){
		escena = SceneManager.GetActiveScene ().buildIndex;
		if (escena == 1) {

			target = null;
		}
	}

    void LateUpdate(){
		//Transform posj=target.GetComponent<PlayerController>().movDirection.x;
		Vector3 desiredPosition = target.transform.position + offset;
			Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);
			transform.position = smoothedPosition;

		//transform.LookAt (target);



    }
}