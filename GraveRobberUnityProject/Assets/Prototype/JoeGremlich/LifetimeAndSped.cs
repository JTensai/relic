using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode]
public class LifetimeAndSped : MonoBehaviour {

	public float SpeedMulitiplier = 1.0f;
	public float lifetimeMultiplier = 1.0f;
	public bool AffectRotation = false;
	public float RotationMultiplier = 2.0f;
	public bool AffectEmission = false;
	public float EmissionRateMultiplier = 1.0f;

	private pusher _pusher;
	private ParticleSystem _pSystem;

	private ParticleSystem.Particle[] myArray;

	// Use this for initialization
	void Start () {
		
		_pusher = transform.GetComponentInParent<pusher> ();
		_pSystem = GetComponent<ParticleSystem> ();

		myArray = new ParticleSystem.Particle[_pSystem.particleCount];
	}
	
	// Update is called once per frame
	void Update () {
		float _targetLength;
		
		float _length = _pusher.effectLength;
		float _strength = _pusher.pushStrength;
		
		_targetLength = _length;
		
		float _life = getLifetime (_targetLength, _strength);
		
		_pSystem.startLifetime = _life * lifetimeMultiplier;
		_pSystem.startSpeed = _strength * SpeedMulitiplier;
		
		if(AffectRotation){
			
			//ParticleSystem.Particle[] myArray = new ParticleSystem.Particle[_pSystem.particleCount];
			int count = _pSystem.GetParticles(myArray);
			
			for(int i = 0;i < count;i++)
			{
				//float velocity = UnityEngine.Random.Range(first, second);
				float velocity = RotationMultiplier *_strength - 1;
				myArray[i].rotation += velocity;
			}
			
			_pSystem.SetParticles (myArray, count);
		}
		
		if(AffectEmission){
			_pSystem.emissionRate = (1 + 3*_strength) * EmissionRateMultiplier;
		}
	}

	
	private float getLifetime(float targetLength, float speed){
		return (float) targetLength/speed;
	}
}
