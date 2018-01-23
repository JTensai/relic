using UnityEngine;
using System.Collections;

public class WaterSplash : MonoBehaviour {
	public EffectBase SplashEffect;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if(SplashEffect != null){
			EffectBase newInstance = SplashEffect.GetInstance(other.transform.position);
			newInstance.PlayEffect();
		}
	}
}
