using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Particles : MonoBehaviour {
	private ParticleSystem Fire;
	public ParticleSystem subfire;
	private Collider Fire_Col;
	public bool playingsubfi;
	public float TimeOn;
	public float TimeOff;

	// Use this for initialization
	void Start () {
		Fire=GetComponent<ParticleSystem> ();
		Fire_Col = GetComponent<Collider> ();
		StartCoroutine (FireOn ());
		playingsubfi = false;
	}
	IEnumerator FireOn()
	{		
		while (true) 
		{
			if (Fire.isStopped)
			{
				Fire.Play ();
				Fire_Col.enabled = true;
				if (playingsubfi == false)
					StartCoroutine (subf ());
				yield return new WaitForSeconds (TimeOn);
			}

			if (Fire.isPlaying) 
			{
				Fire.Stop ();
				subfire.Stop ();
				Fire_Col.enabled = false;
				yield return new WaitForSeconds (TimeOff);
			}
		}
	}

	IEnumerator subf(){
		yield return new WaitForSeconds (0.3f);
		subfire.Play ();		
	}
}
