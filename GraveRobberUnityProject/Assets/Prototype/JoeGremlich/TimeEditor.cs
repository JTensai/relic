using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode]
public class TimeEditor : MonoBehaviour {

	public float lengthMultiplier;
	public float speedMultiplier;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		pusher _pusher = transform.GetComponentInParent<pusher> ();
		ParticleSystem _pSystem = GetComponent<ParticleSystem> ();
		
		float _life = _pusher.effectLength;
		float _speed = _pusher.pushStrength;

		_pSystem.startLifetime = _life * lengthMultiplier;
		_pSystem.startSpeed = _speed * speedMultiplier;

	}
	
	
	private float getLifetime(float speed){
		return (float)Math.Pow(1.6f, (-(speed - 4.2)));
	}
}
