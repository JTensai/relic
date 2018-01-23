using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectTestObject : MonoBehaviour {
	public EffectBase EffectToTest;
	private Queue<EffectBase> _effects;
	// Use this for initialization
	void Start () {
		_effects = new Queue<EffectBase>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.O)){
			//Play the effect
			EffectBase eff = EffectToTest.GetInstance(this.transform.position);
			eff.PlayEffect();
			_effects.Enqueue(eff);
			Debug.Log ("Started effect: " + eff);
		}
		else if(Input.GetKeyUp(KeyCode.P)){
			//Stop the effect?
			if(_effects.Count > 0){
				EffectBase eff = _effects.Dequeue();
				eff.StopEffect();
				Debug.Log("Stopped effect: " + eff);
			}
		}
	}
}
