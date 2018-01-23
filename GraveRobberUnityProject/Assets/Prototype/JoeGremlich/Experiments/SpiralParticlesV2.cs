using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SpiralParticlesV2 : MonoBehaviour {

	public bool reverse = false;
	public float offset = 0.0f;
	private float YVelocity = 0.0f;
	public float SpiralTightnessMultiplier = 1.0f;
	public AnimationCurve SpiralTightness = new AnimationCurve(new Keyframe(0,1), new Keyframe(1,1));
	public float SpeedMultiplier = 1.0f;


	private ParticleSystem _pSystem;
	private ParticleSystem.Particle[] _particles;

	// Use this for initialization
	void Start () {
		_pSystem = gameObject.GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
//		int direction;
//		if(reverse)
//			direction = -1;
//		else
//			direction = 1;

		
		YVelocity = _pSystem.startSpeed;
		_particles = new ParticleSystem.Particle[_pSystem.particleCount];
		int count = _pSystem.GetParticles (_particles);

		for(int i = 0; i<count; i++){
			float timeAlive = _particles[i].startLifetime - _particles[i].lifetime;

			float TimeAliveRatio = timeAlive/_pSystem.startLifetime;

			float currentTight = SpiralTightness.Evaluate(TimeAliveRatio) * SpiralTightnessMultiplier;
			//float currentSpeed = SpeedMultiplier.Evaluate(timeAlive);

			//float xPos = currentTight * Mathf.Cos (timeAlive * SpeedMultiplier * direction + offset);
			//float yPos = _particles[i].position.y + YVelocity;
			//float zPos = currentTight * Mathf.Sin (timeAlive * SpeedMultiplier * direction + offset);

			float xPos = currentTight * Mathf.Cos (timeAlive * SpeedMultiplier);
			float yPos = YVelocity;
			float zPos = currentTight * Mathf.Sin (timeAlive * SpeedMultiplier);

			//xPos += transform.position.x;
			//zPos += transform.position.z;

			_particles[i].velocity = new Vector3(xPos, yPos, zPos);
		}
	
		_pSystem.SetParticles (_particles, _pSystem.particleCount);
	}
}
