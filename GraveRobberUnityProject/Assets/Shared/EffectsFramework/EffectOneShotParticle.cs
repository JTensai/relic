using UnityEngine;
using System.Collections;

public class EffectOneShotParticle : EffectBase {
	ParticleSystem[] _particleSystems;
	private float _longestDuration;
	private float _elapsedTime;
	// Use this for initialization
	void Start () {

	}

	
	// Update is called once per frame
	void Update () {
		if(_isPlaying){
			_elapsedTime += Time.deltaTime;
			if(_elapsedTime >= _longestDuration){
				StopEffect();
			}
		}
	}

	#region implemented abstract members of EffectBase
	
	protected override void InitializeEffect(){
		_particleSystems = GetComponentsInChildren<ParticleSystem>();
		foreach(ParticleSystem p in _particleSystems){
			float lifeTime = p.startLifetime + p.duration;
			_longestDuration = Mathf.Max(lifeTime, _longestDuration);
			p.loop = false;
			p.Stop();
		}
	}

	protected override void StartEffect ()
	{
		if (_particleSystems != null){
			foreach(ParticleSystem p in _particleSystems){
				p.Play();
			}
		}
	}

	#endregion

	#region implemented abstract members of EffectBase

	public override float GetAnimationRate ()
	{
		_particleSystems = GetComponentsInChildren<ParticleSystem>();
		float retVal = 0;
		foreach(ParticleSystem p in _particleSystems){
			retVal = Mathf.Max (retVal, p.emissionRate);
		}
		return retVal;
	}

	public override void SetAnimationRate (float val)
	{
		_particleSystems = GetComponentsInChildren<ParticleSystem>();
		foreach(ParticleSystem p in _particleSystems){
			p.emissionRate = val;
		}
	}

	public override void MultiplyAnimationRate (float mult)
	{
		_particleSystems = GetComponentsInChildren<ParticleSystem>();
		foreach(ParticleSystem p in _particleSystems){
			p.emissionRate *= mult;
		}
	}

	#endregion
}
