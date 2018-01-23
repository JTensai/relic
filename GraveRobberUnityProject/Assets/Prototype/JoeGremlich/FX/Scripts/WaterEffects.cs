using UnityEngine;
using System.Collections;

public class WaterEffects : MonoBehaviour {
	public EffectBase WaterSplash;
	void OnTriggerEnter(Collider other)
	{
		if(WaterSplash != null){
			EffectBase newInstance = WaterSplash.GetInstance (other.transform.transform.position);
			newInstance.PlayEffect();
		}
	}
}
