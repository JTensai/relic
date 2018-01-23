using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CustomAngVel : MonoBehaviour {
	public float AngularVelocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ParticleSystem _pSystem = GetComponent<ParticleSystem> ();
		ParticleSystem.Particle[] myArray = new ParticleSystem.Particle[_pSystem.particleCount];
		int count = _pSystem.GetParticles(myArray);
		
		for(int i = 0;i < count;i++)
		{
			float velocity = AngularVelocity;
			myArray[i].rotation += velocity;
		}
		
		_pSystem.SetParticles (myArray, count);
	}
}
