using UnityEngine;
using System.Collections;

public class BlindSpotterCoordinator : MonoBehaviour {
	public BlindMonster BlindSide;
	public Spotter Spotter;

	private HealthComponent _blindHealth;
	private HealthComponent _spotterHealth;
	// Use this for initialization
	void Start () {
		_blindHealth = BlindSide.GetComponent<HealthComponent>();
		_spotterHealth = Spotter.GetComponent<HealthComponent>();
	}
	
	// Update is called once per frame
	void Update () {
		if(BlindSide == null || Spotter == null){
			HandleKillOther();
		}
		else if((_blindHealth != null && !_blindHealth.IsAlive) || (_spotterHealth != null && !_spotterHealth.IsAlive)){
			HandleKillOther();
		}
	}

	private void HandleKillOther(){
		GameObject.Destroy(gameObject);
	}
}
