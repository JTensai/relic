using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TriggerDetector))]
public class Teleport : Triggerable {

	public int uses = -1;

	//who can 'activate' this
	public enum triggeringActors{Player, Enemy, Both, None};
	public triggeringActors triggeredBy;
	//what method 'activates' this
	public enum activationMethods{OnEnter, OnExit, OnStay};
	public activationMethods on;


	public Transform targetLocation;
	private TriggerDetector detectorScript;

	// Use this for initialization
	void Start () {
		detectorScript = GetComponent<TriggerDetector>();	
		detectorScript.PlayerTriggered =false;
		detectorScript.EnemyTriggered = false;
		if (triggeredBy == triggeringActors.Player || triggeredBy == triggeringActors.Both) {
			detectorScript.PlayerTriggered = true;
		}
		if (triggeredBy == triggeringActors.Player || triggeredBy == triggeringActors.Both) {
			detectorScript.EnemyTriggered = true;
		}
	}



	private void TeleportTo(Transform target) {
		if (uses > 0 || uses <= -1) {
			uses--;
			if (uses == 0) {
				NotifyTargets(false);
			} else {
				if (targetLocation != null) {
					target.position = targetLocation.position;
				} else {
					Vector3 pos = this.transform.position;
					pos.y += 1;
					target.position = pos;
				}
				NotifyTargets(true);
				if (uses < 0 ) {
					uses = -1;
				}
			}

		}

	}

	// Update is called once per frame
	void Update () {
		//when entering tile
		if ( detectorScript.TriggeredOn() && on == activationMethods.OnEnter) {
			TeleportTo(detectorScript.TriggeringEntity().transform);
		}
		//when exiting tile
		if ( detectorScript.TriggeredOff() && on == activationMethods.OnExit ) {
			TeleportTo(detectorScript.TriggeringEntity().transform);
		}

	}
}
