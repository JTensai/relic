using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class TriggerDetector : MonoBehaviour {

	[HideInInspector]
	public bool PlayerTriggered = true;
	[HideInInspector]
	public bool EnemyTriggered = false;
	
	private int thingsInBounds = 0;
	private bool triggered = false;
	private bool forcedTrigger = false;

	private GameObject triggeringEntity;

	public GameObject TriggeringEntity() {
		return triggeringEntity;
	}

	public void ForceTrigger(){
		Debug.Log("Forced");
		forcedTrigger = true;
	}

	public bool TriggeredOn() {
		if (thingsInBounds > 0 && triggered || forcedTrigger) {
			triggered = false;
			return true;
		}
		return false;
	}

	public bool TriggeredOff() {
		if (thingsInBounds < 1 && triggered) {
			triggered = false;
			return true;
		}
		return false;
	}

	public bool ThingInBounds() {
		if (thingsInBounds > 0)
			return true;
		return false;
	}

	void OnTriggerEnter(Collider other) {
		if ( ( other.GetComponent<PlayerBase>() && PlayerTriggered ) || ( other.GetComponent<MonsterBase>() && EnemyTriggered ) ) {
			thingsInBounds++;
			triggeringEntity = other.gameObject;
		}
		if (thingsInBounds == 1) {
			triggered = true;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if ( ( other.GetComponent<PlayerBase>() && PlayerTriggered ) || ( other.GetComponent<MonsterBase>() && EnemyTriggered ) ) {
			thingsInBounds--;
			triggeringEntity = other.gameObject;
		}
		if (thingsInBounds <= 0) {
			triggered = true;
			thingsInBounds = 0;
		}
	}

}
