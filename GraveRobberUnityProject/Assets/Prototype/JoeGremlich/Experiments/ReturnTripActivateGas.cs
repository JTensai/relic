using UnityEngine;
using System.Collections;

public class ReturnTripActivateGas : MonoBehaviour {
	public LevelBehavior LB;
	private ParticleSystem _pSystem;

	// Use this for initialization
	void Start () {
		if (LB == null)
		{
			LB = GameObject.FindObjectOfType<LevelBehavior> ();
		}
		_pSystem = GetComponent<ParticleSystem> ();
		_pSystem.enableEmission = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(LB.ReturnTrip){
			_pSystem.enableEmission = true;
		}
	}
}
