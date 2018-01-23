using UnityEngine;
using System.Collections;

public class RippleControl : MonoBehaviour {
	public AnimationCurve Curve = new AnimationCurve(new Keyframe(0,0), new Keyframe(1,1));
	private ParticleSystem _pSystem;

	// Use this for initialization
	void Start () {
		_pSystem = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(_pSystem != null)
		{
			float time = _pSystem.time/_pSystem.startLifetime;
			float value = Curve.Evaluate(time);
			this.renderer.material.SetFloat("_Step", value);
		}
	}
}
