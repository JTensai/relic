using UnityEngine;
using System.Collections;

public class AnimateNeon : MonoBehaviour {
	public AnimationCurve Animation = new AnimationCurve(new Keyframe(0,0), new Keyframe(1,1));
	private ParticleSystem _pSystem;

	// Use this for initialization
	void Start () {
		_pSystem = this.GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		float time = _pSystem.time / _pSystem.startLifetime;
		this.renderer.material.SetFloat ("_Slide", time);
	}
}
