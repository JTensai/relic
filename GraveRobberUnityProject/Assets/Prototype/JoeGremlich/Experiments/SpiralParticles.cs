using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SpiralParticles : MonoBehaviour {

	public bool reverse = false;
	public bool randomizeSpeed = false;
	public float offset = 0.0f;
	public float YVelocity = 0.0f;
	public AnimationCurve SpiralTightness = new AnimationCurve(new Keyframe(0,1), new Keyframe(10,1));
	public float SpeedMultiplier = 1.0f;


	private ParticleSystem _pSystem;
	private ParticleSystem.Particle[] _particles;

	// Use this for initialization
	void Start () {
		_pSystem = gameObject.GetComponent<ParticleSystem> ();

	}
	
	// Update is called once per frame
	void Update () {
		if(randomizeSpeed)
			SpeedMultiplier = Random.Range(1.0f, 10.0f);
		int direction;
		if(reverse)
			direction = -1;
		else
			direction = 1;


		_particles = new ParticleSystem.Particle[_pSystem.particleCount];
		int count = _pSystem.GetParticles (_particles);

		for(int i = 0; i<count; i++){
			float timeAlive = _particles[i].startLifetime - _particles[i].lifetime;

			float currentTight = SpiralTightness.Evaluate(timeAlive);
			//float currentSpeed = SpeedMultiplier.Evaluate(timeAlive);

			float xPos = currentTight * Mathf.Cos (timeAlive * SpeedMultiplier * direction + offset);
			float yPos = _particles[i].position.y + YVelocity;
			float zPos = currentTight * Mathf.Sin (timeAlive * SpeedMultiplier * direction + offset);

			//xPos += transform.position.x;
			//zPos += transform.position.z;

			_particles[i].position = new Vector3(xPos, yPos, zPos);
		}
	
		_pSystem.SetParticles (_particles, _pSystem.particleCount);
	}
}
