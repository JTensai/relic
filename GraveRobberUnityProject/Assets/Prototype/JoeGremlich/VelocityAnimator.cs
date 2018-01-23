using UnityEngine;
using System.Collections;

public class VelocityAnimator : MonoBehaviour {
	private ParticleSystem _pSystem;
	private ParticleSystem.Particle[] _particles;

	public float lamda = 2.0f;
	public float a = 1.0f;
	public float b = 1.0f;
	public float yHeight = 2.0f;
	public float lifetime = 10.0f;

	// Use this for initialization
	void Start () {
		_pSystem = gameObject.GetComponent<ParticleSystem> ();
		_pSystem.startLifetime = lifetime;
	}
	
	// Update is called once per frame
	void Update () {
		_particles = new ParticleSystem.Particle[_pSystem.particleCount];
		_pSystem.GetParticles (_particles);
		for(int i = 0; i < _particles.Length; i++) {
			float t = _particles[i].lifetime;
			t = lifetime - t;

			float xVel = ((b * lamda) * (Mathf.Cos (lamda * t))) - 
				((a + (b * lamda * t)) * (Mathf.Sin(lamda * t)));
			float yVel = ((b * lamda) * (Mathf.Sin (lamda * t))) + 
				((a + (b * lamda * t)) * (Mathf.Cos(lamda * t)));
			_particles[i].velocity = new Vector3(xVel, yVel, yHeight);
		}
		_pSystem.SetParticles (_particles, _pSystem.particleCount);
	}

	void OnDrawGizmos() {
		_pSystem = gameObject.GetComponent<ParticleSystem> ();
		Vector3 start = transform.position;
		float angle = this.transform.rotation.eulerAngles.y;

		if (yHeight > 0)
			angle += 0.0f;
		else
			angle += 180.0f;

		float length = lifetime * yHeight;
		float arcAngle = 360.0f * b;

		DebugDraw.DrawArc(start, angle, length, arcAngle, Color.red);
	}
	
}
