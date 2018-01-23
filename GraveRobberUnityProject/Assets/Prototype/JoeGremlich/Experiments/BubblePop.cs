using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BubblePop : MonoBehaviour {
	public AnimationCurve DissolveAmtOverTime;
	private ParticleSystem _pSystem;
	// Use this for initialization
	void Start (){
		_pSystem = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		float time = _pSystem.time / _pSystem.duration;
		float amt = DissolveAmtOverTime.Evaluate (time);
		this.renderer.sharedMaterial.SetFloat ("_DissolveAmt", amt);
	}
}
