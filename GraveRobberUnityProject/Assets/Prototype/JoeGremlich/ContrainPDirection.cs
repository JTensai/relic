using UnityEngine;
using System.Collections;

public class ContrainPDirection : MonoBehaviour {

	public bool X = false;
	public bool Y = false;
	public bool Z = false;

	private ParticleSystem _pSystem;

	public 
	// Use this for initialization
	void Start () {
		_pSystem = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[_pSystem.particleCount];
		int _count = _pSystem.GetParticles (_particles);

		for(int i = 0; i < _count; i++){
			if(X)
				_particles[i].velocity = new Vector3(0, _particles[i].velocity.y, _particles[i].velocity.z);
			if(Y)
				_particles[i].velocity = new Vector3(_particles[i].velocity.x, 0, _particles[i].velocity.z);
			if(Z)
				_particles[i].velocity = new Vector3(_particles[i].velocity.x, _particles[i].velocity.y, 0);
		}

		_pSystem.SetParticles (_particles, _pSystem.particleCount);
	}
}
