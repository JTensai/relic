using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SetPRotAxis : MonoBehaviour {
	public Vector3 axis;

	private ParticleSystem _pSystem;

	private ParticleSystem.Particle[] myArray;
	// Use this for initialization
	void Start () {
		_pSystem = GetComponent<ParticleSystem> ();
		myArray = new ParticleSystem.Particle[_pSystem.particleCount];
	}
	
	// Update is called once per frame
	void Update () {


	//	ParticleSystem.Particle[] myArray = new ParticleSystem.Particle[_pSystem.particleCount];
		int count = _pSystem.GetParticles(myArray);
		
		for(int i = 0;i < count;i++)
		{
			myArray[i].axisOfRotation = axis;
		}

		_pSystem.SetParticles (myArray, count);
	}
}
