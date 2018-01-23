using UnityEngine;
using System.Collections;

public class ForwardRipple : MonoBehaviour {
	public AnimationCurve Animation = new AnimationCurve();
	private ParticleSystem _pSystem;
	// Use this for initialization
	void Start () {
		_pSystem = this.GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[_pSystem.particleCount];
		int count = _pSystem.GetParticles (_particles);

		for(int i = 0; i < _particles.Length; i++)
		{
			ParticleSystem.Particle p = _particles[i];

		}
		float pTime = _pSystem.time;
		pTime /= _pSystem.startLifetime;
		GetComponent<ParticleRenderer> ().renderer.sharedMaterial.SetFloat ("_MainSlider", Animation.Evaluate (pTime));
	}
}
