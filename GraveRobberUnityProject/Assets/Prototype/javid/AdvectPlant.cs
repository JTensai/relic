using UnityEngine;
using System.Collections;

public class AdvectPlant : MonoBehaviour {

	// Use this for initialization
	Animator a;
	float time;
	bool sway = false;

	public float timeToStop  = 2f;
	public float swaySpeed = 50f;

	void Start () {
		a = GetComponent<Animator>();
		a.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("plant rigidbody.IsSleeping " + rigidbody.IsSleeping());

		// Liner interpolate back down to one, base on last interactopsn
		if (sway) {
						//float force = a.GetFloat ("force");
			float force = a.speed;
						time += Time.deltaTime;
			force = Mathf.Lerp (swaySpeed, 1f, time / timeToStop);
						//a.SetFloat ("force", force);
			a.speed = force; 
				if (a.speed <= 1) {
					a.speed = 1;
					sway = false;
					a.enabled = false;
				}
			}
	}

	void OnTriggerEnter(Collider other){
		//Debug.Log ("!");
		if (other.CompareTag ("Player")) {
			//Animator a = GetComponent<Animator>();
			a.speed = swaySpeed;
			a.enabled = true;
		/*	a.Play("violent",0,0f);
			//a.ForceStateNormalizedTime(0.0f);
			a.SetFloat("force",5f);
			Debug.Log ("2");*/
			sway = true;
			time = 0f;
		}
	}
}
