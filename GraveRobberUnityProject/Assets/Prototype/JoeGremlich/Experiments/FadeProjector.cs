using UnityEngine;
using System.Collections;

public class FadeProjector : MonoBehaviour {
	private Projector projector;
	private float clip;
	private ParticleSystem _pSystem;
	public AnimationCurve curve = new AnimationCurve(new Keyframe(0,0), new Keyframe(1,1));
	public float maxClip;

	// Use this for initialization
	void Start () {
		projector = GetComponent<Projector> ();
		clip = projector.farClipPlane;
		_pSystem = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_pSystem != null) {
			float pTime = _pSystem.time / _pSystem.startLifetime;
			float curveVal = curve.Evaluate (pTime);
			float clip = maxClip * curveVal;
			projector.farClipPlane = clip;
		}
	}
}
