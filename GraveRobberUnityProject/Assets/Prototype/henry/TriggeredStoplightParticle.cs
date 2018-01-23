using UnityEngine;
using System.Collections;

public class TriggeredStoplightParticle : Triggerable {

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem>().startColor = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void TriggeredActions (bool active)
	{
		base.TriggeredActions (active);
		if(active){
			GetComponent<ParticleSystem>().startColor = Color.green;
		}
		else{
			GetComponent<ParticleSystem>().startColor = Color.red;
		}
	}
}
