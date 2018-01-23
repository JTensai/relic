/// <summary>
/// Floor switch.cs
/// Notifies targets when activated or deactivated
/// </summary>

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TriggerDetector))]
public class FloorSwitch : Triggerable {


	//who can 'activate' this
	public enum triggeringActors{Player, Enemy, Both, None};
	public triggeringActors triggeredBy;
	//public bool PlayerTriggered = true;
	//public bool EnemyTriggered = false;

	public bool Sticky = true;
	
	private bool Triggered = false; 

	private GameObject switchObj;
	private TriggerDetector detectorScript;

	// Use this for initialization
	void Start () {
		switchObj = transform.GetChild(0).gameObject;
		detectorScript = switchObj.GetComponent<TriggerDetector>();
		detectorScript.PlayerTriggered = false;
		detectorScript.EnemyTriggered = false;
		if (triggeredBy == triggeringActors.Player || triggeredBy == triggeringActors.Both) {
			detectorScript.PlayerTriggered = true;
		}
		if (triggeredBy == triggeringActors.Player || triggeredBy == triggeringActors.Both) {
			detectorScript.EnemyTriggered = true;
		}
	}

	//called by self to set animations and notify targets it was triggered
	private void SelfTriggeredActions() {

		//call delegates
		NotifyTargets(Triggered);

		if (Triggered) {	//on swtich activating
			//play animation forward
			animation["floorSwitchPress"].speed = 1;
			animation["floorSwitchPress"].time = 0;
			animation.Play();
			//play sound

		} else {		//on switch deactivating
			//play animation reverse
			animation["floorSwitchPress"].speed = -1;
			animation["floorSwitchPress"].time = animation["floorSwitchPress"].length;
			animation.Play();
			//play sound

		}
	}

	//called when this is the target of something that was triggered
	public override void TriggeredActions(bool state) {
		Triggered = state;
		SelfTriggeredActions();
	}

	// Update is called once per frame
	void Update () {
		//when touching switch
		if ( detectorScript.TriggeredOn() ) {
			if ( !Triggered ) {
				Triggered = true;
				SelfTriggeredActions();
			}
		}
		//when nothing on switch
		if ( detectorScript.TriggeredOff() ) {
			if ( Triggered && !Sticky) {
				Triggered = false;
				SelfTriggeredActions();
			}
		}
	}
	
}
