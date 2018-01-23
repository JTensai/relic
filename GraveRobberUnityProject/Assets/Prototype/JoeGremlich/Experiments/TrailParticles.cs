using UnityEngine;
using System.Collections;

public class TrailParticles : MonoBehaviour {

	private ParticleSystem _pSystem;
	public TrailBase trail;
	public float start = 0.01f;

	private TrailBase tb;
	private Transform tail;

	// Use this for initialization
	void Start () {
		_pSystem = GetComponent<ParticleSystem> ();
		tb = trail.GetInstance (_pSystem.transform.position);
		tb.transform.rotation = _pSystem.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		tail = tb.transform.GetChild (0);
		ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[_pSystem.particleCount];
		int count = _pSystem.GetParticles (_particles);

		for (int i = 0; i < count; i++){
			float timeAlive = _particles[i].startLifetime - _particles[i].lifetime;

			Vector3 location = _particles[i].position;
			if(timeAlive < start){
				tb.StopEffect();
				tb = trail.GetInstance(_pSystem.transform.position);
				tb.transform.rotation = _pSystem.transform.rotation;
			}
			else {

				tail.localPosition = location;
			}
		}

		_pSystem.SetParticles (_particles, count);
	}
}
