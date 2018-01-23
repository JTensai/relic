using UnityEngine;
using System.Collections;

public class TriggerKillAmount : MonoBehaviour {


	bool used;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (!used) {
			used = true;
			GameUI.IncrementStatCounter(0f, 0f, StatTracker.StatTypes.ShamblerDeath);
		}
		
	}


}
